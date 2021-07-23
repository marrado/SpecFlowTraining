using SpecFlowTests.Utils.Common.ValueRetrievers;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TechTalk.SpecFlow.Assist.ValueRetrievers;

namespace SpecFlowTests.Setup
{
    [Binding]
    public class ValueRetrieversSetup
    {
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            OverrideValueRetrievers();
        }

        private static void OverrideValueRetrievers()
        {
            Service.Instance.ValueRetrievers.Replace<EnumValueRetriever, StringToEnumValueRetriever>();
            Service.Instance.ValueRetrievers.Replace<BoolValueRetriever, StringToBoolValueRetriever>();
            Service.Instance.ValueRetrievers.Replace<StringValueRetriever, ReplacesMagicStringsValueRetriever>();
            Service.Instance.ValueRetrievers.Replace<DateTimeValueRetriever, StringToDateTimeValueRetriever>();
            Service.Instance.ValueRetrievers.Replace<DecimalValueRetriever, StringToPercentageValueRetriever>();

            Service.Instance.ValueRetrievers
                .Register<StringToNullableDateTimeValueRetriever>();
            Service.Instance.ValueRetrievers
                .Replace<DateTimeOffsetValueRetriever, StringToDateTimeOffsetValueRetriever>();
            Service.Instance.ValueRetrievers
                .Register<StringToNullableDateTimeOffsetValueRetriever>();
            Service.Instance.ValueRetrievers
                .Register<NullableStringToPercentageValueRetriever>();
            Service.Instance.ValueRetrievers
                .Register<NullableIntValueRetriever>();
            Service.Instance.ValueRetrievers.Register<StringToNullableBoolValueRetriever>();
            Service.Instance.ValueRetrievers.Register<StringToEnumerableEnumValueRetriever>();
            Service.Instance.ValueRetrievers.Register<StringToEnumerableValueRetriever<decimal>>();
        }
    }
}