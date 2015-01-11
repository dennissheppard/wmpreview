using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WMP.Core.Tests
{
    [TestClass]
    public class OperationResultOfTTests : CoreUnitTest
    {
        public class TargetObject
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        private OperationResult<TargetObject> _target;        

        #region Setup / Teardown
        [TestInitialize]
        public void TestInitialize()
        {
            _target = new OperationResult<TargetObject>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _target = null;
        }
        #endregion

        #region Constructor tests

        [TestMethod]
        public void Constructor_ValidTargetObject_SetsResultAndSucces()
        {
            //Arrange
            const ResultCode expectedInitialResultCode = ResultCode.Success;
            var testTarget = GetTestTargetObject();
            //Act
            var result = new OperationResult<TargetObject>(testTarget);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreSame(testTarget, result.Result);
            Assert.AreEqual(expectedInitialResultCode, result.Code);
            result.AssertMessagesAreEmpty();
        }

        #endregion   

        #region SetAsFailed Tests



        [TestMethod]
        public void SetAsFailed_SingleStringAndTarget_SetsFailureMessageAndResult()
        {
            //Arrange
            const ResultCode expectedCode = ResultCode.Failure;
            const string testMessage = "Error message";
            var testTarget = GetTestTargetObject();

            //Act
            _target.SetAsFailed(testMessage, testTarget);

            //Assert
            Assert.AreEqual(expectedCode, _target.Code);
            Assert.AreSame(testTarget, _target.Result);
            Assert.AreEqual(1, _target.Messages.Count);
        }

        [TestMethod]
        public void SetAsFailed_SingleString_SetsFailureAndAddsMessages()
        {
            //Arrange
            const ResultCode expectedCode = ResultCode.Failure;
            const string testMessage = "Error message";

            //Act
            _target.SetAsFailed(testMessage);

            //Assert
            Assert.AreEqual(expectedCode, _target.Code);
            Assert.AreEqual(1, _target.Messages.Count);        
        }

        #endregion

        #region SetAsSuccessful Tests

        [TestMethod]
        public void SetAsSuccessful_NoParameters_SetsCodeToSuccessAndItemNotFoundMessage()
        {
            //Arrange
            const ResultCode expectedCode = ResultCode.Success;
            const string expectedMessage = "Item not found";

            //Act
            _target.SetAsSuccessful();

            //Assert
            Assert.AreEqual(expectedCode, _target.Code);
            Assert.AreEqual(expectedMessage, _target.Message);
        }

        [TestMethod]
        public void SetAsSuccessful_ItemAndMessage_SetsMessageAndResult()
        {
            //Arrange
            const ResultCode expectedCode = ResultCode.Success;
            const string expectedMessage = "Item not found";
            var testItem = GetTestTargetObject();

            //Act
            _target.SetAsSuccessful(expectedMessage, testItem);

            //Assert
            Assert.AreEqual(expectedCode, _target.Code);
            Assert.AreSame(testItem, _target.Result);
            Assert.AreEqual(expectedMessage, _target.Message);
            Assert.AreEqual(1, _target.Messages.Count);
        }

        #endregion

        #region OkWithItem Tests

        [TestMethod]
        public void OkWithItem_NoItem_ReturnsFalse()
        {
            //Arrange
            _target.SetAsSuccessful();

            //Act
            var actual = _target.OkWithItem;

            //Assert
            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void OkWithItem_FailureAndNoItem_ReturnsFalse()
        {
            //Arrange
            SetTargetToFail();

            //Act
            var actual = _target.OkWithItem;

            //Assert
            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void OkWithItem_FailureWithItem_ReturnsFalse()
        {
            //Arrange
            _target.SetAsFailed("test fail", GetTestTargetObject());

            //Act
            var actual = _target.OkWithItem;

            //Assert
            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void OkWithItem_DefaultAndNoItem_ReturnsFalse()
        {
            //Arrange

            //Act
            var actual = _target.OkWithItem;

            //Assert
            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void OkWithItem_DefaultWithItem_ReturnsFalse()
        {
            //Arrange
            _target.Result = GetTestTargetObject();
            //Act
            var actual = _target.OkWithItem;

            //Assert
            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void OkWithItem_SuccessAndItem_ReturnsTrue()
        {
            //Arrange
            _target.SetAsSuccessful(item: GetTestTargetObject());

            //Act
            var actual = _target.OkWithItem;

            //Assert
            Assert.IsTrue(actual);
        }

        #endregion

        #region OkWithNoItem Tests

        [TestMethod]
        public void OkWithNoItem_NoItem_ReturnsTrue()
        {
            //Arrange
            _target.SetAsSuccessful();

            //Act
            var actual = _target.OkWithNoItem;

            //Assert
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void OkWithNoItem_FailureAndNoItem_ReturnsFalse()
        {
            //Arrange
            SetTargetToFail();

            //Act
            var actual = _target.OkWithNoItem;

            //Assert
            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void OkWithNoItem_FailureWithItem_ReturnsFalse()
        {
            //Arrange
            _target.SetAsFailed("test fail", GetTestTargetObject());

            //Act
            var actual = _target.OkWithNoItem;

            //Assert
            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void OkWithNoItem_DefaultAndNoItem_ReturnsFalse()
        {
            //Arrange

            //Act
            var actual = _target.OkWithNoItem;

            //Assert
            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void OkWithNoItem_DefaultWithItem_ReturnsFalse()
        {
            //Arrange
            _target.Result = GetTestTargetObject();
            //Act
            var actual = _target.OkWithNoItem;

            //Assert
            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void OkWithNoItem_SuccessAndItem_ReturnsFalse()
        {
            //Arrange
            _target.SetAsSuccessful(item: GetTestTargetObject());

            //Act
            var actual = _target.OkWithNoItem;

            //Assert
            Assert.IsFalse(actual);
        }

        #endregion

        #region GetResultIfSuccessful Tests

        [TestMethod]
        public void GetResultIfSuccessful_IfFailed_ReturnsNull()
        {
            //Arrange
            SetTargetToFail();

            //Act
            var actual = _target.GetResultIfSuccessful();

            //Assert
            Assert.IsNull(actual);
        }
        [TestMethod]
        public void GetResultIfSuccessful_IfSuccessWithNoItem_ReturnsNull()
        {
            //Arrange
            _target.SetAsSuccessful();

            //Act
            var actual = _target.GetResultIfSuccessful();

            //Assert
            Assert.IsNull(actual);
        }
        [TestMethod]
        public void GetResultIfSuccessful_IfSuccessWithItem_ReturnsItem()
        {
            //Arrange
            var expectedObject = GetTestTargetObject();
            _target.SetAsSuccessful(null, expectedObject);

            //Act
            var actual = _target.GetResultIfSuccessful();

            //Assert
            Assert.AreSame(expectedObject, actual);
        }
        

        #endregion

        #region Private helpers
        private static TargetObject GetTestTargetObject()
        {
            return new TargetObject { Id = 1, Name = "Test" };
        }

        private void SetTargetToFail()
        {
            _target.SetAsFailed("test fail");
        }
        #endregion
    }
    public static partial class OperationResultTestHelpers
    {       
        public static void AssertMessagesAreEmpty<T>(this OperationResult<T> result) where T : class
        {
            Assert.IsNotNull(result.Messages);
            Assert.AreEqual(string.Empty, result.Message);
            Assert.AreEqual(0, result.Messages.Count);
        }

    }
}