namespace SETest.Tests.Services
{
    using NUnit.Framework;
    using SETest.Services;
    using SETest.Services.Interface;

    [TestFixture]
    public class LiveNationServiceTest
    {
        [Test]
        [TestCase(3)]
        [TestCase(6)]
        [TestCase(9)]
        [TestCase(12)]
        public void ApplyLiveRuleEngine(int number)
        {
            IRuleValidator liveRuleValidator = new LiveRuleValidator();
            var validatorResponse = liveRuleValidator.ApplyRule(number);
            Assert.AreEqual(true, validatorResponse);
        }

        [Test]
        [TestCase(5)]
        [TestCase(10)]
        public void ApplyNationRuleEngine(int number)
        {
            IRuleValidator liveRuleValidator = new NationRuleValidator();
            var validatorResponse = liveRuleValidator.ApplyRule(number);
            Assert.AreEqual(true, validatorResponse);
        }

        [Test]
        [TestCase(15)]
        public void ApplyLiveNationRuleEngine(int number)
        {
            IRuleValidator liveRuleValidator = new LiveNationRuleValidator();
            var validatorResponse = liveRuleValidator.ApplyRule(number);
            Assert.AreEqual(true, validatorResponse);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(4)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(11)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(16)]
        [TestCase(17)]
        [TestCase(19)]
        public void ApplyDefaultRuleEngine(int number)
        {
            IRuleValidator liveRuleValidator = new LiveRuleValidator();
            var validatorResponse = liveRuleValidator.ApplyRule(number);
            Assert.AreEqual(false, validatorResponse);

            IRuleValidator nationRuleValidator = new NationRuleValidator();
            var nationValidatorResponse = nationRuleValidator.ApplyRule(number);
            Assert.AreEqual(false, nationValidatorResponse);

            IRuleValidator liveNationRuleValidator = new LiveNationRuleValidator();
            var liveNationValidatorResponse = liveNationRuleValidator.ApplyRule(number);
            Assert.AreEqual(false, liveNationValidatorResponse);
        }
    }
}
