using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService.HelpClass
{
    class StringGen
    {

        public static string StringGenName()
        {
            string nameSymbol = string.Empty;

            for (int i = 65; i < 91; i++)
            {
                nameSymbol += (char)i;
            }
            for (int i = 97; i < 123; i++)
            {
                nameSymbol += (char)i;
            }
            for (int i = 1040; i < 1104; i++)
            {
                nameSymbol += (char)i;
            }
            nameSymbol += "-";

            return nameSymbol;
        }

        public static string StringGenEmail()
        {
            string emailSymbol = string.Empty;

            for (int i = 65; i < 91; i++)
            {
                emailSymbol += (char)i;
            }
            for (int i = 97; i < 123; i++)
            {
                emailSymbol += (char)i;
            }

            for (int i = 0; i < 10; i++)
            {
                emailSymbol += i.ToString();
            }

            emailSymbol += "_";
            emailSymbol += "@";
            emailSymbol += ".";

            return emailSymbol;
        }

        public static string StringGenPhone()
        {
            string phoneSymbol = string.Empty;

            for (int i = 0; i < 10; i++)
            {
                phoneSymbol += i.ToString();
            }

            phoneSymbol += "+-()";

            return phoneSymbol;
        }

    }
}
