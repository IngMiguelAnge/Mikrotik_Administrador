using Microsoft.Data.SqlClient;
using Mikrotik_Administrador.Model;
using System;
using System.Collections.Generic;


//using Mikrotik_Administrador.Model;
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
        public async Task<UserModel> GetUser(string Usuario, string Password)
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
                Id_TipoUsuario = Convert.IsDBNull(reader["Id_TipoUsuario"]) ? 0 : (int)reader["Id_TipoUsuario"],
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
        public async Task<bool> InsertandUpdateMikrotik(MikrotikModel obj)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("InsertandUpdateMikrotik", sql))
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
        public async Task<bool> InsertandUpdateUbicacionMikrotik(UbicacionModel obj)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("InsertandUpdateUbicacionMikrotik", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Direccion", obj.Direccion));
                        cmd.Parameters.Add(new SqlParameter("@Direccion_Oficial", obj.Direccion_Oficial));
                        cmd.Parameters.Add(new SqlParameter("@Latitud", obj.Latitud));  
                        cmd.Parameters.Add(new SqlParameter("@Longitud", obj.Longitud));
                        cmd.Parameters.Add(new SqlParameter("@Id_Mikrotik", obj.Id_Mikrotik));
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
        public async Task<UbicacionModel> GetUbicacionByIdMikrotik(int Id_Mikrotik)
        {
            UbicacionModel response = new UbicacionModel();
            List<UbicacionModel> list = new List<UbicacionModel>();
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("GetUbicacionByIdMikrotik", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Id_Mikrotik", Id_Mikrotik));
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
                Id_Mikrotik = (int)reader["Id_Mikrotik"],
            };
        }

        #endregion
        #region ActionsUsuariosGeneral
        public async Task<List<ListUsuariosGeneralModel>> GetUsuariosMikrotiksByName(string Nombre, int Id_Mikrotik)
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
                        cmd.Parameters.Add(new SqlParameter("@Id_Mikrotik", Id_Mikrotik));

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
                Nombre = (string)reader["Nombre"],
                Mikrotik = (string)reader["Mikrotik"],
            };
        }
        public async Task<bool> InsertandUpdateUsuariosGeneral(UsuariosGeneralModel obj)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("InsertAndUpdateUsuariosGeneral", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Id", obj.Id));
                        cmd.Parameters.Add(new SqlParameter("@Nombre", obj.Nombre));
                        cmd.Parameters.Add(new SqlParameter("@Id_Mikrotik", obj.Id_Mikrotik));
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
        private ListWirelessModel MapToWireless(SqlDataReader reader)
        {
            return new ListWirelessModel()
            {
                Id = (int)reader["Id"],
                Address = (string)reader["Address"],
                Network = (string)reader["Network"],
                Interface = (string)reader["Interface"],
                Actual_Interface = (string)reader["Actual_Interface"],
                Comment = (string)reader["Comment"],
                Mikrotik = (string)reader["Mikrotik"],
                Estatus = (string)reader["Estatus"],
            };
        }
        public async Task<bool> InsertandUpdateWireless(InsertListWirelessModel obj)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("InsertandUpdateWireless", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Address", obj.Address));
                        cmd.Parameters.Add(new SqlParameter("@Network", obj.Network));
                        cmd.Parameters.Add(new SqlParameter("@Interface", obj.Interface));
                        cmd.Parameters.Add(new SqlParameter("@Actual_Interface", obj.Actual_Interface));
                        cmd.Parameters.Add(new SqlParameter("@Comment", obj.Comment));
                        cmd.Parameters.Add(new SqlParameter("@Id_Mikrotik", obj.Id_Mikrotik));
                        cmd.Parameters.Add(new SqlParameter("@Estatus", obj.Estatus));
                        cmd.Parameters.Add(new SqlParameter("@Id_Interno", obj.Id_Interno));
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
        public async Task<bool> DesactivarWireless(int Id_Mikrotik)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(MikrotikConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("DesactivarWireless", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Id_Mikrotik", Id_Mikrotik));
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
