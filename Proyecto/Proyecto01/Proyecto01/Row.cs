using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructuresURL_3._0;

namespace Proyecto01
{

    enum ColumnType { INT, VARCHAR, TIMEDATE}
    class Row : IStringParseable<Row>
    {
        
        public Dictionary<string, Int> intColumnsDictionary;
        public Dictionary<string, Varchar> varcharColumnsDictionary;
        public Dictionary<string, TimeDate> timeDateColumnsDictionary;


        public List<ColumnType> columnTypeOrder;
        public List<string> columnNames;

        public string[] RowValuesToString()
        {
            int rowSize = CountColums();
            string[] stringRow = new string[rowSize];

            for(int i = 0; i < rowSize; i++)
            {
                switch(columnTypeOrder[i])
                {
                    case ColumnType.INT:
                        stringRow[i] = intColumnsDictionary[columnNames[i]].ParseToString();
                        break;
                    case ColumnType.VARCHAR:
                        stringRow[i] = varcharColumnsDictionary[columnNames[i]].ParseToString();
                        break;
                    case ColumnType.TIMEDATE:
                        stringRow[i] = timeDateColumnsDictionary[columnNames[i]].ParseToString();
                        break;
                    default:
                        break;
                }
            }

            return new string[10];
        }
        public string DEFAULT_FORMAT_
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string DEFAULT_MIN_VAL_FORMAT
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int CountColums()
        {
            return intColumnsDictionary.Count + varcharColumnsDictionary.Count + timeDateColumnsDictionary.Count;
        }

        public string ParseToString(Row obj)
        {
            throw new NotImplementedException();
        }

        public Row ParseToObjectType(string str)
        {
            throw new NotImplementedException();
        }
    }
}
