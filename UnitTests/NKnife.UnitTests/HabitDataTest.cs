using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using Xunit;

namespace NKnife.UnitTests
{
    public class HabitDataTest
    {
        public static string ProjectPath { get; set; } = nameof(ProjectPath);
        public static string SaveTables { get; set; } = nameof(SaveTables);
        public static string ExcelIsMultiFile { get; set; } = nameof(ExcelIsMultiFile);
        public static string TableSavePath { get; set; } = nameof(TableSavePath);

        [Fact]
        public void Test01()
        {
            var hd = new HabitData();
            hd.SetValue("abcd", "abcd");
            hd.GetValue("abcd","1234").Should().Be("abcd");
        }

        [Fact]
        public void Test02()
        {
            var folder = Environment.SpecialFolder.Desktop;
            var path = Environment.GetFolderPath(folder);
            var name = "Habit-UnitTest.xml";
            var fi = new FileInfo($"{path}\\{name}");

            var habitData = new HabitData(fi);
            habitData.SetValue(ProjectPath, "ProjectPath---ProjectPath");
            habitData.SetValue(SaveTables, "SaveTables---SaveTables");
            habitData.SetValue(ExcelIsMultiFile, "ExcelIsMultiFile---ExcelIsMultiFile");
            habitData.SetValue(TableSavePath, "TableSavePath---TableSavePath");

            habitData.GetValue(ProjectPath, null).Should().Be($"{ProjectPath}---{ProjectPath}");
            habitData.GetValue(SaveTables, null).Should().Be($"{SaveTables}---{SaveTables}");
            habitData.GetValue(ExcelIsMultiFile, null).Should().Be($"{ExcelIsMultiFile}---{ExcelIsMultiFile}");
            habitData.GetValue(TableSavePath, null).Should().Be($"{TableSavePath}---{TableSavePath}");
        }

        [Fact]
        public void Test03()
        {
            var folder = Environment.SpecialFolder.Desktop;
            var path = Environment.GetFolderPath(folder);
            var name = "Habit-UnitTest.xml";
            var fi = new FileInfo($"{path}\\{name}");

            var hd1 = new HabitData(fi);
            hd1.SetValue(ProjectPath, "ProjectPath---ProjectPath");
            hd1.SetValue(SaveTables, "SaveTables---SaveTables");
            hd1.SetValue(ExcelIsMultiFile, "ExcelIsMultiFile---ExcelIsMultiFile");
            hd1.SetValue(TableSavePath, "TableSavePath---TableSavePath");

            var hd2 = new HabitData(fi);
            hd2.GetValue(ProjectPath, null).Should().Be($"{ProjectPath}---{ProjectPath}");
            hd2.GetValue(SaveTables, null).Should().Be($"{SaveTables}---{SaveTables}");
            hd2.GetValue(ExcelIsMultiFile, null).Should().Be($"{ExcelIsMultiFile}---{ExcelIsMultiFile}");
            hd2.GetValue(TableSavePath, null).Should().Be($"{TableSavePath}---{TableSavePath}");
        }

        [Fact]
        public void Test04()
        {
            var folder = Environment.SpecialFolder.Desktop;
            var path = Environment.GetFolderPath(folder);
            var name = "Habit-UnitTest.xml";
            var fi = new FileInfo($"{path}\\{name}");

            var habitData = new HabitData(fi);

            habitData.GetValue(ProjectPath, $"{ProjectPath}---{ProjectPath}")
                .Should().Be($"{ProjectPath}---{ProjectPath}");
            habitData.GetValue(SaveTables, $"{SaveTables}---{SaveTables}")
                .Should().Be($"{SaveTables}---{SaveTables}");
            habitData.GetValue(ExcelIsMultiFile, $"{ExcelIsMultiFile}---{ExcelIsMultiFile}")
                .Should().Be($"{ExcelIsMultiFile}---{ExcelIsMultiFile}");
            habitData.GetValue(TableSavePath, $"{TableSavePath}---{TableSavePath}")
                .Should().Be($"{TableSavePath}---{TableSavePath}");

            var hd2 = new HabitData(fi);
            hd2.GetValue(ProjectPath, null).Should().Be($"{ProjectPath}---{ProjectPath}");
            hd2.GetValue(SaveTables, null).Should().Be($"{SaveTables}---{SaveTables}");
            hd2.GetValue(ExcelIsMultiFile, null).Should().Be($"{ExcelIsMultiFile}---{ExcelIsMultiFile}");
            hd2.GetValue(TableSavePath, null).Should().Be($"{TableSavePath}---{TableSavePath}");
        }


        [Fact]
        public void Test05()
        {
            var folder = Environment.SpecialFolder.Desktop;
            var path = Environment.GetFolderPath(folder);
            var name = "Habit-UnitTest.xml";
            var fi = new FileInfo($"{path}\\{name}");

            var hd1 = new HabitData(fi);
            hd1.SetValue("aa", "a-123456");
            hd1.SetValue("bb", "b-987654");
            hd1.GetValue("aa", null).Should().Be("a-123456");
            hd1.GetValue("bb", null).Should().Be("b-987654");

            var hd2 = new HabitData(fi);
            hd2.GetValue("aa", null).Should().Be("a-123456");
            hd2.GetValue("bb", null).Should().Be("b-987654");
            hd2.SetValue("aa", "xyz-123");
            hd2.SetValue("bb", "abc-789");
            hd2.GetValue("aa", null).Should().Be("xyz-123");
            hd2.GetValue("bb", null).Should().Be("abc-789");
        }
    }

}
