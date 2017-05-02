using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructuresURL_3._0;

namespace Proyecto01
{

    enum ColumnType { INT, VARCHAR, TIMEDATE }


    class Row : IStringParseable<Row>
    {

        public Dictionary<string, Int> intColumnsDictionary;
        public Dictionary<string, Varchar> varcharColumnsDictionary;
        public Dictionary<string, TimeDate> timeDateColumnsDictionary;


        public List<ColumnType> columnTypeOrder;
        public List<string> columnNames;


        public Row()
        {


            intColumnsDictionary = new Dictionary<string, Int>();
            varcharColumnsDictionary = new Dictionary<string, Varchar>();
            timeDateColumnsDictionary = new Dictionary<string, TimeDate>();


            columnTypeOrder = new List<ColumnType>();
            columnNames = new List<string>();
        }

        public Row(string rowModelSpecifications)
        {
            intColumnsDictionary = new Dictionary<string, Int>();
            varcharColumnsDictionary = new Dictionary<string, Varchar>();
            timeDateColumnsDictionary = new Dictionary<string, TimeDate>();


            columnTypeOrder = new List<ColumnType>();
            columnNames = new List<string>();




            //columnName,type,specialAttribute|columnName,type,specialAttribute
            string[] columns = rowModelSpecifications.Split('|');

            for (int i = 0; i < columns.Length; i++)
            {
                string[] columnsAttributes = columns[i].Split(',');
                string columnName = columnsAttributes[0];
                string columnType = columnsAttributes[1];
                //string columnSpecialAttribute = columnsAttributes[2];


                columnNames.Add(columnName);

                switch (columnType)
                {
                    case "INT":
                        columnTypeOrder.Add(ColumnType.INT);
                        intColumnsDictionary.Add(columnName, new Int());
                        break;
                    case "VARCHAR(100)":
                        columnTypeOrder.Add(ColumnType.VARCHAR);
                        varcharColumnsDictionary.Add(columnName, new Varchar());
                        break;
                    case "DATETIME":
                        columnTypeOrder.Add(ColumnType.TIMEDATE);
                        timeDateColumnsDictionary.Add(columnName, new TimeDate());
                        break;
                    default:
                        break;
                }
            }


        }


