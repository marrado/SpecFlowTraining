using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace SpecFlowTests.Utils.Common.StepArgumentTransformations
{
    [Binding]
    public class StringToShortArrayStepArgumentTransformation
    {
        [StepArgumentTransformation]
        public short[] TransformToShortArray(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new NotSupportedException($"'Empty' entry to '{nameof(Int16)}[]' transformation is not supported.");
            }

            return value.Split(',')
                        .Select(s => s.TrimToNull())
                        .Select(s => MapDriver.IsSet(s) ? short.Parse(s) : default)
                        .ToArray();
        }
    }
}
