using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace SpecFlowTests.Utils.Common.StepArgumentTransformations
{
    [Binding]
    public class StringToIEnumerableStringStepArgumentTransformation
    {
        private const string ValidierungsmeldungenSeparator = "::";

        [StepArgumentTransformation]
        public IEnumerable<string> TransformToStringArray(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new NotSupportedException($"'Empty' entry to '{nameof(String)}[]' transformation is not supported.");
            }

            return value.Split(new[] {ValidierungsmeldungenSeparator}, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.TrimToNull())
                .ToList();
        }
    }
}