        public string[] RowValuesToString()
        {
            int rowSize = CountColums();
            string[] stringRow = new string[rowSize];

            for (int i = 0; i < rowSize; i++)
            {
                switch (columnTypeOrder[i])
                {
                    case ColumnType.INT:
                        stringRow[i] = intColumnsDictionary[columnNames[i]].value.ToString();
                        break;
                    case ColumnType.VARCHAR:
                        stringRow[i] = varcharColumnsDictionary[columnNames[i]].value.ToString();
                        break;
                    case ColumnType.TIMEDATE:
                        stringRow[i] = timeDateColumnsDictionary[columnNames[i]].value.ToString("dd/MM/yyyy");
                        break;
                    default:
                        break;
                }
            }
            return stringRow;
        }
        public string[] RowValuesToString(string keyValue)
        {
            int rowSize = 1 + CountColums();
            string[] stringRow = new string[rowSize];


            stringRow[0] = keyValue;
            for (int i = 0; i < rowSize; i++)
            {
                switch (columnTypeOrder[i])
                {
                    case ColumnType.INT:
                        stringRow[i + 1] = intColumnsDictionary[columnNames[i]].ParseToString();
                        break;
                    case ColumnType.VARCHAR:
                        stringRow[i + 1] = varcharColumnsDictionary[columnNames[i]].ParseToString();
                        break;
                    case ColumnType.TIMEDATE:
                        stringRow[i + 1] = timeDateColumnsDictionary[columnNames[i]].ParseToString();
                        break;
                    default:
                        break;
                }
            }



            return stringRow;
        }
        public string[] RowValuesToString(string keyValue, string[] columnsToSelect)
        {
            //// Removemos keyValue de columnsToSelect (si existe) porque... row no contiene keys.
            //List<string> colsToSelectList = columnsToSelect.ToList<string>();
            //colsToSelectList.Remove(keyValue);
            //columnsToSelect = colsToSelectList.ToArray();

            // rowSize es igual al espacio de key mas el espacio de las columnas a seleccionar
            int rowSize = 1 + columnsToSelect.Length;
            // stringRow, nuestro string que simulara la misma row, pero valores en string.
            string[] stringRow = new string[rowSize];

            // stringRowCounter muy importante, es nuestro contador referencia para colocar valores en el arreglo
            int stringRowCounter = 0;
            stringRow[stringRowCounter++] = keyValue; //agregamos keyValue y sumamos a stringRowCounter

            for (int i = 0; i < CountColums(); i++) // Este for recorrera todas las columnas que contiene el objeto Row
            {
                switch (columnTypeOrder[i]) //Necesitamos saber con que tipo de columna vamos tratando pasito a pasito, dandole suavecito suavecito
                {
                    case ColumnType.INT:
                        if (columnsToSelect.Contains<string>(columnNames[i])) //Como esta es sobrecarga del metodo es para filtro, solo añadimos a stringRow si el valor coincide con valoresToSelect...
                            stringRow[stringRowCounter++] = intColumnsDictionary[columnNames[i]].value.ToString();
                        break;
                    case ColumnType.VARCHAR:
                        if (columnsToSelect.Contains<string>(columnNames[i])) // Misma situacion
                            stringRow[stringRowCounter++] = varcharColumnsDictionary[columnNames[i]].value.ToString();
                        break;
                    case ColumnType.TIMEDATE:
                        if (columnsToSelect.Contains<string>(columnNames[i])) // Misma situacion
                            stringRow[stringRowCounter++] = timeDateColumnsDictionary[columnNames[i]].value.ToString("dd/MM/yyyy");
                        break;
                    default:
                        break;
                }
            }



            return stringRow;
        }

        public string[] RowValuesToString(string[] columnsToSelect)
        {
            //// Removemos keyValue de columnsToSelect (si existe) porque... row no contiene keys.
            //List<string> colsToSelectList = columnsToSelect.ToList<string>();
            //colsToSelectList.Remove(keyValue);
            //columnsToSelect = colsToSelectList.ToArray();

            // rowSize es igual al espacio de key mas el espacio de las columnas a seleccionar
            int rowSize = columnsToSelect.Length;
            // stringRow, nuestro string que simulara la misma row, pero valores en string.
            string[] stringRow = new string[rowSize];

            // stringRowCounter muy importante, es nuestro contador referencia para colocar valores en el arreglo
            int stringRowCounter = 0;

            for (int i = 0; i < CountColums(); i++) // Este for recorrera todas las columnas que contiene el objeto Row
            {
                switch (columnTypeOrder[i]) //Necesitamos saber con que tipo de columna vamos tratando pasito a pasito, dandole suavecito suavecito
                {
                    case ColumnType.INT:
                        if (columnsToSelect.Contains<string>(columnNames[i])) //Como esta es sobrecarga del metodo es para filtro, solo añadimos a stringRow si el valor coincide con valoresToSelect...
                            stringRow[stringRowCounter++] = intColumnsDictionary[columnNames[i]].value.ToString();
                        break;
                    case ColumnType.VARCHAR:
                        if (columnsToSelect.Contains<string>(columnNames[i])) // Misma situacion
                            stringRow[stringRowCounter++] = varcharColumnsDictionary[columnNames[i]].value.ToString();
                        break;
                    case ColumnType.TIMEDATE:
                        if (columnsToSelect.Contains<string>(columnNames[i])) // Misma situacion
                            stringRow[stringRowCounter++] = timeDateColumnsDictionary[columnNames[i]].value.ToString("dd/MM/yyyy");
                        break;
                    default:
                        break;
                }
            }



            return stringRow;
        }


