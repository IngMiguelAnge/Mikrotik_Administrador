namespace Mikrotik_Administrador.Model
{
    public class InsertListWirelessModel
    {
        public string Address { get; set; }
        public string Network { get; set; }
        public string Interface { get; set; }
        public string Actual_Interface { get; set; }
        public string Comment { get; set; }
        public int Id_Mikrotik { get; set; }
        public string Disabled { get; set; }
        public string Id_Interno { get; set; } //Es el id que da el mikrotik real
    }
}
