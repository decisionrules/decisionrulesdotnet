﻿using DecisionRules.Exceptions;
using NUnit.Framework;
using System.Collections.Generic;
using FakeItEasy;
using System.Threading.Tasks;
using static DecisionRules.Model.SolverStragiesEnum;
using static DecisionRules.Model.GeoLocationsEnum;

namespace DecisionRules.Tests
{
    [TestFixture]
    class DecisionRulesTest
    {
        private static RequestOption requestOption = A.Fake<RequestOption>(x => x.WithArgumentsForConstructor(() => new RequestOption("mockey", GeoLocations.DEFAULT)));
        private static DecisionRulesService drs = A.Fake<DecisionRulesService>(x => x.WithArgumentsForConstructor(() => new DecisionRulesService(requestOption)));

        [Test]
        public void Called_With_Generic_Request_Should_Return_List()
        {

            A.CallTo(() => drs.Solve<InputData, ResultModel>(A<string>.Ignored, new InputData(), A<SolverStrategies>.Ignored, A<string>.Ignored)).Returns(new List<ResultModel>());

            var result = drs.Solve<InputData, ResultModel>("ruleID", new InputData(), SolverStrategies.STANDARD, "1").Result;

            Assert.AreEqual(new List<ResultModel>(), result);
        }

        [Test]
        public void Called_With_String_Request_Should_Return_List()
        {
            A.CallTo(() => drs.Solve<ResultModel>(A<string>.Ignored, A<string>.Ignored, A<SolverStrategies>.Ignored, A<string>.Ignored)).Returns(new List<ResultModel>());

            var result = drs.Solve<InputData, ResultModel>("ruleID", new InputData(), SolverStrategies.STANDARD, "1").Result;

            Assert.AreEqual(new List<ResultModel>(), result);
        }

        [Test]
        public void Called_Wrong_APIKEY_Should_Throw_NotPublishException()
        {
            A.CallTo(() => drs.Solve<ResultModel>(A<string>.Ignored, A<string>.Ignored, A<SolverStrategies>.Ignored, A<string>.Ignored)).Throws<NotPublishedException>();

            var result = drs.Solve<InputData, ResultModel>("ruleID", new InputData(), SolverStrategies.STANDARD, "1").Result;

            Assert.Throws<NotPublishedException>(delegate { throw new NotPublishedException(); });
        }

        [Test]
        public void Called_Wrong_APIKEY_Should_Throw_NotUserException()
        {
            A.CallTo(() => drs.Solve<ResultModel>(A<string>.Ignored, A<string>.Ignored, A<SolverStrategies>.Ignored, A<string>.Ignored)).Throws<NoUserException>();

            var result = drs.Solve<InputData, ResultModel>("ruleID", new InputData(), SolverStrategies.STANDARD, "1").Result;

            Assert.Throws<NoUserException>(delegate { throw new NoUserException(); });
        }

        [Test]
        public void Should_Throw_TooManyApiCalls()
        {
            A.CallTo(() => drs.Solve<ResultModel>(A<string>.Ignored, A<string>.Ignored, A<SolverStrategies>.Ignored, A<string>.Ignored)).Throws<TooManyApiCallsException>();

            var result = drs.Solve<InputData, ResultModel>("ruleID", new InputData(), SolverStrategies.STANDARD, "1").Result;

            Assert.Throws<TooManyApiCallsException>(delegate { throw new TooManyApiCallsException(); });
        }

        [Test]
        public void Should_Throw_ServerErrorException()
        {
            A.CallTo(() => drs.Solve<ResultModel>(A<string>.Ignored, A<string>.Ignored, A<SolverStrategies>.Ignored, A<string>.Ignored)).Throws<ServerErrorException>();

            var result = drs.Solve<InputData, ResultModel>("ruleID", new InputData(), SolverStrategies.STANDARD, "1").Result;

            Assert.Throws<ServerErrorException>(delegate { throw new ServerErrorException(); });
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
