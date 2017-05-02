using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructuresURL_3._0;

namespace Proyecto01
{
    class TimeDate : IStringParseable<TimeDate>, IComparable<TimeDate>
    {
        public DateTime value;
        public string DEFAULT_FORMAT_
        {
            get
            {
                return "dd/MM/yyyy";
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
                return 10;
            }
        }

        public int CompareTo(TimeDate other)
        {
            if (other == null)
                return 1;

            return value.CompareTo(other.value);
        }

        public TimeDate ParseToObjectType(string str)
        {
            TimeDate timeDateInstance = new TimeDate();
            timeDateInstance.value = DateTime.Parse(str);

            return timeDateInstance;

        }

        public string ParseToString(TimeDate obj)
        {
            return obj.value.ToString("dd/MM/yyyy");
        }
        public string ParseToString()
        {
            return value.ToString("dd/MM/yyyy");
        }
    }
}
