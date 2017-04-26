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
        string IStringParseable<Varchar>.DEFAULT_FORMAT_
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        string IStringParseable<Varchar>.DEFAULT_MIN_VAL_FORMAT
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        int IStringParseable<Varchar>.objectLength
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        int IComparable<Varchar>.CompareTo(Varchar other)
        {
            throw new NotImplementedException();
        }

        Varchar IStringParseable<Varchar>.ParseToObjectType(string str)
        {
            throw new NotImplementedException();
        }

        string IStringParseable<Varchar>.ParseToString(Varchar obj)
        {
            throw new NotImplementedException();
        }
    }
}
