using System;
using System.Collections.Generic;
using SpecFlowTests.Utils.Common.Converter;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowTests.Utils.Common.ValueRetrievers
{
    /// <summary>
    /// Beispiel:
    /// - Angabe im Feature als '#2zeichen' wird umgewandelt in den string 'XX'
    /// - Angabe im Feature als '#7zeichen' wird umgewandelt in den string 'XXXXXXX'
    /// Eine genaue Beschreibung zu den verschiedenen MagicStrings findet man hier: https://confluence.intern.fsw.at/pages/viewpage.action?pageId=65486210
    /// </summary>
    public class ReplacesMagicStringsValueRetriever : IValueRetriever
    {
        private static string GetValue(KeyValuePair<string, string> result)
        {
            return result.Value.ReplaceMagicStrings();
        }

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return GetValue(keyValuePair);
        }

        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return propertyType == typeof(string);
        }
    }
}