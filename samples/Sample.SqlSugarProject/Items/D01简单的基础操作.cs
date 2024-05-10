using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using ConsoleUi;
using Newtonsoft.Json;
using SqlSugar;

namespace Sample.SqlSugarProject.Items
{
    class D01简单的基础操作 : IRunnable
    {
        public string DbFile => "demo.db";

        public bool Run(IMenuUserInterface ui, object? obj = null)
        {
            File.Delete(DbFile);
            var sugar = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString      = $"datasource={DbFile}",
                DbType                = DbType.Sqlite,
                IsAutoCloseConnection = true
            }, _ => {});
            ui.Prompt("创建数据库对象");
            
            sugar.DbMaintenance.CreateDatabase();
            ui.Prompt("建库");

            sugar.CodeFirst.InitTables<Student>();
            ui.Prompt("建表");

            SelectList(ui, sugar);

            var fixture = new Fixture();

            for (int i = 0; i < 5; i++)
            {
                var s1 = fixture.Create<Student>();

                sugar.Insertable(s1).ExecuteCommand();
                var info = $"-> {s1}";
                ui.Prompt(info);
            }

            SelectList(ui, sugar);

            sugar.Updateable(new Student() { Id = 1, SchoolId = 2, Name = "jack2" }).ExecuteCommand();
            ui.Prompt("更新");

            sugar.Deleteable<Student>().Where(it => it.Id == 1).ExecuteCommand();
            ui.Prompt("删除");

            return true;
        }

        private static void SelectList(IMenuUserInterface ui, SqlSugarClient sugar)
        {
            var list = sugar.Queryable<Student>().ToList();
            ui.Info($"查询表的所有,数量={list.Count}");

            foreach (var student in list)
            {
                ui.Info(student.ToString());
            }
        }

        public class Student
        {
            [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
            public int Id { get; set; }
            public int? SchoolId { get; set; }
            public string? Name { get; set; }

            public override string ToString()
            {
                return JsonConvert.SerializeObject(this);
            }
        }
    }
}