        public bool ValueExistsInRow(string column, string value)
        {
            if (intColumnsDictionary.ContainsKey(column))
                return ValueExistsInRow(column, new Int().ParseToObjectType(value));

            else if (varcharColumnsDictionary.ContainsKey(column))
                return ValueExistsInRow(column, new Varchar().ParseToObjectType(value));

            else if (timeDateColumnsDictionary.ContainsKey(column))
                return ValueExistsInRow(column, new TimeDate().ParseToObjectType(value));

            else
                return false;


        }
        bool ValueExistsInRow(string column, Int obj)
        {
            return intColumnsDictionary[column].value == obj.value;
        }
        bool ValueExistsInRow(string column, Varchar obj)
        {

            return varcharColumnsDictionary[column].value == obj.value;
        }
        bool ValueExistsInRow(string column, TimeDate obj)
        {

            return timeDateColumnsDictionary[column].value == obj.value;
        }



        // INTERFACE METHODS


        // NOMBRE,VARCHAR,JOSE^APELLIDO,VARCHAR,ALVAREZ^FACULTAD,VARCHAR,INGENIERIA^CUMPLEAÑOS,TIMEDATE,08/12/0997
        public string DEFAULT_FORMAT_
        {
            get
            {
                //"~"

                string defaultFormat = "";

                for (int i = 0; i < SpaceRequiredToStoreInfoInString(); i++)
                {
                    defaultFormat = defaultFormat + "~";

                }

                return defaultFormat;

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
                return SpaceRequiredToStoreInfoInString();
            }
        }

        public int CountColums()
        {
            return intColumnsDictionary.Count + varcharColumnsDictionary.Count + timeDateColumnsDictionary.Count;
        }

        public string ParseToString(Row obj)
        {

            string rowInfo = "";

            if (obj.CountColums() != 0)
            {
                for (int i = 0; i < obj.CountColums(); i++)
                {
                    switch (obj.columnTypeOrder[i])
                    {
                        case ColumnType.INT:
                            rowInfo += obj.columnNames[i] + ",INT," + obj.intColumnsDictionary[obj.columnNames[i]].value.ToString() + "^";
                            break;
                        case ColumnType.VARCHAR:
                            rowInfo += obj.columnNames[i] + ",VARCHAR(100)," + obj.varcharColumnsDictionary[obj.columnNames[i]].value + "^";

                            break;
                        case ColumnType.TIMEDATE:
                            rowInfo += obj.columnNames[i] + ",DATETIME," + obj.timeDateColumnsDictionary[obj.columnNames[i]].value.ToString("dd/MM/yyyy") + "^";
                            break;
                        default:
                            break;
                    }
                }

                rowInfo = rowInfo.Substring(0, rowInfo.Length - 1);
            }



            int spaceRequiredToStoreInfo = SpaceRequiredToStoreInfoInString(obj);

            while (rowInfo.Length != spaceRequiredToStoreInfo)
            {
                rowInfo = "~" + rowInfo;
            }

            return rowInfo;
        }

