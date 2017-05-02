using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructuresURL_3._0;

namespace Proyecto01
{
    class Int : IStringParseable<Int>, IComparable<Int>
    {

        public int value { get; set; }

        public string DEFAULT_FORMAT_
        {
            get
            {
                return "00000000000";
            }
        }

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
                return 11;
            }
        }

        public int CompareTo(Int other)
        {
            if (other == null)
                return 1;

            return value.CompareTo(other.value);
        }

        public Int ParseToObjectType(string str)
        {
            Int intInstance = new Int();
            intInstance.value = int.Parse(str);
            return intInstance;
        }

        public string ParseToString(Int obj)
        {
            if (obj.value != int.MinValue)
                return obj.value.ToString(DEFAULT_FORMAT_);
            else
                return int.MinValue.ToString();
        }

        public string ParseToString()
        {
            if (value != int.MinValue)
                return value.ToString(DEFAULT_FORMAT_);
            else
                return int.MinValue.ToString();
        }
    }
}
