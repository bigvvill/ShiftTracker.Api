using ConsoleTableExt;
using ShiftTracker.Api.Entities;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;

namespace ShiftTracker.Ui;

internal class TableFormat
{
    public static void ShowTable<T>(List<Shift> tableData, [AllowNull] string tableName) where T :
        class
    {        
        // TODO : format Pay - round

        foreach(var table in tableData)
        {
            var pay = table.Pay.ToString();
            Console.WriteLine(pay);
            
        }
        Console.ReadLine();

        if (tableName == null)
            tableName = "";

        ConsoleTableBuilder
            .From(tableData)
            .WithTitle("Shifts")            
            .WithFormat(ConsoleTableBuilderFormat.Alternative)
            .ExportAndWriteLine(TableAligntment.Center);        
    }    
}
