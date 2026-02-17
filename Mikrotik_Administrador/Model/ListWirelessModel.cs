namespace Mikrotik_Administrador.Model
{
    public class ListWirelessModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Network { get; set; }
        public string Interface { get; set; }
        public string Actual_Interface { get; set; }
        public string Comment { get; set; }
        public string Mikrotik { get; set; }
        public string Disabled { get; set; } //Es el estatus del mikrotik
        public string Estatus { get; set; }//Es el estatus para saber si aun existe en el mikrotik
    }
}
