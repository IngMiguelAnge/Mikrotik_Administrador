using Microsoft.Data.SqlClient;
using Mikrotik_Administrador.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
namespace Mikrotik_Administrador.Data
{
    public class AppRepository
    {
        public string MikrotikConnection { get; set; }
        public AppRepository(bool isUnitOfWork = false)
        {
             MikrotikConnection = "Data Source=.;Initial Catalog=Mikrotiks;User ID=sa;Password=admin123;Trust Server Certificate=True";
        }
        public void Dispose()
        {
            GC.Collect();
        }
        #region ActionsUsers
        public async Task<UserModel> GetUserbyNameAndPassword(string Usuario, string Password)
        {
            UserModel response = new UserModel();
            List<UserModel> list = new List<UserModel>();
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("GetUserbyNameAndPassword", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                        cmd.Parameters.Add(new SqlParameter("@Password", Password));
                        await sql.OpenAsync().ConfigureAwait(false);
                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                list.Add(MapToUser(reader));
                            }
                            response = list.Count()> 0 ? list[0]: null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = null;
            }
            return response;
        }
        private UserModel MapToUser(SqlDataReader reader)
        {
            return new UserModel()
            {
                Id = (int)reader["Id"],
                Usuario = (string)reader["Usuario"],
                Password = (string)reader["Password"],
                Estatus = (bool)reader["Estatus"],
                IdTipoUsuario = Convert.IsDBNull(reader["IdTipoUsuario"]) ? 0 : (int)reader["IdTipoUsuario"],
            };
        }

