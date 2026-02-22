namespace Mikrotik_Administrador.Class
{
    public class Address
    {
        public string id { get; set; }
        public string address { get; set; } // Ejemplo: 192.168.88.1/24
        public string network { get; set; } // Ejemplo: 192.168.88.0
        public string @interface { get; set; } // Ejemplo: ether1
        public string actual_interface { get; set; }
        public string comment { get; set; }
        public string estatus { get; set; }
    }
}
