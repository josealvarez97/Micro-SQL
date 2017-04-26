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
        string IStringParseable<Int>.DEFAULT_FORMAT_
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        string IStringParseable<Int>.DEFAULT_MIN_VAL_FORMAT
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        int IStringParseable<Int>.objectLength
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        int IComparable<Int>.CompareTo(Int other)
        {
            throw new NotImplementedException();
        }

        Int IStringParseable<Int>.ParseToObjectType(string str)
        {
            throw new NotImplementedException();
        }

        string IStringParseable<Int>.ParseToString(Int obj)
        {
            throw new NotImplementedException();
        }
    }
}
