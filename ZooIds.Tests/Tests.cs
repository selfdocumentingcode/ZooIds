using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ZooIds.Tests
{
    [TestClass]
    public class Tests
    {
        private ZooIdGenerator _sut;

        private const int _testSeed = 1;

        [TestMethod]
        public void Generate_ConfigHasMoreThan10Adjectives_ThrowArgumentException()
        {
            var config = GeneratorConfig.Default;
            config.NumAdjectives = 11;

            _sut = new ZooIdGenerator(config, _testSeed);

            try
            {
                var id = _sut.GenerateId();

                Assert.Fail();
            }
            catch (ArgumentException) { }
        }

        [DataTestMethod]
        [DataRow((uint)0)]
        [DataRow((uint)5)]
        public void Generate_ConfigHasNumAdjectives_IdHasCorrectNumberOfAdjectives(uint inputNumAdjectives)
        {
            var config = GeneratorConfig.Default;
            config.NumAdjectives = inputNumAdjectives;

            _sut = new ZooIdGenerator(config, _testSeed);

            var id = _sut.GenerateId();

            // X Adjectives + 1 Animal
            var expectedWordCount = inputNumAdjectives + 1;

            Assert.IsTrue(id.Split("-").Length == expectedWordCount);
        }

        [TestMethod]
        public void Generate_ConfigHasDelimiter_IdHasCorrectDelimiter()
        {
            var config = GeneratorConfig.Default;
            config.Delimiter = "#";

            _sut = new ZooIdGenerator(config, _testSeed);

            var id = _sut.GenerateId();

            // everlasting-cavernous-jay
            Assert.IsTrue(id.Split("#").Length == 3);
        }

        [DataTestMethod]
        [DataRow(CaseStyle.LowerCase, "everlasting-cavernous-jay")]
        [DataRow(CaseStyle.UpperCase, "EVERLASTING-CAVERNOUS-JAY")]
        [DataRow(CaseStyle.TitleCase, "Everlasting-Cavernous-Jay")]
        [DataRow(CaseStyle.ToggleCase, "EvErLaStInG-CaVeRnOuS-JaY")]        
        public void Generate_ConfigHasCaseStyle_IdHasRightCaseStyle(CaseStyle inputCaseStyle, string expectedResult)
        {
            var config = GeneratorConfig.Default;
            config.CaseStyle = inputCaseStyle;

            _sut = new ZooIdGenerator(config, _testSeed);

            var id = _sut.GenerateId();

            Assert.IsTrue(id.Equals(expectedResult));
        }

        [TestMethod]
        public void Generate_DifferentSeeds_IdsAreDifferent()
        {
            var sut1 = new ZooIdGenerator(6);
            var sut2 = new ZooIdGenerator(9);

            var id1 = sut1.GenerateId();
            var id2 = sut2.GenerateId();

            Assert.IsFalse(id1.Equals(id2));
        }
    }
}
