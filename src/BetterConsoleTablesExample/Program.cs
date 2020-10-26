﻿using System;
using BetterConsoleTables;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using BetterConsoleTablesExample;
using BetterConsoleTables.Models;
using BetterConsoleTables.Configuration;
using System.Drawing;
using BetterConsoleTables.Builders;
using System.Linq;
using BetterConsole.Core;
using BetterConsole.Colors.Extensions;

using Clawfoot.TestUtilities.Performance;
using BetterConsoleTables.Builders.Interfaces;
using BetterConsoleTables.Builders.Interfaces.Table;

namespace BetterConsoleTables_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            PerformanceTest.Run();
            //ShowAlignedTables();
            //ShowExmapleMultiTable();
            //ShowFormattedTable();
            //ShowExampleTables();
            Console.ReadLine();
        }

        //Unused TEST
        private static void RunWrapPerformanceTest1()
        {
            int iterations = 25000;
            Stopwatch stopwatch = new Stopwatch();
            long total = 0;

            for(int i = 0; i < iterations; i++)
            {
                Console.SetCursorPosition(0, 0);
                stopwatch.Restart();

                WordWrap("For a simple concatenation of 3 or 4 strings, it probably won't make any significant difference, and string concatenation may even be slightly faster - but if you're wrong and there are lots of rows, StringBuilder will start getting much more efficient, and it's always more descriptive of what you're doing.", 20);

                stopwatch.Stop();
                total += stopwatch.ElapsedTicks;
                Console.Write(i);
            }

            Console.WriteLine();
            Console.WriteLine(total / iterations);
        }

        public static List<string> WordWrap(string input, int maxCharacters)
        {
            List<string> lines = new List<string>();

            if (!input.Contains(" "))
            {
                int start = 0;
                while (start < input.Length)
                {
                    lines.Add(input.Substring(start, Math.Min(maxCharacters, input.Length - start)));
                    start += maxCharacters;
                }
            }
            else
            {
                string[] words = input.Split(' ');

                string line = "";
                foreach (string word in words)
                {
                    if ((line + word).Length > maxCharacters)
                    {
                        lines.Add(line.Trim());
                        line = "";
                    }

                    line += string.Format("{0} ", word);
                }

                if (line.Length > 0)
                {
                    lines.Add(line.Trim());
                }
            }

            return lines;
        }
        private static void ShowFormattedTable()
        {
            Console.WriteLine();
            Table1();
            Console.WriteLine();
            Table2();

            void Table1()
            {
                //THis throws Exception
                IColumn[] headers = new[]
                {
                    new ColumnBuilder("Colors!").WithHeaderFormat().WithForegroundColor(Color.BlueViolet).GetColumn(),
                    new ColumnBuilder("Right").WithHeaderFormat().WithForegroundColor(Color.Green).GetColumn(),
                    new ColumnBuilder("Center!").WithHeaderFormat().WithForegroundColor(Color.Firebrick).GetColumn(),
                };

                Table table = new Table(headers);
                table.Config = TableConfig.MySqlSimple();
                table.AddRow(Color.Gray.ToString(), "2", "3");
                table.AddRow("Hello", "2", "3");
                table.AddRow("Hello World!", "item", "Here");
                table.AddRow("Longer items go here", "stuff stuff", "some centered thing");

                Console.Write(table.ToString());
            }

            ITableBuilder test = null;

            void Table2()
            {
                IColumn[] columns =
                {
                    new ColumnBuilder("Colors!")
                        .WithHeaderFormat()
                            .WithForegroundColor(Color.BlueViolet)
                        .GetColumn(),
                    new ColumnBuilder("Right")
                                    .WithHeaderFormat()
                                        .WithForegroundColor(Color.Green)
                                        .WithAlignment(Alignment.Right)
                                    .GetColumn(),
                    new ColumnBuilder("Center!")
                                    .WithHeaderFormat()
                                        .WithForegroundColor(Color.Firebrick)
                                        .WithAlignment(Alignment.Center)
                                        .WithFontStyle(FontStyleExt.Bold)
                                    .WithRowsFormat()
                                        .WithForegroundColor(Color.DarkOliveGreen)
                                        .WithAlignment(Alignment.Center)
                                    .GetColumn(),
                    new ColumnBuilder("Bold & Underlined!!")
                                    .WithHeaderFormat()
                                        .WithForegroundColor(Color.SeaShell)
                                        .WithAlignment(Alignment.Center)
                                        .WithFontStyle(FontStyleExt.Bold | FontStyleExt.Underline)
                                    .GetColumn()
                };

                Table table = new Table()
                    .AddColumn(columns[0])
                    .AddColumn(columns[1])
                    .AddColumn(columns[2])
                    .AddColumn(columns[3]);
                table.Config = TableConfig.MySqlSimple();
                table.AddRow("99", "2", "3");
                table.AddRow("Hello World!", "item", "Here");
                table.AddRow("Longer items go here", "stuff stuff", "some centered thing");

                Console.Write(table.ToString());

                table.ReplaceRows(new List<object[]>()
                {
                    new [] { "123", "2", "3" },
                    new [] { "Hello World!", "item", "Here" },
                    new [] { "Replaced", "the", "data" },
                });

                Console.WriteLine();
                Console.Write(table.ToString());
            }
        }

        private static void ShowAlignedTables()
        {
            Console.WriteLine();
            Table1();
            Console.WriteLine();
            Table2();
            Console.WriteLine();

            void Table1()
            {
                IColumn[] headers = new[]
                {
                    Column.Simple("Left"),
                    Column.Simple("Right", Alignment.Right, Alignment.Right),
                    Column.Simple("Center", Alignment.Center, Alignment.Center),
                };

                var test = Color.Gray;

                Table table = new Table(headers);
                table.Config = TableConfig.MySqlSimple();
                table.AddRow("1", "2", "3");
                table.AddRow("Hello There How Do You", "2", "3");
                table.AddRow("Hello World!", "item", "Here");
                table.AddRow("Longer items go here", "stuff stuff", "some centered thing");

                Console.Write(table.ToString());
            }

            void Table2()
            {
                IColumn[] headers = new[]
                {
                    Column.Simple("Left"),
                    Column.Simple("Left Header", Alignment.Right),
                    Column.Simple("Right Header", Alignment.Center, Alignment.Right),
                };
                Table table = new Table(headers);
                table.Config = TableConfig.MySqlSimple();
                table.AddRow("1", "2", "3");
                table.AddRow("Short", "item", "Here");
                table.AddRow("Longer items go here", "Right Contents", "Centered Contents");

                Console.Write(table.ToString());
            }
        }

        private static void ShowExampleTables()
        {
            ShowFormattedTable();
            ShowAlignedTables();
            Console.WriteLine();

            Table table = new Table("One", "Two", "Three");
            table.AddRow("1", "2", "3");
            table.AddRow("Short", "item", "Here");
            table.AddRow("Longer items go here", "stuff stuff", "stuff");

            table.Config = TableConfig.Default();

            Console.Write(table.ToString());
            Console.WriteLine();
            table.Config = TableConfig.Markdown();
            Console.Write(table.ToString());
            Console.WriteLine();
            table.Config = TableConfig.MySql();
            Console.Write(table.ToString());
            Console.WriteLine();
            table.Config = TableConfig.MySqlSimple();
            Console.Write(table.ToString());
            Console.WriteLine();
            table.Config = TableConfig.Unicode();
            Console.Write(table.ToString());
            Console.WriteLine();
            table.Config = TableConfig.UnicodeAlt();
            Console.Write(table.ToString());
            Console.WriteLine();
        }

        private static void ShowExmapleMultiTable()
        {
            Table table = new Table("One", "Two", "Three");
            table.Config = TableConfig.UnicodeAlt();
            table.AddRow("1", "2", "3");
            table.AddRow("Short", "item", "Here");
            table.AddRow("Longer items go here", "stuff stuff", "stuff");

            Table table2 = new Table("One", "Two", "Three", "Four");
            table2.Config = TableConfig.UnicodeAlt();
            table2.AddRow("One", "Two", "Three");
            table2.AddRow("Short", "item", "Here", "A fourth column!!!");
            table2.AddRow("stuff", "longer stuff", "even longer stuff in this cell");


            Table table3 = new Table("One", "Two");
            table3.Config = TableConfig.UnicodeAlt();
            table3.AddRow("One", "Two");
            table3.AddRow("Short", "item");
            table3.AddRow("stuff", "longer stuff");

            ConsoleTables tables = new ConsoleTables(table, table2, table3);
            Console.Write(tables.ToString());
        }

        private static void ShowExampleGeneratedTable()
        {
            DataStuff[] objects = new DataStuff[10];
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                objects[i] = new DataStuff(random);
            }
            Table table = new Table(TableConfig.MySql());
            table.From<DataStuff>(objects);

            Console.Write(table.ToString());
        }
    }

    public class DataStuff
    {
        public DataStuff(Random random)
        {
            Name = $"Name #{random.Next(1, 100)}";
            Count = random.Next(1000, 50000);
            TimeSpent = new TimeSpan(random.Next(123456, 1234567890));
        }

        string Name { get; set; }
        int Count { get; set; }
        TimeSpan TimeSpent { get; set; }
    }
}