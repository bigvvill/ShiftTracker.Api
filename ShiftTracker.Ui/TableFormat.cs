using ConsoleTableExt;
using System.Diagnostics.CodeAnalysis;

namespace ShiftTracker.Ui;

internal class TableFormat
{
    public static void ShowTable<T>(List<T> tableData, [AllowNull] string tableName) where T :
        class
    {        
        // TODO : format Pay - round

        if (tableName == null)
            tableName = "";

        ConsoleTableBuilder
            .From(tableData)
            .WithTitle("Shifts")            
            .WithFormat(ConsoleTableBuilderFormat.Alternative)
            .ExportAndWriteLine(TableAligntment.Center);        
    }    
}
