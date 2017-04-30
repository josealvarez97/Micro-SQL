using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructuresURL_3._0;
namespace Proyecto01
{
    class Key : IStringParseable<Key>, IComparable<Key>
    {
        string IStringParseable<Key>.DEFAULT_FORMAT_
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        string IStringParseable<Key>.DEFAULT_MIN_VAL_FORMAT
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        int IStringParseable<Key>.objectLength
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        int IComparable<Key>.CompareTo(Key other)
        {
            throw new NotImplementedException();
        }

        Key IStringParseable<Key>.ParseToObjectType(string str)
        {
            throw new NotImplementedException();
        }

        string IStringParseable<Key>.ParseToString(Key obj)
        {
            throw new NotImplementedException();
        }
    }
}
