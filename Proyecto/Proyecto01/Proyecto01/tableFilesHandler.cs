using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto01
{
    class tableFilesHandler
    {


        static public int ReadNumberOfColumns(string path)
        {
            return 0;
        }
        static public string ReadTypeOfKey(string path)
        {
            return "";
        }

        static public Row GetRowModelFromFile(string path)
        {
            Row aRow = new Row();
            //leyendo que tiene una columna Apellido
            aRow.varcharColumnsDictionary.Add("Apellido", new Varchar());
            return new Row();
        }


    }
}
