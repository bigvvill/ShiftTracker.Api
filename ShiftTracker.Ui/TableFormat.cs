using ConsoleTableExt;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftTracker.Ui
{
    internal class TableFormat
    {
        public static void ShowTable<T>(List<T> tableData, [AllowNull] string tableName) where T :
            class
        {
            Console.Clear();

            if (tableName == null)
                tableName = "";

            ConsoleTableBuilder
                .From(tableData)
                .WithColumn(tableName)
                .WithFormat(ConsoleTableBuilderFormat.Alternative)
                .ExportAndWriteLine(TableAligntment.Center);
            //Console.WriteLine("\n");
        }

        public static void ShowList(List<object> tableData, string tableName)
        {
            Console.Clear();

            if (tableName == null)
                tableName = "";

            ConsoleTableBuilder
                .From(tableData)
                .WithColumn(tableName)
                .ExportAndWriteLine();
            //Console.WriteLine("\n");
        }
    }
}
