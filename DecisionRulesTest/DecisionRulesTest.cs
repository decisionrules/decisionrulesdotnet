using DecisionRules.Exceptions;
using NUnit.Framework;
using System.Collections.Generic;
using FakeItEasy;
using System.Threading.Tasks;

namespace DecisionRules.Tests
{
    [TestFixture]
    class DecisionRulesTest
    {
        private static RequestOption requestOption = A.Fake<RequestOption>(x => x.WithArgumentsForConstructor(() => new RequestOption("mockey", "eu1")));
        private static DecisionRulesService drs = A.Fake<DecisionRulesService>(x => x.WithArgumentsForConstructor(() => new DecisionRulesService(requestOption)));

        [Test]
        public void Called_With_Generic_Request_Should_Return_List()
        {

            A.CallTo(() => drs.Solve<InputData, ResultModel>(A<string>.Ignored, new InputData(), A<string>.Ignored)).Returns(new List<ResultModel>());

            var result = drs.Solve<InputData, ResultModel>("ruleID", new InputData(), "1").Result;

            Assert.AreEqual(new List<ResultModel>(), result);
        }

        [Test]
        public void Called_With_String_Request_Should_Return_List()
        {
            A.CallTo(() => drs.Solve<ResultModel>(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored)).Returns(new List<ResultModel>());

            var result = drs.Solve<InputData, ResultModel>("ruleID", new InputData(), "1").Result;

            Assert.AreEqual(new List<ResultModel>(), result);
        }

        [Test]
        public void Called_Wrong_APIKEY_Should_Throw_NotPublishException()
        {
            A.CallTo(() => drs.Solve<ResultModel>(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored)).Throws<NotPublishedException>();

            var result = drs.Solve<InputData, ResultModel>("ruleID", new InputData(), "1").Result;

            Assert.Throws<NotPublishedException>(delegate { throw new NotPublishedException(); });
        }

    }
    class InputData
    {
        public string day { get; set; }
    }

    class ResultModel
    {
        public string result { get; set; }
    }
}
