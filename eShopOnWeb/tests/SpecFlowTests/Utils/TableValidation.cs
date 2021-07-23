using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTests.Utils
{
    public static class TableValidation
    {
        private static readonly MethodInfo IsMemberMatchingToColumnNameInfo;
        private static readonly MethodInfo IsMatchingAliasInfo;

        //Columns die mit # anfangen sind Kommentare. Zum Beispiel #Kommentar. Diese Column muss nicht zu einem Property gemappt werden.
        private static readonly string[] ColumnRegexes =
        {
            "^#"
        };

        static TableValidation()
        {
            var assembly = typeof(TableHelperExtensionMethods).Assembly;
            var type = assembly.GetType("TechTalk.SpecFlow.Assist.TEHelpers");
            IsMemberMatchingToColumnNameInfo = type.GetMethod("IsMemberMatchingToColumnName", BindingFlags.NonPublic | BindingFlags.Static);
            IsMatchingAliasInfo = type.GetMethod("IsMatchingAlias", BindingFlags.NonPublic | BindingFlags.Static);
        }

        public static IEnumerable<TRow> CreateSetValidated<TRow>(this Table table)
        {
            if (table.RowCount == 0) throw new NotSupportedException("An empty table is not supported.");
            if (!ValidateColumns<TRow>(table, out var unknownColumns)) throw new NotSupportedException($"The provided table contains the following unknown columns: {string.Join(", ", unknownColumns)}");
            return table.CreateSet<TRow>();
        }

        public static TObject CreateInstanceValidated<TObject>(this Table table)
        {
            ValidateHasSingleRow(table);
            if (!ValidateColumns<TObject>(table, out var unknownRows)) throw new NotSupportedException($"The provided table contains the following rows, which could not be matched to any property of {typeof(TObject)} : {string.Join(", ", unknownRows)}");
            return table.CreateInstance<TObject>();
        }

        [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Global")]
        public static void ValidateHasSingleRow(this Table table)
        {
            if (table.RowCount != 1) throw new NotSupportedException("A table with exactly one row is required.");
        }

        private static bool ValidateColumns<TRow>(this Table table, out IEnumerable<string> unknownColumns)
        {
            var properties = typeof(TRow).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            unknownColumns = table.Header.Where(header => !IsValidColumnName(header, properties));
            return !unknownColumns.Any();
        }

        private static bool IsValidColumnName(this string columnName, PropertyInfo[] rowPropertyInfos)
        {
            return ColumnRegexes.Any(regex => Regex.IsMatch(columnName, regex))
                   || rowPropertyInfos.Any(propertyInfo => propertyInfo.IsMatchingToColumnName(columnName))
                   || rowPropertyInfos.Any(propertyInfo => propertyInfo.HasMatchingAlias(columnName));
        }

        private static bool IsMatchingToColumnName(this PropertyInfo propertyInfo, string columnName)
        {
            var result = (bool)IsMemberMatchingToColumnNameInfo.Invoke(null, new object[] { propertyInfo, columnName });
            return result;
        }

        private static bool HasMatchingAlias(this PropertyInfo propertyInfo, string columnName)
        {
            var result = (bool)IsMatchingAliasInfo.Invoke(null, new object[] { propertyInfo, columnName });
            return result;
        }
    }
}
