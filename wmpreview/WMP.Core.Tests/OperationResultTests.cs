using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WMP.Core.Tests
{
    [TestClass]
    public class OperationResultTests : CoreUnitTest
    {
        private OperationResult _target;

        #region Setup / Teardown
        [TestInitialize]
        public void TestInitialize()
        {
            _target = new OperationResult();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _target = null;
        }
        #endregion

        #region Constructor tests

        [TestMethod]
        public void Constructor_ParameterlessIsNotNull_AndSetsCodeToUnknown()
        {
            //Arrange
            const ResultCode expectedInitialResultCode = ResultCode.Unknown;

            //Act
            var result = new OperationResult();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedInitialResultCode, result.Code);
            result.AssertMessagesAreEmpty();
        }

        [TestMethod]
        public void Constructor_WithMessage_AddsMessageToList()
        {

            //Arrange
            const ResultCode expectedInitialResultCode = ResultCode.Unknown;
            const string testMessage = "test message";

            //Act
            var result = new OperationResult(testMessage);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedInitialResultCode, result.Code);
            Assert.IsNotNull(result.Messages);
            Assert.AreEqual(testMessage, result.Message);
            Assert.AreEqual(1, result.Messages.Count);
        }

        [TestMethod]
        public void Constructor_WithCode_SetsCodeAndEmptyMessage()
        {
            //Arrange
            const ResultCode expectedInitialResultCode = ResultCode.Success;

            //Act
            var result = new OperationResult(expectedInitialResultCode);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedInitialResultCode, result.Code);
            result.AssertMessagesAreEmpty();
        }

        [TestMethod]
        public void Constructor_WithCodeAndMessage_SetsCodeAndMessages()
        {
            //Arrange
            const ResultCode expectedInitialResultCode = ResultCode.Success;
            const string testMessage = "test message";

            //Act
            var result = new OperationResult(testMessage, expectedInitialResultCode);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedInitialResultCode, result.Code);
            Assert.IsNotNull(result.Messages);
            Assert.IsNotNull(result.Messages);
            Assert.AreEqual(testMessage, result.Message);
            Assert.AreEqual(1, result.Messages.Count);
        }

        [TestMethod]
        public void Constructor_NullMessage_DoesNotAddMessage()
        {
            //Arrange
            const ResultCode expectedInitialResultCode = ResultCode.Unknown;
            const string testMessage = null;

            //Act
            var result = new OperationResult(testMessage);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedInitialResultCode, result.Code);
            result.AssertMessagesAreEmpty();
        }

        #endregion

        #region Code Test

        [TestMethod]
        public void Code_Set_SetsValue()
        {
            //Arrange
            const ResultCode testValue = ResultCode.Success;

            //Act
            _target.Code = testValue;

            //Assert
            Assert.AreEqual(testValue, _target.Code);
        }

        #endregion

        #region OK Tests
        [TestMethod]
        public void Ok_DefaultState_IsFalse()
        {
            //Arrange
            const ResultCode expectedCode = ResultCode.Unknown;
            //Act
            var result = _target.Ok;

            //Assert
            Assert.AreEqual(expectedCode, _target.Code);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Ok_SetAsSuccessful_IsTrue()
        {
            //Arrange
            const ResultCode initialCode = ResultCode.Unknown;
            //Act
            _target.SetAsSuccessful();

            //Assert
            Assert.AreNotEqual(initialCode, _target.Code);
            Assert.IsTrue(_target.Ok);
        }
        [TestMethod]
        public void Ok_SetAsFailed_IsFalse()
        {
            //Arrange
            const ResultCode initialCode = ResultCode.Unknown;
            //Act
            _target.SetAsFailed("testKey", "test message");

            //Assert
            Assert.AreNotEqual(initialCode, _target.Code);
            Assert.IsFalse(_target.Ok);
        }

        #endregion

        #region Failed Tests
        [TestMethod]
        public void Failed_DefaultState_IsFalse()
        {
            //Arrange
            const ResultCode expectedCode = ResultCode.Unknown;
            //Act
            var result = _target.Failed;

            //Assert
            Assert.AreEqual(expectedCode, _target.Code);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void Failed_SetAsSuccessful_IsFalse()
        {
            //Arrange
            const ResultCode initialCode = ResultCode.Unknown;
            //Act
            _target.SetAsSuccessful();

            //Assert
            Assert.AreNotEqual(initialCode, _target.Code);
            Assert.IsFalse(_target.Failed);
        }

        [TestMethod]
        public void Failed_SetAsFailed_IsTrue()
        {
            //Arrange
            const ResultCode initialCode = ResultCode.Unknown;
            //Act
            _target.SetAsFailed("testmessage", "test message");

            //Assert
            Assert.AreNotEqual(initialCode, _target.Code);
            Assert.IsTrue(_target.Failed);
        }
        #endregion

        #region SetAsFailed Tests

        [TestMethod]
        public void SetAsFailed_SingleKeyValue_SetsMessageAndFailureCode()
        {
            //Arrange
            const ResultCode expectedCode = ResultCode.Failure;
            const string testKey = "testkey";
            const string testMessage = "test message";

            //Act
            _target.SetAsFailed(testKey, testMessage);

            //Assert
            Assert.AreEqual(expectedCode, _target.Code);
            Assert.AreEqual(1, _target.Messages.Count);

            var firstMessage = _target.Messages.First();
            Assert.AreEqual(testKey, firstMessage.Key);
            Assert.AreEqual(testMessage, firstMessage.Value);
        }

        [TestMethod]
        public void SetAsFailed_SingleKeyNullValue_SetsFailureCodeNoMessages()
        {
            //Arrange
            const ResultCode expectedCode = ResultCode.Failure;
            const string testKey = "testkey";
            const string testMessage = null;

            //Act
            _target.SetAsFailed(testKey, testMessage);

            //Assert
            Assert.AreEqual(expectedCode, _target.Code);
            Assert.AreEqual(0, _target.Messages.Count);        
        }

        [TestMethod]
        public void SetAsFailed_ListOfMessages_SetsMessages()
        {
            //Arrange
            const ResultCode expectedCode = ResultCode.Failure;
            var testMessages = new Dictionary<string, string>
                                                      {
                                                              {"test1", "test one"},
                                                              {"test2", "test two"}
                                                      };

            //Act
            _target.SetAsFailed(testMessages);

            //Assert
            Assert.AreEqual(expectedCode, _target.Code);
            Assert.AreEqual(testMessages.Count, _target.Messages.Count);

            foreach (var key in testMessages.Keys)
            {
                Assert.IsTrue(_target.Messages.ContainsKey(key));
            }
        }

        #endregion

        #region SetAsSuccessful Tests

        [TestMethod]
        public void SetAsSuccessful_NoParameters_SetsCodeToSuccess()
        {
            //Arrange
            const ResultCode expectedCode = ResultCode.Success;

            //Act
            _target.SetAsSuccessful();

            //Assert
            Assert.AreEqual(expectedCode, _target.Code);
            _target.AssertMessagesAreEmpty();
        }


        #endregion

    }
    public static partial class OperationResultTestHelpers
    {
        public static void AssertMessagesAreEmpty(this OperationResult result)
        {
            Assert.IsNotNull(result.Messages);
            Assert.AreEqual(string.Empty, result.Message);
            Assert.AreEqual(0, result.Messages.Count);
        }

    }
}