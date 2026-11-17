using System;

namespace Mikrotik_Administrador.Class
{
    public static class ConversorMoneda
    {
        public static string ConvertirALetras(decimal numero)
        {
            long parteEntera = (long)Math.Truncate(numero);
            int centavos = (int)Math.Round((numero - parteEntera) * 100);

            string moneda = parteEntera == 1 ? "PESO" : "PESOS";
            string letras = NumeroALetras(parteEntera);

            // Regla gramatical: "UN MILLÓN DE PESOS" vs "TRESCIENTOS PESOS"
            if (parteEntera > 0 && parteEntera % 1000000 == 0)
            {
                letras += " DE";
            }

            return $"{letras} {moneda} {centavos:00}/100 M.N.";
        }

        private static string NumeroALetras(long value)
        {
            if (value == 0) return "CERO";
            if (value < 0) return "MENOS " + NumeroALetras(Math.Abs(value));

            string num2Text = "";

            if (value == 1) num2Text = "UN";
            else if (value == 2) num2Text = "DOS";
            else if (value == 3) num2Text = "TRES";
            else if (value == 4) num2Text = "CUATRO";
            else if (value == 5) num2Text = "CINCO";
            else if (value == 6) num2Text = "SEIS";
            else if (value == 7) num2Text = "SIETE";
            else if (value == 8) num2Text = "OCHO";
            else if (value == 9) num2Text = "NUEVE";
            else if (value == 10) num2Text = "DIEZ";
            else if (value == 11) num2Text = "ONCE";
            else if (value == 12) num2Text = "DOCE";
            else if (value == 13) num2Text = "TRECE";
            else if (value == 14) num2Text = "CATORCE";
            else if (value == 15) num2Text = "QUINCE";
            else if (value < 20) num2Text = "DIECI" + NumeroALetras(value - 10);
            else if (value == 20) num2Text = "VEINTE";
            else if (value < 30)
            {
                // Acentos correctos para 21, 22, 23 y 26
                long unidad = value - 20;
                if (unidad == 1) num2Text = "VEINTIÚN";
                else if (unidad == 2) num2Text = "VEINTIDÓS";
                else if (unidad == 3) num2Text = "VEINTITRÉS";
                else if (unidad == 6) num2Text = "VEINTISÉIS";
                else num2Text = "VEINTI" + NumeroALetras(unidad);
            }
            else if (value == 30) num2Text = "TREINTA";
            else if (value == 40) num2Text = "CUARENTA";
            else if (value == 50) num2Text = "CINCUENTA";
            else if (value == 60) num2Text = "SESENTA";
            else if (value == 70) num2Text = "SETENTA";
            else if (value == 80) num2Text = "OCHENTA";
            else if (value == 90) num2Text = "NOVENTA";
            else if (value < 100) num2Text = NumeroALetras((value / 10) * 10) + " Y " + NumeroALetras(value % 10);
            else if (value == 100) num2Text = "CIEN";
            else if (value < 200) num2Text = "CIENTO " + NumeroALetras(value - 100);
            else if (value == 200 || value == 300 || value == 400 || value == 600 || value == 800)
                num2Text = NumeroALetras(value / 100) + "CIENTOS";
            else if (value == 500) num2Text = "QUINIENTOS";
            else if (value == 700) num2Text = "SETECIENTOS";
            else if (value == 900) num2Text = "NOVECIENTOS";
            else if (value < 1000) num2Text = NumeroALetras((value / 100) * 100) + " " + NumeroALetras(value % 100);
            else if (value == 1000) num2Text = "MIL";
            else if (value < 2000) num2Text = "MIL " + NumeroALetras(value % 1000);
            else if (value < 1000000)
            {
                num2Text = NumeroALetras(value / 1000) + " MIL";
                if ((value % 1000) > 0) num2Text += " " + NumeroALetras(value % 1000);
            }
            else if (value == 1000000) num2Text = "UN MILLÓN";
            else if (value < 2000000) num2Text = "UN MILLÓN " + NumeroALetras(value % 1000000);
            else if (value < 1000000000000)
            {
                num2Text = NumeroALetras(value / 1000000) + " MILLONES";
                if ((value % 1000000) > 0) num2Text += " " + NumeroALetras(value % 1000000);
            }

            return num2Text;
        }
    }
}