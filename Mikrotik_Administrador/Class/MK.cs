using Microsoft.IdentityModel.Tokens;
using Mikrotik_Administrador.Data;
using Mikrotik_Administrador.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace Mikrotik_Administrador.Class
{
    public class MK
    {
        Stream connection;
        TcpClient con;
        string _ip;
        int _port;

        // El constructor ahora solo guarda los datos, NO CONECTA
        public MK(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }
        public bool ConectarYLogin(string user, string pass)
        {
            try
            {
                con = new TcpClient();
                con.NoDelay = true; // Desactiva el algoritmo de Nagle (crucial para CCR)
                con.Connect(_ip, _port);
                connection = con.GetStream();

                // Esperamos 50ms a que el CCR estabilice la conexión antes de mandar los bytes
                System.Threading.Thread.Sleep(50);

                return this.Login(user, pass);
            }
            catch
            {
                return false;
            }
        }
        public bool Close()
        {
            try
            {
                if (connection != null)
                {
                    // Intentar enviar /quit solo si el socket sigue vivo
                    if (con != null && con.Connected)
                    {
                        Send("/quit", true);
                        System.Threading.Thread.Sleep(50);
                    }
                    connection.Dispose(); // Usar Dispose es más agresivo y limpio
                }
                if (con != null) con.Close();
                return true;
            }
            catch { return false; }
        }
        public void Send(string co)
        {
            Send(co, false);
        }
        public void Send(string co, bool endsentence = false)
        {
            byte[] bajty = Encoding.UTF8.GetBytes(co); // v7 prefiere UTF8
            byte[] velikost = EncodeLength(bajty.Length);

            byte[] paquete = new byte[velikost.Length + bajty.Length + (endsentence ? 1 : 0)];
            System.Buffer.BlockCopy(velikost, 0, paquete, 0, velikost.Length);
            System.Buffer.BlockCopy(bajty, 0, paquete, velikost.Length, bajty.Length);

            if (endsentence) paquete[paquete.Length - 1] = 0;

            connection.Write(paquete, 0, paquete.Length);
            // NO uses Flush después de cada palabra, deja que el buffer de red decida
        }
        private byte[] EncodeLength(int delka)
        {
            if (delka < 128)
            {
                return new byte[] { (byte)delka };
            }
            else if (delka < 16384)
            {
                return new byte[] { (byte)((delka >> 8) | 0x80), (byte)(delka & 0xFF) };
            }
            else if (delka < 2097152)
            {
                return new byte[] { (byte)((delka >> 16) | 0xC0), (byte)((delka >> 8) & 0xFF), (byte)(delka & 0xFF) };
            }
            return new byte[] { (byte)delka };
        }
        public List<string> Read()
        {
            List<string> output = new List<string>();

            int waitAttempts = 100;
            while (con.Available == 0 && waitAttempts > 0)
            {
                System.Threading.Thread.Sleep(50);
                waitAttempts--;
            }

            // Si después de la espera sigue en 0, es que el router recibió 
            // el comando pero no lo entendió o no lo aceptó.
            if (con.Available == 0)
            {
                System.Diagnostics.Debug.WriteLine("El router no mandó datos de respuesta.");
                return output;
            }

            while (true)
            {
                int curByte = connection.ReadByte();
                if (curByte == -1) break;

                long count = 0;
                if (curByte < 0x80) { count = curByte; }
                else if (curByte < 0xC0) { count = ((curByte ^ 0x80) << 8) + connection.ReadByte(); }
                else if (curByte < 0xE0) { count = ((curByte ^ 0xC0) << 16) + (connection.ReadByte() << 8) + connection.ReadByte(); }
                else if (curByte < 0xF0) { count = ((curByte ^ 0xE0) << 24) + (connection.ReadByte() << 16) + (connection.ReadByte() << 8) + connection.ReadByte(); }
                else if (curByte == 0xF0) { count = (connection.ReadByte() << 24) + (connection.ReadByte() << 16) + (connection.ReadByte() << 8) + connection.ReadByte(); }

                if (count == 0)
                {
                    if (output.Count > 0 && (output.Last().StartsWith("!done") || output.Last().StartsWith("!trap") || output.Last().StartsWith("!fatal")))
                    {
                        break;
                    }
                    continue;
                }

                byte[] buffer = new byte[count];
                int read = 0;
                while (read < count)
                {
                    int result = connection.Read(buffer, read, (int)count - read);
                    if (result <= 0) break;
                    read += result;
                }

                string word = Encoding.UTF8.GetString(buffer); // Usar UTF8 es mejor en v7
                output.Add(word);
            }
            return output;
        }
        public bool Login(string username, string password)
        {
            try
            {
                // Forzamos el envío de los 3 parámetros en un solo Flush
                Send("/login");
                Send("=name=" + username);
                Send("=password=" + password, true);
                connection.Flush(); // Solo un Flush al final de la frase

                // Espera obligatoria para el CCR2116 (proceso de autenticación interno)
                System.Threading.Thread.Sleep(250);

                List<string> respuesta = Read();

                if (respuesta.Count == 0)
                {
                    // Si llega vacío, intentamos una segunda lectura rápida
                    System.Threading.Thread.Sleep(250);
                    respuesta = Read();
                }

                return respuesta.Any(s => s.Contains("!done")) && !respuesta.Any(s => s.Contains("!trap"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error crítico: " + ex.Message);
                return false;
            }
        }
        public bool ActualizarVelocidadQueue(string Name, string Velocidad)
        {
            try
            {
                string Id = VerIdQueue(Name);
                // El comando 'set' requiere identificar el item, usualmente por su nombre (.id o name)
                Send("/queue/simple/set");

                // Indicamos cuál queue queremos modificar usando su nombre
                Send("=.id=" + Id);

                // Construimos el string de velocidad, ej: "2M/5M" o "512k/1M"
                // MikroTik acepta perfectamente los sufijos M y k
                Send("=max-limit=" + Velocidad, true);

                // Leemos la respuesta para confirmar que no hubo errores
                foreach (string row in Read())
                {
                    if (row.StartsWith("!trap"))
                    {
                        // Si el router devuelve !trap, hubo un error (ej. el nombre no existe)
                        return false;
                    }
                    if (row.StartsWith("!done"))
                    {
                        return true; // Éxito
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
        private string FormatearNumero(string numeroStr)
        {
            if (!long.TryParse(numeroStr, out long bits)) return "0";

            if (bits >= 1000000) // Si es mayor o igual a 1 Megabit
                return (bits / 1000000) + "M";

            if (bits >= 1000) // Si es mayor o igual a 1 Kilobit
                return (bits / 1000) + "k";

            return bits.ToString(); // Si es menor a 1k, lo deja igual
        }
        public string VerVelocidadQueue(string name)
        {
            string MaxLimit = string.Empty;
            try
            {
                Send("/queue/simple/print");
                Send("=.proplist=max-limit");// Esto ayuda a que el router no se pierda enviando datos extra
                Send("?name=" + name, true);
                foreach (string row in Read())
                {
                    if (row.StartsWith("!re"))
                    {
                        continue;
                    }
                    if (row.StartsWith("!done")) break;

                    if (row.StartsWith("="))
                    {
                        string[] parts = row.Split(new char[] { '=' }, 3);
                        if (parts.Length < 3) continue;

                        string key = parts[1];
                        string value = parts[2];

                        if (key == "max-limit")
                        {
                            if (value.Contains("/"))
                            {
                                string[] partes = value.Split('/');
                                return $"{FormatearNumero(partes[0])} / {FormatearNumero(partes[1])}";
                            }
                            return FormatearNumero(value);
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            return MaxLimit;
        }
        public List<Antenas> VerAntenas(string name, List<ListWirelessModel> ListWireless)
        {
            List<Antenas> listaFinal = new List<Antenas>();
            try
            {
                Send("/ip/firewall/address-list/print");
                Send("=.proplist=list,.id,address,comment,disabled", true);// Esto ayuda a que el router no se pierda enviando datos extra
                                                                      //}
                Antenas currentObj = null;
                List<string> respuesta = Read();
                bool objetoValido = true;
                foreach (string row in respuesta)
                {
                    if (row.StartsWith("!re"))
                    {
                        currentObj = new Antenas();
                        currentObj.comment = "Sin Comentario";
                        objetoValido = true;
                        continue;
                    }

                    if (!objetoValido && row.StartsWith("=")) continue;

                    if (row.StartsWith("!done") ||
                        row.StartsWith("!done") && (name != string.Empty && currentObj.comment.Contains(name))
                        ) break;

                    if (row.StartsWith("="))
                    {
                        string[] parts = row.Split(new char[] { '=' }, 3);
                        if (parts.Length < 3) continue;

                        string key = parts[1];
                        string value = parts[2];
                        //ListWireless
                        switch (key)
                        {
                            case "list": value = value.Replace("\r", "").Replace("\n", "").Trim();
                                if (value != "PERMITIDOS")
                                {
                                    objetoValido = false; // Marcamos que este registro no nos sirve
                                    currentObj = null;    // Limpiamos la referencia
                                }
                                break;
                            case ".id": currentObj.id = value; break;
                            case "comment":
                                currentObj.comment = value.Replace("\r", "").Replace("\n", "").Trim();
                                currentObj.idplan = string.Empty;
                                currentObj.velocidad = VerVelocidadQueue(value.Replace("\r", "").Replace("\n", "").Trim());
                                break;
                            case "address": currentObj.address = value;
                                if (currentObj != null && !string.IsNullOrEmpty(currentObj.address))
                                {
                                    bool coincide = ListWireless.Any(w => IpPerteneceARango(currentObj.address, w.Address));

                                    if (coincide)
                                    {
                                        // Evitar duplicados si el !re se procesa varias veces
                                        if (!listaFinal.Any(a => a.id == currentObj.id))
                                        {
                                            listaFinal.Add(currentObj);
                                        }
                                    }
                                }

                                break;
                            case "disabled": currentObj.estatus = value == "false" ? "Activo" : "Inactivo"; break;
                        }
                    }
                   
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en ver antenas: " + ex.Message);
            }
            return name != string.Empty ? listaFinal.Where(r=> r.comment == name).ToList():listaFinal;
        }
        private bool IpPerteneceARango(string ipDestino, string redReferencia)
        {
            try
            {
                string ipDest = ipDestino.Split('/')[0].Trim();
                string redRef = redReferencia.Trim();

                if (!redRef.Contains("/")) return ipDest == redRef;

                string[] partes = redRef.Split('/');
                byte[] bytesDest = System.Net.IPAddress.Parse(ipDest).GetAddressBytes();
                byte[] bytesRed = System.Net.IPAddress.Parse(partes[0]).GetAddressBytes();
                int cidr = int.Parse(partes[1]);

                // Construimos la máscara de red byte por byte (8 bits por byte)
                byte[] maskBytes = new byte[4];
                for (int i = 0; i < 4; i++)
                {
                    if (cidr >= 8)
                    {
                        maskBytes[i] = 0xFF;
                        cidr -= 8;
                    }
                    else if (cidr > 0)
                    {
                        maskBytes[i] = (byte)(0xFF << (8 - cidr));
                        cidr = 0;
                    }
                    else
                    {
                        maskBytes[i] = 0x00;
                    }
                }

                // Comparamos cada octeto aplicando la máscara
                for (int i = 0; i < 4; i++)
                {
                    if ((bytesDest[i] & maskBytes[i]) != (bytesRed[i] & maskBytes[i]))
                    {
                        return false; // En cuanto un octeto no coincide, fuera.
                    }
                }

                return true;
            }
            catch { return false; }
        }
        public bool ActualizarUsuarioPPP(string Id, string nombrePerfil, string Velocidad)
        {
            try
            {
                // Paso 1: Asegurarnos que el perfil existe
                if (!AsegurarPerfil(nombrePerfil, Velocidad)) return false;

                // Paso 2: Asignar el perfil al Secret del usuario
                Send("/ppp/secret/set");
                Send("=.id=" + Id); // Usamos el nombre como ID
                Send("=profile=" + nombrePerfil, true);

                foreach (string row in Read())
                {
                    if (row.StartsWith("!trap")) return false;
                    if (row.StartsWith("!done")) return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
        public bool AsegurarPerfil(string nombrePerfil, string velocidad)
        {
            string idEncontrado = string.Empty;
            bool existe = false;

            try
            {
                // --- PASO 1: BÚSQUEDA ---
                Send("/ppp/profile/print");
                Send("=.proplist=.id");
                Send("?name=" + nombrePerfil, true);

                // Leemos TODA la respuesta del print hasta el !done
                foreach (string row in Read())
                {
                    if (row.StartsWith("!re"))
                    {
                        existe = true;
                    }
                    else if (row.StartsWith("="))
                    {
                        string[] parts = row.Split(new char[] { '=' }, 3);
                        if (parts.Length >= 3 && parts[1] == ".id")
                        {
                            idEncontrado = parts[2];
                        }
                    }
                    else if (row.StartsWith("!done"))
                    {
                        break; // Salimos del foreach del print
                    }
                }

                // --- PASO 2: ACCIÓN (SET o ADD) ---
                if (existe && !string.IsNullOrEmpty(idEncontrado))
                {
                    Send("/ppp/profile/set");
                    Send("=.id=" + idEncontrado);
                    Send("=rate-limit=" + velocidad, true);
                }
                else
                {
                    Send("/ppp/profile/add");
                    Send("=name=" + nombrePerfil);
                    Send("=rate-limit=" + velocidad, true);
                }

                // --- PASO 3: CONFIRMACIÓN FINAL ---
                // Leemos la respuesta del SET o ADD
                foreach (string row in Read())
                {
                    if (row.StartsWith("!trap")) return false;
                    if (row.StartsWith("!done")) return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
        public bool SavePerfil(PlanModel Plan, PlanesAnidadosModel Anidado)
        {
            string idEncontrado = string.Empty;
            bool existe = false;

            try
            {
                if (Anidado.IdPlanInterno != string.Empty)
                {
                    Send("/ppp/profile/set");
                    Send("=.id=" + Anidado.IdPlanInterno);
                    Send("=name=" + Plan.Nombre);
                    Send("=rate-limit=" + Plan.Velocidad, true);
            
                    foreach (string row in Read())
                    {
                        if (row.StartsWith("!trap")) return false;
                        if (row.StartsWith("!done")) return true;
                    }
                }
                else
                {
                    Send("/ppp/profile/print");
                    Send("=.proplist=.id");
                    Send("?name=" + Plan.Nombre, true);
                    foreach (string row in Read())
                    {
                        if (row.StartsWith("!re"))
                        {
                            existe = true;
                        }
                        else if (row.StartsWith("="))
                        {
                            string[] parts = row.Split(new char[] { '=' }, 3);
                            if (parts.Length >= 3 && parts[1] == ".id")
                            {
                                idEncontrado = parts[2];
                            }
                        }
                        else if (row.StartsWith("!done"))
                        {
                            break; // Salimos del foreach del print
                        }
                    }

                    // --- PASO 2: ACCIÓN (SET o ADD) ---
                    if (existe && !string.IsNullOrEmpty(idEncontrado))
                    {
                        Send("/ppp/profile/set");
                        Send("=.id=" + idEncontrado);
                        Send("=name=" + Plan.Nombre);
                        Send("=rate-limit=" + Plan.Velocidad, true);
                    }
                    else
                    {
                        Send("/ppp/profile/add");
                        Send("=name=" + Plan.Nombre);
                        Send("=rate-limit=" + Plan.Velocidad, true);
                        Send("/ppp/profile/print");
                        Send("=.proplist=.id");
                        Send("?name=" + Plan.Nombre, true);
                        foreach (string row in Read())
                        {
                            if (row.StartsWith("!re"))
                            {
                                existe = true;
                            }
                            else if (row.StartsWith("="))
                            {
                                string[] parts = row.Split(new char[] { '=' }, 3);
                                if (parts.Length >= 3 && parts[1] == ".id")
                                {
                                    idEncontrado = parts[2];
                                }
                            }
                            else if (row.StartsWith("!done"))
                            {
                                break; // Salimos del foreach del print
                            }
                        }

                    }
                    AppRepository obj = new AppRepository();
                    PlanAnidadoModel plansave = new PlanAnidadoModel();
                    plansave.IdPlan = Plan.Id;
                    plansave.IdPlanInterno = idEncontrado;
                    plansave.IsAntena = Plan.IsAntena;
                    plansave.IdMikrotik = Anidado.IdMikrotik;
                    int guardado= obj.SavePlanAnidadoByMigracion(plansave).Result;
                    foreach (string row in Read())
                    {
                        if (row.StartsWith("!trap")) return false;
                        if (row.StartsWith("!done")) return true;
                    }
                }
               
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
        public List<LimiteModel> VerProfile()
        {
            List<LimiteModel> lista = new List<LimiteModel>();
            try
            {
                Send("/ppp/profile/print");
                Send("=.proplist=.id,name,rate-limit", true);
                LimiteModel obj = null;
                foreach (string row in Read())
                {
                    if (row.StartsWith("!re"))
                    {
                        obj = new LimiteModel();
                        lista.Add(obj);
                        continue;
                    }
                    if (row.StartsWith("!done")) break;

                    if (row.StartsWith("="))
                    {
                        string[] parts = row.Split(new char[] { '=' }, 3);
                        if (parts.Length < 3) continue;

                        string key = parts[1];
                        string value = parts[2];
                        if (key == ".id") obj.Id = value;
                        if (key == "name") obj.Name = value;
                        if (key == "rate-limit") obj.Velocidad = value;
                    }
                }
            }
            catch (Exception e)
            {

            }
            return lista;
        }
        public List<Fibra> VerFibra(string name)
        {
            List<Fibra> listaFinal = new List<Fibra>();
            try
            {
                var Listalimites = VerProfile();
                if (name == string.Empty)
                {
                    Send("/ppp/secret/print");
                    Send("=.proplist=.id,name,profile,remote-address,disabled", true);// Esto ayuda a que el router no se pierda enviando datos extra
                }
                if (name != string.Empty)
                {
                    Send("/ppp/secret/print");
                    Send("=.proplist=.id,name,profile,remote-address,disabled");// Esto ayuda a que el router no se pierda enviando datos extra
                    Send("?name=" + name, true);
                }
                Fibra currentObj = null;
                foreach (string row in Read())
                {
                    if (row.StartsWith("!re"))
                    {
                        currentObj = new Fibra();
                        listaFinal.Add(currentObj);
                        continue;
                    }

                    if (row.StartsWith("!done")) break;

                    if (row.StartsWith("="))
                    {
                        string[] parts = row.Split(new char[] { '=' }, 3);
                        if (parts.Length < 3) continue;

                        string key = parts[1];
                        string value = parts[2];

                        if (key == ".id") currentObj.id = value;
                        if (key == "name") currentObj.comment = value;
                        if (key == "remote-address") currentObj.address = value;
                        if (key == "disabled") currentObj.estatus = value == "false" ? "Activo" : "Inactivo";
                        if (key == "profile")
                        {
                            var perfil = Listalimites.FirstOrDefault(p => p.Name == value);
                            if (perfil != null)
                            { currentObj.idplan = perfil.Id;
                                currentObj.velocidad = perfil.Velocidad; }
                            else {
                                currentObj.idplan = string.Empty;
                                currentObj.velocidad = string.Empty; }
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            return listaFinal;
        }
        public List<Address> VerAddres()
        {
            List<Address> listaFinal = new List<Address>();
            try
            {
                // Enviamos el comando a la ruta de IP -> Address
                Send("/ip/address/print");
                Send("=.proplist=.id,address,comment,network,interface,actual-interface,disabled", true);// Esto ayuda a que el router no se pierda enviando datos extra
                Address currentObj = null;
                List<string> respuesta = Read();
                foreach (string row in respuesta)
                {
                    // Cada vez que aparece !re, es una nueva fila/registro
                    if (row.StartsWith("!re"))
                    {
                        currentObj = new Address();
                        currentObj.comment = "Sin Comentario"; // Valor por defecto
                        listaFinal.Add(currentObj);
                        continue;
                    }

                    if (row.StartsWith("!done")) break;

                    // Procesamos las propiedades del objeto actual
                    if (row.StartsWith("=") && currentObj != null)
                    {
                        string[] parts = row.Substring(1).Split(new char[] { '=' }, 2);
                        if (parts.Length < 2) continue;

                        string key = parts[0];
                        string value = parts[1];

                        switch (key)
                        {
                            case ".id": currentObj.id = value; break;
                            case "address": currentObj.address = value; break;
                            case "comment": currentObj.comment = value; break;
                            case "disabled":
                                currentObj.estatus = value == "false" ? "Activo" : "Inactivo";
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en VerAddres: " + ex.Message);
            }
            return listaFinal;
        }
        public bool CambiarEstatusAntena(string Id, string Estatus)
        {
            try
            {
                Send("/ip/firewall/address-list/set");
                Send("=.id=" + Id);
                if(Estatus == "Activo")
                    Send("=disabled=yes", true);
                else
                    Send("=disabled=no", true);

                List<string> respuesta = Read();
                // Si no hay errores (!trap), asumimos éxito
                return !respuesta.Any(r => r.Contains("!trap"));
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool CambiarEstatusQueues(string Name, string Estatus)
        {
            try
            {
                string Id = VerIdQueue(Name);
                Send("/queue/simple/set");
                Send("=.id=" + Id);
                if (Estatus == "Activo")
                    Send("=disabled=yes", true);
                else
                    Send("=disabled=no", true);

                List<string> respuesta = Read();
                return !respuesta.Any(r => r.Contains("!trap"));
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool CambiarEstatusFibra(string Id, string Estatus)
        {
            try
            {
                Send("/ppp/secret/set");
                Send("=.id=" + Id);
                if (Estatus == "Activo")
                    Send("=disabled=yes", true);
                else
                    Send("=disabled=no", true);

                List<string> respuesta = Read();

                // Si el router responde con !trap es que hubo un error (ej: el usuario no existe)
                return !respuesta.Any(r => r.Contains("!trap"));
            }
            catch (Exception ex)
            {
                 return false;
            }
        }
        public string VerIdQueue(string name)
        {
            string Id = string.Empty;
            try
            {
                Send("/queue/simple/print");
                Send("=.proplist=.id");// Esto ayuda a que el router no se pierda enviando datos extra
                Send("?name=" + name, true);
                foreach (string row in Read())
                {
                    if (row.StartsWith("!re"))
                    {
                        continue;
                    }
                    if (row.StartsWith("!done")) break;

                    if (row.StartsWith("="))
                    {
                        string[] parts = row.Split(new char[] { '=' }, 3);
                        if (parts.Length < 3) continue;

                        string key = parts[1];
                        string value = parts[2];

                        if (key == ".id")
                            return value;
                    }
                }
            }
            catch (Exception e)
            {

            }
            return Id;
        }
    }
}