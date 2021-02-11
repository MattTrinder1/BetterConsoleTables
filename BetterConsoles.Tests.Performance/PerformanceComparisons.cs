﻿using BenchmarkDotNet.Attributes;
using BetterConsoles.Core;
using BetterConsoles.Tables;
using BetterConsoles.Tables.Builders;
using BetterConsoles.Tables.Configuration;
using BetterConsoles.Tables.Models;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Alignment = BetterConsoles.Tables.Alignment;

namespace BetterConsoles.Tests.Performance
{
    [MemoryDiagnoser]
    [Config(typeof(AllowNonOptimized))]
    public class PerformanceComparisons
    {
        Table defaultTable;

        public PerformanceComparisons()
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

            this.defaultTable = table;
        }


        [Benchmark(Baseline = true)]
        public void TestDefacto()
        {
            var table = new ConsoleTable("One", "Two", "Three");
            table.AddRow("1", "2", "3")
                .AddRow("Short", "item", "Here")
                .AddRow("Longer items go here", "stuff", "stuff");

            string tableString = table.ToMarkDownString();
        }

        [Benchmark]
        public void TestLegacyTables()
        {
            BetterConsoleTables.Table table = new BetterConsoleTables.Table("One", "Two", "Three");
            table.Config = BetterConsoleTables.TableConfiguration.Unicode();
            table.AddRow("1", "2", "3");
            table.AddRow("Short", "item", "Here");
            table.AddRow("Longer items go here", "stuff", "stuff");

            string tableString = table.ToString();
        }

        [Benchmark]
        public void TestNewTables()
        {
            Table table = new Table("One", "Two", "Three");
            table.Config = TableConfig.Unicode();
            table.AddRow("1", "2", "3")
                .AddRow("Short", "item", "Here")
                .AddRow("Longer items go here", "stuff", "stuff");

            string tableString = table.ToString();
        }

        [Benchmark]
        public void NewTableFormatted()
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

            string tableString = table.ToString();
        }

        [Benchmark]
        public void FormattedReplaceData()
        {
            defaultTable.ReplaceRows(new List<object[]>()
                {
                    new [] { "123", "2", "3" },
                    new [] { "Hello World!", "item", "Here" },
                    new [] { "Replaced", "the", "data" },
                });

            string tableString = defaultTable.ToString();
        }
    }
}
