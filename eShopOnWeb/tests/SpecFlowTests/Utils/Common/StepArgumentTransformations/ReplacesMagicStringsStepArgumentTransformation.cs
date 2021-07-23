using SpecFlowTests.Utils.Common.Converter;
using TechTalk.SpecFlow;

namespace SpecFlowTests.Utils.Common.StepArgumentTransformations
{
    [Binding]
    public class ReplacesMagicStringsStepArgumentTransformation
    {
        [StepArgumentTransformation]
        public string StringTransform(string result)
        {
            return result.ReplaceMagicStrings();
        }
    }
}
