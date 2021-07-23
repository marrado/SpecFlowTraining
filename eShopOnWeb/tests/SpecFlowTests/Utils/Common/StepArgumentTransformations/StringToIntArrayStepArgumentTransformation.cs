using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace SpecFlowTests.Utils.Common.StepArgumentTransformations
{
    [Binding]
    public class StringToIntArrayStepArgumentTransformation
    {
        [StepArgumentTransformation]
        public int[] TransformToIntArray(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new NotSupportedException($"'Empty' entry to '{nameof(Int32)}[]' transformation is not supported.");
            }

            return value.Split(',')
                        .Select(s => s.TrimToNull())
                        .Select(s => MapDriver.IsSet(s) ? int.Parse(s) : default)
                        .ToArray();
        }
    }
}
