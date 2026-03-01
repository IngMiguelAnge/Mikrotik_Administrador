using Mikrotik_Administrador.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

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
        public void Close()
        {
            connection.Close();
            con.Close();
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

            // El CCR2116 es una bestia multinúcleo; a veces tarda en "contestar"
            // Vamos a esperar hasta 2 segundos (40 intentos de 50ms)
            int waitAttempts = 40;
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
        public string EncodePassword(string Password, string hash)
        {
            byte[] hash_byte = new byte[hash.Length / 2];
            for (int i = 0; i <= hash.Length - 2; i += 2)
            {
                hash_byte[i / 2] = Byte.Parse(hash.Substring(i, 2), System.Globalization.NumberStyles.HexNumber);
            }
            byte[] heslo = new byte[1 + Password.Length + hash_byte.Length];
            heslo[0] = 0;
            Encoding.ASCII.GetBytes(Password.ToCharArray()).CopyTo(heslo, 1);
            hash_byte.CopyTo(heslo, 1 + Password.Length);

            Byte[] hotovo;
            System.Security.Cryptography.MD5 md5;

            md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();

            hotovo = md5.ComputeHash(heslo);

            //Convert encoded bytes back to a 'readable' string
            string navrat = "";
            foreach (byte h in hotovo)
            {
                navrat += h.ToString("x2");
            }
            return navrat;
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

                return respuesta.Any(s => s.Contains("!done"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error crítico: " + ex.Message);
                return false;
            }
        }
        public List<UsuariosModel> VerUsuarios(string name)
        {
            List<UsuariosModel> obj = new List<UsuariosModel>();
            try
            {
                if (name == string.Empty)
                    Send("/ip/hotspot/user/getall", true);
                if (name != string.Empty)
                {
                    Send("/ip/hotspot/user/print");
                    Send("?name=" + name, true);
                }
                foreach (string row in Read())
                {
                    string gets = string.Empty;
                    UsuariosModel objp = new UsuariosModel();
                    try
                    {
                        gets = row.Split(new string[] { "!re=.id=*" }, StringSplitOptions.None)[1];
                        objp.id = gets.Split(new string[] { "=name=" }, StringSplitOptions.None)[0];

                        string[] array = gets.Split('=');
                        int contador = 1;
                        while (contador < array.Count())
                        {
                            switch (array[contador])
                            {
                                case "name":
                                    objp.name = array[contador + 1];
                                    contador += 1;
                                    break;
                            }
                            contador += 1;
                        }
                        obj.Add(objp);
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            catch (Exception e)
            {

            }
            return obj;
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
        public string VerQueue(string name)
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

                        if (key == "max-limit") {
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
        public List<Antenas> VerAntenas(string name)
        {
            List<Antenas> listaFinal = new List<Antenas>();
            try
            {
               
                if (name == string.Empty)
                {
                    Send("/ip/firewall/address-list/print");
                    Send("=.proplist=.id,address,comment,disabled", true);// Esto ayuda a que el router no se pierda enviando datos extra
                }

                if (name != string.Empty)
                {
                    Send("/ip/firewall/address-list/print");
                    Send("=.proplist=.id,address,comment,disabled");// Esto ayuda a que el router no se pierda enviando datos extra
                    Send("?comment=" + name, true);                   
                }
                Antenas currentObj = null;
                string ultimoComentarioEncontrado = "Sin Comentario";

                foreach (string row in Read())
                {
                    if (row.StartsWith("!re"))
                    {
                        currentObj = new Antenas();
                        // IMPORTANTE: Primero asumimos que hereda el comentario anterior
                        currentObj.comment = ultimoComentarioEncontrado;
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
                        if (key == "comment")
                        {
                            // Si la IP trae su propio comentario, actualizamos el "arrastre"
                            currentObj.comment = value;
                            ultimoComentarioEncontrado = value;
                            currentObj.maxlimit = VerQueue(value);
                        }
                        if (key == "address") currentObj.address = value;
                        if (key == "disabled") currentObj.estatus = value == "false" ? "Activo" : "Inactivo";                   
                    }
                }
            }
            catch (Exception e)
            {

            }
            return listaFinal;
        }
        public List<Fibra> VerFibra(string name)
        {
            List<Fibra> listaFinal = new List<Fibra>();
            try
            {
                if (name == string.Empty)
                {
                    Send("/ppp/secret/print");
                    Send("=.proplist=.id,name,remote-address,disabled", true);// Esto ayuda a que el router no se pierda enviando datos extra
                }
                if (name != string.Empty)
                {
                    Send("/ppp/secret/print");
                    Send("=.proplist=.id,name,remote-address,disabled");// Esto ayuda a que el router no se pierda enviando datos extra
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
                string ultimoComentarioEncontrado = "Sin Comentario";

                // El Read() debe capturar la ráfaga completa
                foreach (string row in Read())
                {
                    if (row.StartsWith("!re"))
                    {
                        currentObj = new Address();
                        // IMPORTANTE: Primero asumimos que hereda el comentario anterior
                        currentObj.comment = ultimoComentarioEncontrado;
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
                        if (key == "address") currentObj.address = value;
                        if (key == "comment")
                        {
                            // Si la IP trae su propio comentario, actualizamos el "arrastre"
                            currentObj.comment = value;
                            ultimoComentarioEncontrado = value;
                        }
                        //if (key == "network") currentObj.network = value;
                        //if (key == "interface") currentObj.@interface = value;
                        //if (key == "actual-interface") currentObj.actual_interface = value;
                        if (key == "disabled") currentObj.estatus = value == "false"? "Activo": "Inactivo";
                    }
                }
            }
            catch (Exception e)
            {

            }
            return listaFinal;
        }
    }
}