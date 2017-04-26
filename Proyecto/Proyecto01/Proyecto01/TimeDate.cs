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
        string IStringParseable<TimeDate>.DEFAULT_FORMAT_
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        string IStringParseable<TimeDate>.DEFAULT_MIN_VAL_FORMAT
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        int IStringParseable<TimeDate>.objectLength
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        int IComparable<TimeDate>.CompareTo(TimeDate other)
        {
            throw new NotImplementedException();
        }

        TimeDate IStringParseable<TimeDate>.ParseToObjectType(string str)
        {
            throw new NotImplementedException();
        }

        string IStringParseable<TimeDate>.ParseToString(TimeDate obj)
        {
            throw new NotImplementedException();
        }
    }
}
