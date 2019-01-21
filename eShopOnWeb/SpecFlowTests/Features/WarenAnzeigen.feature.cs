// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:3.0.0.0
//      SpecFlow Generator Version:3.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace SpecFlowTests.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class AnzeigenDerWarenAufHauptseiteFeature : Xunit.IClassFixture<AnzeigenDerWarenAufHauptseiteFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "WarenAnzeigen.feature"
#line hidden
        
        public AnzeigenDerWarenAufHauptseiteFeature(AnzeigenDerWarenAufHauptseiteFeature.FixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("de-DE"), "Anzeigen der Waren auf Hauptseite", null, ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 3
#line 4
 testRunner.Given("eine leere in-Memory Datenbank", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Gegeben sei ");
#line hidden
        }
        
        void System.IDisposable.Dispose()
        {
            this.ScenarioTearDown();
        }
        
        [Xunit.FactAttribute(DisplayName="Es gibt 8 Waren, alle sollten angezeigt werden")]
        [Xunit.TraitAttribute("FeatureTitle", "Anzeigen der Waren auf Hauptseite")]
        [Xunit.TraitAttribute("Description", "Es gibt 8 Waren, alle sollten angezeigt werden")]
        public virtual void EsGibt8WarenAlleSolltenAngezeigtWerden()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Es gibt 8 Waren, alle sollten angezeigt werden", "\tMit standarte Einstellungen - 10 Waren pro Seite", ((string[])(null)));
#line 8
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 3
this.FeatureBackground();
#line 11
 testRunner.Given("es 8 Waren in dem Katalog gibt", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Angenommen ");
#line 13
 testRunner.When("ich die erste Seite von Waren anfordere", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wenn ");
#line 15
 testRunner.Then("sollten alle diese Waren angezeigt werden", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dann ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.TheoryAttribute(DisplayName="Es gibt nicht mehr als 10 Waren, alle sollen angezeigt werden")]
        [Xunit.TraitAttribute("FeatureTitle", "Anzeigen der Waren auf Hauptseite")]
        [Xunit.TraitAttribute("Description", "Es gibt nicht mehr als 10 Waren, alle sollen angezeigt werden")]
        [Xunit.InlineDataAttribute("0", new string[0])]
        [Xunit.InlineDataAttribute("8", new string[0])]
        [Xunit.InlineDataAttribute("10", new string[0])]
        public virtual void EsGibtNichtMehrAls10WarenAlleSollenAngezeigtWerden(string warenanzahl, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Es gibt nicht mehr als 10 Waren, alle sollen angezeigt werden", "\tMit standarte Einstellungen - 10 Waren pro Seite", exampleTags);
#line 19
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 3
this.FeatureBackground();
#line 22
 testRunner.Given(string.Format("es {0} Waren in dem Katalog gibt", warenanzahl), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Angenommen ");
#line 24
 testRunner.When("ich die erste Seite von Waren anfordere", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wenn ");
#line 26
 testRunner.Then("sollten alle diese Waren angezeigt werden", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dann ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.TheoryAttribute(DisplayName="Es gibt mehr als 10 Waren, zusätzliche sollen auf nächsten Seiten landen")]
        [Xunit.TraitAttribute("FeatureTitle", "Anzeigen der Waren auf Hauptseite")]
        [Xunit.TraitAttribute("Description", "Es gibt mehr als 10 Waren, zusätzliche sollen auf nächsten Seiten landen")]
        [Xunit.InlineDataAttribute("8", "2", "0", new string[0])]
        [Xunit.InlineDataAttribute("11", "2", "1", new string[0])]
        [Xunit.InlineDataAttribute("15", "2", "5", new string[0])]
        [Xunit.InlineDataAttribute("20", "2", "10", new string[0])]
        [Xunit.InlineDataAttribute("21", "2", "10", new string[0])]
        [Xunit.InlineDataAttribute("21", "3", "1", new string[0])]
        public virtual void EsGibtMehrAls10WarenZusatzlicheSollenAufNachstenSeitenLanden(string warenanzahl, string seite, string anzahlAufgelisteterWaren, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Es gibt mehr als 10 Waren, zusätzliche sollen auf nächsten Seiten landen", null, exampleTags);
#line 36
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 3
this.FeatureBackground();
#line 38
 testRunner.Given(string.Format("es {0} Waren in dem Katalog gibt", warenanzahl), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Angenommen ");
#line 40
  testRunner.And("es bis 10 Waren auf einer Seite gibt", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Und ");
#line 42
 testRunner.When(string.Format("ich Seite {0} von Waren anfordere", seite), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wenn ");
#line 44
 testRunner.Then(string.Format("sollen {0} Waren angezeigt werden", anzahlAufgelisteterWaren), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dann ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.TheoryAttribute(DisplayName="Es gibt mehr als 10 Waren, zusätzliche sollen auf nächsten Seiten landen. Untersc" +
            "hiedliche Seitengroßen")]
        [Xunit.TraitAttribute("FeatureTitle", "Anzeigen der Waren auf Hauptseite")]
        [Xunit.TraitAttribute("Description", "Es gibt mehr als 10 Waren, zusätzliche sollen auf nächsten Seiten landen. Untersc" +
            "hiedliche Seitengroßen")]
        [Xunit.InlineDataAttribute("8", "2", "3", "5", new string[0])]
        [Xunit.InlineDataAttribute("8", "2", "2", "2", new string[0])]
        [Xunit.InlineDataAttribute("8", "2", "0", "8", new string[0])]
        public virtual void EsGibtMehrAls10WarenZusatzlicheSollenAufNachstenSeitenLanden_UnterschiedlicheSeitengroBen(string warenanzahl, string seite, string anzahlAufgelisteterWaren, string warenProSeite, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Es gibt mehr als 10 Waren, zusätzliche sollen auf nächsten Seiten landen. Untersc" +
                    "hiedliche Seitengroßen", null, exampleTags);
#line 57
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 3
this.FeatureBackground();
#line 59
 testRunner.Given(string.Format("es {0} Waren in dem Katalog gibt", warenanzahl), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Angenommen ");
#line 61
  testRunner.And(string.Format("es bis {0} Waren auf einer Seite gibt", warenProSeite), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Und ");
#line 63
 testRunner.When(string.Format("ich Seite {0} von Waren anfordere", seite), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wenn ");
#line 65
 testRunner.Then(string.Format("sollen {0} Waren angezeigt werden", anzahlAufgelisteterWaren), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dann ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.0.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                AnzeigenDerWarenAufHauptseiteFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                AnzeigenDerWarenAufHauptseiteFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
