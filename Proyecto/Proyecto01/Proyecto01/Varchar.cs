using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructuresURL_3._0;

namespace Proyecto01
{
    class Varchar : IStringParseable<Varchar>, IComparable<Varchar>
    {
        public string value { get; set; }

        public Varchar()
        {
            value = "";
        }

        public string DEFAULT_FORMAT_
        {
            get
            {
                string defaultFormat = "";

                for (int i = 0; i < 100; i++)
                {
                    defaultFormat += "~";
                }
                return defaultFormat;
            }
        }


        // Es para los punteros, para cuando no apunten a nada...
        public string DEFAULT_MIN_VAL_FORMAT
        {
            get
            {                
                return int.MinValue.ToString();
            }
        }

        public int objectLength
        {
            get
            {
                return 100;
            }
        }

        public int CompareTo(Varchar other)
        {
            if (other == null)
                return 1;

            return value.CompareTo(other.value);
        }

        public Varchar ParseToObjectType(string str)
        {
            Varchar varcharInstance = new Varchar();
            varcharInstance.value = str;

            return varcharInstance;
        }

        public string ParseToString(Varchar obj)
        {
            string stringOutput = obj.value;

            while(stringOutput.Length != 100)
            {
                stringOutput = "~" + stringOutput;
            }

            return stringOutput;
        }

        public string ParseToString()
        {
            string stringOutput = value;

            while (stringOutput.Length != 100)
            {
                stringOutput = "~" + stringOutput;
            }

            return stringOutput;
        }
    }
}