        public Row ParseToObjectType(string str)
        {
            //NOMBRE,VARCHAR,JOSE^APELLIDO,VARCHAR,ALVAREZ^FACULTAD,VARCHAR,INGENIERIA^CUMPLEAÑOS,TIMEDATE,08/12/0997


            str = str.Replace('~', ' ');
            str = str.Trim();



            Row rowInstance = new Row();

            if (str == "")
                return rowInstance;


            string[] columns = str.Split('^');

            for (int i = 0; i < columns.Length; i++)
            {
                string[] columnAttributes = columns[i].Split(',');
                string columnName = columnAttributes[0];
                string columnType = columnAttributes[1];
                string columnValue = columnAttributes[2];

                rowInstance.columnNames.Add(columnName);

                switch (columnType)
                {
                    case "INT":
                        rowInstance.columnTypeOrder.Add(ColumnType.INT);
                        rowInstance.intColumnsDictionary.Add(columnName, new Int().ParseToObjectType(columnValue));
                        break;
                    case "VARCHAR(100)":
                        rowInstance.columnTypeOrder.Add(ColumnType.VARCHAR);
                        rowInstance.varcharColumnsDictionary.Add(columnName, new Varchar().ParseToObjectType(columnValue));
                        break;
                    case "DATETIME":
                        rowInstance.columnTypeOrder.Add(ColumnType.TIMEDATE);
                        rowInstance.timeDateColumnsDictionary.Add(columnName, new TimeDate().ParseToObjectType(columnValue));
                        break;
                    default:
                        break;
                }

            }


            return rowInstance;

        }

        public string ParseToString()
        {

            string rowInfo = "";

            if (CountColums() != 0)
            {
                for (int i = 0; i < CountColums(); i++)
                {
                    switch (columnTypeOrder[i])
                    {
                        case ColumnType.INT:
                            rowInfo += columnNames[i] + ",INT," + intColumnsDictionary[columnNames[i]].value.ToString() + "^";
                            break;
                        case ColumnType.VARCHAR:
                            rowInfo += columnNames[i] + ",VARCHAR(100)," + varcharColumnsDictionary[columnNames[i]].value + "^";

                            break;
                        case ColumnType.TIMEDATE:
                            rowInfo += columnNames[i] + ",DATETIME," + timeDateColumnsDictionary[columnNames[i]].value.ToString("dd/MM/yyyy") + "^";
                            break;
                        default:
                            break;
                    }
                }



                rowInfo = rowInfo.Substring(0, rowInfo.Length - 1);
            }
                

            int spaceRequiredToStoreInfo = SpaceRequiredToStoreInfoInString();

            while (rowInfo.Length != spaceRequiredToStoreInfo)
            {
                rowInfo = "~" + rowInfo;
            }

            return rowInfo;

        }


        public int SpaceRequiredToStoreInfoInString()
        {
            /*
             *  20 -> NOMBRE DE COLUMNA

                INT -> 11

                TIMEDATE -> 10

                VARCHAR -> 100 

                tamaño minimo aproximado de espacio requerido para guardar la info...

                20 * (#cols) + 11 * intDictionary.Count + 10 * timedateDictionary.count + 100 * varcharDictionary.count + (#cols)*3
                                                                                                                              ^
                                                                                                                            esta parte representa 2 comas y separadores ^
             *
             * 
             * 
             * 
             * 
             */

            int spaceRequired = 0;
            int numberOfColumns = CountColums();


            spaceRequired = (20 * numberOfColumns) + (11 * intColumnsDictionary.Count + 100 * varcharColumnsDictionary.Count + 10 * timeDateColumnsDictionary.Count) + (3 * numberOfColumns);

            return spaceRequired;


        }

        public int SpaceRequiredToStoreInfoInString(Row obj)
        {
            /*
             *  20 -> NOMBRE DE COLUMNA

                INT -> 11

                TIMEDATE -> 10

                VARCHAR -> 100 

                tamaño minimo aproximado de espacio requerido para guardar la info...

                20 * (#cols) + 11 * intDictionary.Count + 10 * timedateDictionary.count + 100 * varcharDictionary.count + (#cols)*3
                                                                                                                              ^
                                                                                                                            esta parte representa 2 comas y separadores ^
             *
             * 
             * 
             * 
             * 
             */

            int spaceRequired = 0;
            int numberOfColumns = obj.CountColums();


            spaceRequired = (20 * numberOfColumns) + (11 * obj.intColumnsDictionary.Count + 100 * obj.varcharColumnsDictionary.Count + 10 * obj.timeDateColumnsDictionary.Count) + (3 * numberOfColumns);

            return spaceRequired;


        }


    }
}
