﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace select_properties_from_class
{
    class Program
    {
        static void Main()
        {
            DataTable dt = ConvertToDataTable(testList);
            Console.WriteLine("D I S P L A Y    P O P U L A T E D    T A B L E");
            DisplayTableInConsole(dt);

            // Pause
            Console.ReadKey();
        }
        private static DataTable ConvertToDataTable<T>(IEnumerable<T> data) where T : IMyConstraint
        {
            DataTable table = new DataTable();
            Type type = typeof(T), propertyType;
            // "If" you already know the names of the properties you want...
            string[] names = new string[] { "type", "id", "name", "city" };
            
            foreach (var name in names)     // Add columns 
            {
                propertyType = type.GetProperty(name).PropertyType;
                table.Columns.Add(name).DataType = propertyType;
            }            
            foreach (var item in data)      // Add rows 
            {
                object[] values =
                    names.Select(name => type.GetProperty(name).GetValue(item)).ToArray();
                table.Rows.Add(values);
            }
            return table;
        }
        class SomeClass : IMyConstraint
        {
            public string type { get => GetType().Name; }
            public int id { get; private set; } = _count++;
            public string name { get => "Name " + id.ToString(); }
            public string city { get => "City " + id.ToString(); }
            public string someOtherProperty { get; set; } = "Do Not Display";

            static int _count = 0;
        }
        static List<SomeClass> testList =
            Enumerable
            .Range(1, 5)
            .Select(fx => new SomeClass())
            .ToList();
        private static void DisplayTableInConsole(DataTable dt)
        {
            var columns = from DataColumn column in dt.Columns select column;
            Console.WriteLine(string.Join("|\t", columns));
            Console.WriteLine();
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine(string.Join("\t", columns.Select(column=>row[column.ColumnName])));
            }
        }
    }

    interface IMyConstraint
    {
        string type { get; }
        int id { get; }
        string name { get; }
        string city { get; }
    }
}
