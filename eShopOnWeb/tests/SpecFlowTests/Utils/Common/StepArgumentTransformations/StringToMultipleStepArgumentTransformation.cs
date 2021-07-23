using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace SpecFlowTests.Utils.Common.StepArgumentTransformations
{
    [Binding]
    public class StringToMultipleStepArgumentTransformation
    {
        [StepArgumentTransformation]
        public string[] TransformStringToMultiple(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new NotSupportedException("'Empty' entry to 'string[]' transformation is not supported.");
            }

            return MapDriver.MapSingleRowCellEntryToMultiple(value).ToArray();
        }
    }
}
