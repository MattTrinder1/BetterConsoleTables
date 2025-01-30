using BetterConsoles.Tables;
using BetterConsoles.Tables.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetterConsoles.Tests
{

    class test
    {
        public double? NullDoubleProp { get; set; }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            IColumn[] headers = new[]
            {
                new Column("Null Double"),
            };
            var table = new Table(headers);
            var t = new test();
            table.AddRow(t.NullDoubleProp);

            Console.Write(table.ToString());

        }
    }
}