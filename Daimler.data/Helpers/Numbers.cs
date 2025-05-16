using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.Helpers
{
    public class Numbers
    {
        public string DecimalToWords(decimal number)
        {
            var numbers = number.ToString().Split(".");

            return NumberToWords(Convert.ToInt32(numbers[0])) + " " + ((numbers[1].ToString().Length < 2) ? "0" + numbers[1].ToString() : numbers[1].ToString()) + "/100 M.N.";
        }

        public string NumberToWords(int number)
        {
            try
            {
                if (number == 0)
                    return "CERO";

                if (number < 0)
                    throw new Exception("Valor incorrecto");

                string words = "";

                if (number == 1000000)
                {
                    words += NumberToWords(number / 1000000) + "UN MILLON";
                    number %= 1000000;
                }

                if ((number / 1000000) > 0)
                {
                    words += NumberToWords(number / 1000000) + " MILLONES ";
                    number %= 1000000;
                }

                if (number == 1000)
                {
                    words += NumberToWords(number / 1000) + "MIL ";
                    number %= 1000;
                }

                if ((number / 1000) > 0)
                {
                    words += NumberToWords(number / 1000) + " MIL ";
                    number %= 1000;
                }

                if (number == 100)
                {
                    words += "CIEN ";
                }

                if (number > 100 && number < 200)
                {
                    words += "CIENTO ";
                    number %= 100;
                }

                if ((number / 100) > 1)
                {
                    if ((number / 100) == 5)
                    {
                        words += "QUINIENTOS ";
                    }
                    else if ((number / 100) == 7)
                    {
                        words += "SETECIENTOS ";
                    }
                    else if ((number / 100) == 9)
                    {
                        words += "NOVECIENTOS ";
                    }
                    else
                    {
                        words += NumberToWords(number / 100) + "CIENTOS ";
                    }

                    number %= 100;
                }

                if (number > 0 && number < 100)
                {
                    var unitsMap = new[] { "CERO", "UNO", "DOS", "TRES", "CUATRO", "CINCO", "SEIS", "SIETE", "OCHO", "NUEVE", "DIEZ", "ONCE", "DOCE", "TRECE", "CATORCE", "QUINCE", "DIECISEIS", "DIECISIETE", "DIECIOCHO", "DIECINUEVE" };
                    var tensMap = new[] { "CERO", "DIEZ", "VENTI", "TREINTA", "CUARENTA", "CINCUENTA", "SESENTA", "SETENTA", "OCHENTA", "NOVENTA" };

                    if (number < 20)
                    {
                        words += unitsMap[number];
                    }
                    else
                    {
                        if ((number / 10) >= 3)
                            words += tensMap[number / 10];

                        if ((number % 10) > 0)
                        {
                            words += " Y ";
                            words += unitsMap[number % 10];
                        }
                    }
                }

                return words;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
