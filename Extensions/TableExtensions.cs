using AutoFixture;
using Lopcommerce.Regles.WebAPI.Tests.Helpers;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Lopcommerce.Regles.WebAPI.Tests.Extensions
{
    public static class TableExtensions
    {

        private class DataWrapper<T>
        {
            public IEnumerable<T> Data { get; set; }
        }

        /// <summary>
        /// Converts a specflow table to an instance. But includes complex objects as well (ParentProperty.ChildProperty is the convention)
        /// </summary>
        public static T CreateComplexInstance<T>(this Table table, Func<string, string> transformValue = null)
        {
            var result = table.CreateInstance<T>();
            FillComplexInstance(table, result, transformValue);

            return result;
        }

        /// <summary>
        /// Converts a specflow table to an instance. But includes complex objects as well (ParentProperty.ChildProperty is the convention)
        /// </summary>
        public static T CreateComplexInstanceFixture<T>(this Table table, Func<string, string> transformValue = null, Fixture fixture = null)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));

            transformValue ??= value => Regex.Replace(value, "^\"(.*)\"$", match => match.Groups[1].Value);
            var result = table.CreateInstance<T>();
            if (HasComplexFields(table))
                table = ToVerticalTable(table);

            if (table.Header.Count == 2)
            {
                var fieldName = table.Header.First();
                var valueName = table.Header.Last();
                foreach (var row in table.Rows)
                {
                    result.SetPropValue(row[fieldName], transformValue(row[valueName]), fixture);
                }
            }

            return result;
        }

        /// <summary>
        /// Fill an instance with specflow table. But includes complex objects as well (ParentProperty.ChildProperty is the convention)
        /// </summary>
        public static void FillComplexInstance<T>(this Table table, T instance, Func<string, string> transformValue = null)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));

            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            transformValue ??= value => Regex.Replace(value, "^\"(.*)\"$", match => match.Groups[1].Value);
            if (HasComplexFields(table))
                table = ToVerticalTable(table);

            if (table.Header.Count == 2)
            {
                var fieldName = table.Header.First();
                var valueName = table.Header.Last();
                foreach (var row in table.Rows)
                {
                    instance.SetPropValue(row[fieldName], transformValue(row[valueName]));
                }
            }
        }

        public static void FillComplexInstanceFixture<T>(this Table table, T instance, Fixture fixture = null)
        {
            if (table.Header.Count != 2)
                throw new NotImplementedException("FillComplexInstance only support table with 2 headers (Field / Value");

            foreach (var row in table.Rows)
            {
                if (!string.IsNullOrWhiteSpace(row[0]))
                {
                    instance.SetPropValue(row[0], row[1], fixture);
                }
            }
        }

        /// <summary>
        /// Converts a specflow table to a Set on T. But includes complex objects as well (ParentProperty.ChildProperty is the convention)
        /// </summary>
        public static IEnumerable<T> CreateComplexSetCustom<T>(this Table table, Func<string, string> transformValue = null)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));

            if (table.Header.Count != 2 || HasComplexFields(table))
                table = ToVerticalTable(table, true);

            var result = table.CreateComplexInstance<DataWrapper<T>>(transformValue);
            return result?.Data;
        }

        private static bool HasComplexFields(Table table) => table.Header.Any(h => h.Contains('.') || Reflections.TabIndexRegex.IsMatch(h));

        private static Table ToVerticalTable(Table table, bool isASet = false)
        {
            var verticalTable = new Table("Field", "Value");
            for (var index = 0; index < table.RowCount; index++)
                foreach (var head in table.Header)
                    verticalTable.AddRow(isASet ? $"Data[{index}].{head}" : head, table.Rows[index][head]);

            return verticalTable;
        }
    }
}
