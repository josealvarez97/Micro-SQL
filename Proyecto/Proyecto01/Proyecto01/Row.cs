using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructuresURL_3._0;

namespace Proyecto01
{
    class Row : IStringParseable<Row>
    {

        Dictionary<string, Int> intColumnsDictionary;
        Dictionary<string, Varchar> varcharColumnsDictionary;
        Dictionary<string, TimeDate> timeDateColumnsDictionary;

        string IStringParseable<Row>.DEFAULT_FORMAT_
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        string IStringParseable<Row>.DEFAULT_MIN_VAL_FORMAT
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        int IStringParseable<Row>.objectLength
        {
            get
            {
                return intColumnsDictionary.Count + varcharColumnsDictionary.Count + timeDateColumnsDictionary.Count;
            }
        }

        Row IStringParseable<Row>.ParseToObjectType(string str)
        {
            throw new NotImplementedException();
        }

        string IStringParseable<Row>.ParseToString(Row obj)
        {
            throw new NotImplementedException();
        }
    }
}
