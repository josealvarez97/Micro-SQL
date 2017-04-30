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
        
        public Dictionary<string, Int> intColumnsDictionary = new Dictionary<string, Int>();
        public Dictionary<string, Varchar> varcharColumnsDictionary;
        public Dictionary<string, TimeDate> timeDateColumnsDictionary;

        public int objectLength => throw new NotImplementedException();

        public string DEFAULT_FORMAT_ => throw new NotImplementedException();

        public string DEFAULT_MIN_VAL_FORMAT => throw new NotImplementedException();

        public Row ParseToObjectType(string str)
        {
            throw new NotImplementedException();
        }

        public string ParseToString(Row obj)
        {
            throw new NotImplementedException();
        }




        public int CountColums()
        {
            return intColumnsDictionary.Count + varcharColumnsDictionary.Count + timeDateColumnsDictionary.Count;
        }
    }
}