        #endregion
        #region ActionsMikrotiks
        public async Task<bool> DesactivarMikrotik(int Id)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("DesactivarMikrotik", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Id", Id));
                        await sql.OpenAsync().ConfigureAwait(false);
                        await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> SaveMikrotik(MikrotikModel obj)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("SaveMikrotik", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Id", obj.Id));
                        cmd.Parameters.Add(new SqlParameter("@Nombre", obj.Nombre));
                        cmd.Parameters.Add(new SqlParameter("@IP", obj.IP));
                        cmd.Parameters.Add(new SqlParameter("@Port", obj.Port));
                        cmd.Parameters.Add(new SqlParameter("@Usuario", obj.Usuario));
                        cmd.Parameters.Add(new SqlParameter("@Password", obj.Password));
                        cmd.Parameters.Add(new SqlParameter("@Estatus", obj.Estatus));
                        cmd.Parameters.Add(new SqlParameter("@Limite_Alcanzado", obj.Limite_Alcanzado));
                        await sql.OpenAsync().ConfigureAwait(false);
                        await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<MikrotikModel> GetMikrotikById(int Id)
        {
            MikrotikModel response = new MikrotikModel();
            List<MikrotikModel> list = new List<MikrotikModel>();
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("GetMikrotikById", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Id", Id));
                        await sql.OpenAsync().ConfigureAwait(false);
                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                list.Add(MapToMikrotik(reader));
                            }
                            response = list.Count() > 0 ? list[0] : null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = null;
            }
            return response;
        }
        public async Task<List<ListMikrotikModel>> GetMikrotiks()
        {
            List<ListMikrotikModel> list = new List<ListMikrotikModel>();
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("GetMikrotiks", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        await sql.OpenAsync().ConfigureAwait(false);
                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                list.Add(MapToMikrotiks(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return list;
        }
        private MikrotikModel MapToMikrotik(SqlDataReader reader)
        {
            return new MikrotikModel()
            {
                Id = (int)reader["Id"],
                Nombre = (string)reader["Nombre"],
                IP = (string)reader["IP"],
                Port = (string)reader["Port"],
                Usuario = (string)reader["Usuario"],
                Password = (string)reader["Password"],
                Estatus = (bool)reader["Estatus"],
                Limite_Alcanzado = (bool)reader["Limite_Alcanzado"],
            };
        }
        private ListMikrotikModel MapToMikrotiks(SqlDataReader reader)
        {
            return new ListMikrotikModel()
            {
                Id = (int)reader["Id"],
                Nombre = (string)reader["Nombre"],
                IP = (string)reader["IP"],
                Estatus = (string)reader["Estatus"],
                Limite_Alcanzado = (string)reader["Limite"],
            };
        }

        #endregion
        #region ActionsUbicacion
        public async Task<bool> SaveUbicacion(UbicacionModel obj)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("SaveUbicacion", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Direccion", obj.Direccion));
                        cmd.Parameters.Add(new SqlParameter("@Direccion_Oficial", obj.Direccion_Oficial));
                        cmd.Parameters.Add(new SqlParameter("@Latitud", obj.Latitud));  
                        cmd.Parameters.Add(new SqlParameter("@Longitud", obj.Longitud));
                        cmd.Parameters.Add(new SqlParameter("@IdMikrotik", obj.IdMikrotik));
                        cmd.Parameters.Add(new SqlParameter("@IdUsuario", obj.IdUsuario));
                        await sql.OpenAsync().ConfigureAwait(false);
                        await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<UbicacionModel> GetUbicacionByIds(int IdMikrotik, int IdUsuario)
        {
            UbicacionModel response = new UbicacionModel();
            List<UbicacionModel> list = new List<UbicacionModel>();
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("GetUbicacionByIds", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@IdMikrotik", IdMikrotik));
                        cmd.Parameters.Add(new SqlParameter("@IdUsuario", IdUsuario));
                        await sql.OpenAsync().ConfigureAwait(false);
                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                list.Add(MapToUbicacion(reader));
                            }
                            response = list.Count() > 0 ? list[0] : null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = null;
            }
            return response;
        }
        private UbicacionModel MapToUbicacion(SqlDataReader reader)
        {
            return new UbicacionModel()
            {
                Id = (int)reader["Id"],
                Direccion = (string)reader["Direccion"],
                Direccion_Oficial = (string)reader["Direccion_Oficial"],
                Latitud = (string)reader["Latitud"],    
                Longitud = (string)reader["Longitud"],
                IdMikrotik = (int)reader["IdMikrotik"],
                IdUsuario = (int)reader["IdUsuario"],
            };
        }

        #endregion
        #region ActionsUsuariosGeneral
        public async Task<List<ListUsuariosGeneralModel>> GetUsuariosMikrotiksByName(string Nombre, int IdMikrotik)
        {
            List<ListUsuariosGeneralModel> list = new List<ListUsuariosGeneralModel>();
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("GetUsuariosMikrotiksByName", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                        cmd.Parameters.Add(new SqlParameter("@IdMikrotik", IdMikrotik));

                        await sql.OpenAsync().ConfigureAwait(false);
                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                list.Add(MapToUsuariosGeneral(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return list;
        }
        private ListUsuariosGeneralModel MapToUsuariosGeneral(SqlDataReader reader)
        {
            return new ListUsuariosGeneralModel()
            {
                Id = (int)reader["Id"],
                IdInterno = (string)reader["IdInterno"],
                Tipo = (string)reader["Tipo"],
                Nombre = (string)reader["Nombre"],
                Address = (string)reader["Address"],
                Estatus = (string)reader["Estatus"],
                IdMikrotik = (int)reader["IdMikrotik"],
                Mikrotik = (string)reader["Mikrotik"],
                IdCliente = Convert.IsDBNull(reader["IdCliente"]) ? (int?)null : (int)reader["IdCliente"],
                Cliente = Convert.IsDBNull(reader["Cliente"]) ? string.Empty : (string)reader["Cliente"],
            };
        }
        public async Task<bool> SaveUsuariosGeneral(UsuariosGeneralModel obj)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("SaveUsuariosGeneral", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Nombre", obj.Nombre));
                        cmd.Parameters.Add(new SqlParameter("@Address", obj.Address));
                        cmd.Parameters.Add(new SqlParameter("@IdMikrotik", obj.IdMikrotik));
                        cmd.Parameters.Add(new SqlParameter("@Antena", obj.Antena));
                        cmd.Parameters.Add(new SqlParameter("@IdInterno", obj.IdInterno));
                        cmd.Parameters.Add(new SqlParameter("@Estatus", obj.Estatus));
                        await sql.OpenAsync().ConfigureAwait(false);
                        await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
        #region ActionsWireless
        public async Task<List<ListWirelessModel>> GetWireless()
        {
            List<ListWirelessModel> list = new List<ListWirelessModel>();
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("GetWireless", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        await sql.OpenAsync().ConfigureAwait(false);
                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                list.Add(MapToWireless(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return list;
        }
        public async Task<List<ListWirelessModel>> GetWirelessbyIdMikrotik(int IdMikrotik)
        {
            List<ListWirelessModel> list = new List<ListWirelessModel>();
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("GetWirelessbyIdMikrotik", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@IdMikrotik", IdMikrotik));
                        await sql.OpenAsync().ConfigureAwait(false);
                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                list.Add(MapToWireless(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return list;
        }
        private ListWirelessModel MapToWireless(SqlDataReader reader)
        {
            return new ListWirelessModel()
            {
                Id = (int)reader["Id"],
                Address = (string)reader["Address"],
                Comment = (string)reader["Comment"],
                Mikrotik = (string)reader["Mikrotik"],
                Estatus = (string)reader["Estatus"],
            };
        }
        public async Task<bool> SaveWireless(InsertListWirelessModel obj)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("SaveWireless", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Address", obj.Address));
                        cmd.Parameters.Add(new SqlParameter("@Comment", obj.Comment));
                        cmd.Parameters.Add(new SqlParameter("@IdMikrotik", obj.IdMikrotik));
                        cmd.Parameters.Add(new SqlParameter("@Estatus", obj.Estatus));
                        cmd.Parameters.Add(new SqlParameter("@IdInterno", obj.IdInterno));
                        await sql.OpenAsync().ConfigureAwait(false);
                        await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DesactivarWireless(int IdMikrotik)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("DesactivarWireless", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@IdMikrotik", IdMikrotik));
                        await sql.OpenAsync().ConfigureAwait(false);
                        await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion
        #region Clientes
        public async Task<bool> SaveClienteInGeneral(int IdUsuario ,string Nombre)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("SaveClienteInGeneral", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@IdUsuario", IdUsuario));
                        cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                        await sql.OpenAsync().ConfigureAwait(false);
                        await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<List<ListClientesModel>> GetClientesSinServicios()
        {
            List<ListClientesModel> list = new List<ListClientesModel>();
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("GetClientesSinServicios", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        await sql.OpenAsync().ConfigureAwait(false);
                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                list.Add(MapToClientes(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return list;
        }
        private ListClientesModel MapToClientes(SqlDataReader reader)
        {
            return new ListClientesModel()
            {
                Id = (int)reader["Id"],
                Nombre = (string)reader["Nombre"],
                Estatus = (string)reader["Estatus"],
                TotalServicios= Convert.IsDBNull(reader["TotalServicios"]) ? 0 : (int)reader["TotalServicios"],
            };
        }
        public async Task<List<ListClientesModel>> GetClientesbyName(string Nombre, int IdMikrotik)
        {
            List<ListClientesModel> list = new List<ListClientesModel>();
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("GetClientesbyName", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                        cmd.Parameters.Add(new SqlParameter("@IdMikrotik", IdMikrotik));

                        await sql.OpenAsync().ConfigureAwait(false);
                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                list.Add(MapToClientes(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return list;
        }
        public async Task<ClienteModel> GetClienteById(int Id)
        {
            ClienteModel response = new ClienteModel();
            List<ClienteModel> list = new List<ClienteModel>();
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("GetClienteById", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Id", Id));
                        await sql.OpenAsync().ConfigureAwait(false);
                        using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                list.Add(MapToCliente(reader));
                            }
                            response = list.Count() > 0 ? list[0] : null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = null;
            }
            return response;
        }
        private ClienteModel MapToCliente(SqlDataReader reader)
        {
            return new ClienteModel()
            {
                Id = (int)reader["Id"],
                Nombre = (string)reader["Nombre"],
                Telefono1 = Convert.IsDBNull(reader["Telefono1"]) ? string.Empty : (string)reader["Telefono1"],
                Telefono2 = Convert.IsDBNull(reader["Telefono2"]) ? string.Empty : (string)reader["Telefono2"],
                Correo = Convert.IsDBNull(reader["Correo"]) ? string.Empty : (string)reader["Correo"],
            };
        }
        public async Task<bool> SaveCliente(ClienteModel Cliente)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("SaveCliente", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Id", Cliente.Id));
                        cmd.Parameters.Add(new SqlParameter("@Nombre", Cliente.Nombre));
                        cmd.Parameters.Add(new SqlParameter("@Correo", Cliente.Correo));
                        cmd.Parameters.Add(new SqlParameter("@Telefono1", Cliente.Telefono1));
                        cmd.Parameters.Add(new SqlParameter("@Telefono2", Cliente.Telefono2));
                        await sql.OpenAsync().ConfigureAwait(false);
                        await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> UpdateEstatusCliente(int Id)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateEstatusCliente", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Id", Id));
                        await sql.OpenAsync().ConfigureAwait(false);
                        await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
