using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WMP.EFDalKit.Tests
{
    [TestClass]
    public class DataPagingSpecifierTest
    {
        #region Nested type: ConstructorTest

        [TestClass]
        public class ConstructorTest
        {
            [TestMethod]
            public void Constructor_NoArguments_IsNotNull()
            {
                //Arrange

                //Act
                var target = new DataPagingSpecifier();

                //Assert
                Assert.IsNotNull(target);
            }

            [TestMethod]
            public void Constructor_WithNonZeroPageIndexAndSize_DoesNotChangePageIndex()
            {
                //Arrange
                var startPage = 3;
                var pageSize = 10;

                //Act
                var actual = new DataPagingSpecifier(startPage, pageSize, null);

                //Assert
                Assert.AreEqual(startPage, actual.PageIndex);
            }
        }

        #endregion

        #region Nested type: DataPagingExtensionTest

        [TestClass]
        public class DataPagingExtensionTest
        {
            [TestMethod]
            public void Page_FirstPageTotalRecordsMoreThanPageSize_OnlyPageSizeCountReturned()
            {
                //Arrange
                var deck = Enumerable.Range(0, 52);
                var query = deck.AsQueryable();
                var pagingInfo = new DataPagingSpecifier();

                //Act
                var actual = deck.AsQueryable().Page(pagingInfo);

                //Assert
                Assert.AreEqual<int>(pagingInfo.PageSize, actual.Count());
            }

            [TestMethod]
            public void Page_LargeNumberOfPages_Test()
            {
                var ids = Enumerable.Range(1, 1500);
                var items = new List<Foo>();
                ids.ToList().ForEach(id => items.Add(new Foo() {Id = id, Name = "Test " + id}));

                var query = from item in items.AsQueryable()
                            orderby item.Id
                            select item;


                var output = query.Skip(20).Take(10).ToList();

                Assert.AreEqual(10, output.Count);
                Assert.AreNotEqual(items[0], output[0]);
            }

            [TestMethod]
            public void Paging_SkipAndTake_Test()
            {
                var ids = Enumerable.Range(1, 1500);
                var items = new List<Foo>();
                ids.ToList().ForEach(id => items.Add(new Foo() {Id = id, Name = "Test " + id}));

                var query = from item in items.AsQueryable()
                            orderby item.Id
                            select item;

                var output = query.Skip(20).Take(10).ToList();

                Assert.AreEqual(10, output.Count);
                Assert.AreNotEqual(items[0], output[0]);
            }

            [TestMethod]
            public void Paging_PageSpecificier_Test()
            {
                var ids = Enumerable.Range(1, 1500);
                var items = new List<Foo>();
                ids.ToList().ForEach(id => items.Add(new Foo() {Id = id, Name = "Test " + id}));

                var query = from item in items.AsQueryable()
                            orderby item.Id
                            select item;

                DataPagingSpecifier page = new DataPagingSpecifier();
                page.PageSize = 20;
                page.PageIndex = 2; //Page index is zero-based

                var output = query.Page(page).ToList(); //use the paging extension method to page


                Assert.AreEqual(page.PageSize, output.Count);
                Assert.AreNotEqual(items[0], output[0]);
            }


            [TestMethod]
            public void Page_LargeNumberOfPages_Test2()
            {
                var ids = Enumerable.Range(1, 1500);
                var items = new List<Foo>();
                ids.ToList().ForEach(id => items.Add(new Foo() {Id = id, Name = "Test " + id}));

                var query = from item in items.AsQueryable()
                            orderby item.Id
                            select item;

                DataPagingSpecifier page = new DataPagingSpecifier();
                page.PageSize = 20;
                page.PageIndex = 2; //Page index is zero-based

                var output = query.Page(page).ToList();


                Assert.AreEqual(page.PageSize, output.Count);
                Assert.AreNotEqual(items[0], output[0]);
            }

            #region Nested type: Foo

            /// <summary>
            /// A test class for paging examples
            /// </summary>
            public class Foo
            {
                public int Id { get; set; }
                public string Name { get; set; }
            }

            #endregion
        }

        #endregion

        #region Nested type: GetSkipTest

        [TestClass]
        public class GetSkipTest
        {
        }

        #endregion

        #region Nested type: PageSizeTest

        [TestClass]
        public class PageSizeTest
        {
            [TestMethod]
            public void Consructor_SetPageSize_ReturnsThatValue()
            {
                //Arrange
                var expectedPageSize = 12;

                //Act
                var target = new DataPagingSpecifier(0, expectedPageSize);

                //Assert
                Assert.AreEqual<int>(expectedPageSize, target.PageSize);
            }

            [TestMethod]
            public void SetPageSize_AnyValue_ReturnsThatValue()
            {
                //Arrange
                var target = new DataPagingSpecifier();
                var expected = 30;
                //Act
                target.PageSize = expected;

                //Assert
                Assert.AreEqual<int>(expected, target.PageSize);
            }

            [TestMethod]
            public void SetPageSize_ChangeSizeOnFirstPageToLargerOne_DoesNotChangePageIndex()
            {
                //Arrange                
                var startingSize = 5;
                var originalIndex = 0;
                var newSize = 30;

                var target = new DataPagingSpecifier() {PageSize = startingSize};
                target.PageIndex = originalIndex;

                //Act
                target.PageSize = newSize;

                //Assert
                Assert.AreEqual<int>(originalIndex, target.PageIndex);
            }

            [TestMethod]
            public void SetPageSize_ChangeSizeOnNonFirstPageToLargerOne_UpdatesPageIndex()
            {
                //Arrange
                var startingSize = 5;
                var originalIndex = 2;
                var newSize = 30;
                var expectedIndex = 0;

                var target = new DataPagingSpecifier() {PageSize = startingSize};
                target.PageIndex = originalIndex;

                //Act
                target.PageSize = newSize;

                //Assert
                Assert.AreEqual<int>(expectedIndex, target.PageIndex);
            }

            [TestMethod]
            public void SetPageSize_ChangeSizeOnNonFirstPageToSmallerOne_UpdatesPageIndex()
            {
                //Arrange
                var startingSize = 20;
                var originalIndex = 2; //Records 40-59
                var newSize = 5;
                var expectedIndex = 8; //Records 40-45

                var target = new DataPagingSpecifier() {PageSize = startingSize};
                target.PageIndex = originalIndex;

                //Act
                target.PageSize = newSize;

                //Assert
                Assert.AreEqual<int>(expectedIndex, target.PageIndex);
            }
        }

        #endregion

        #region Nested type: TotalPagesTest

        [TestClass]
        public class TotalPagesTest
        {
            [TestMethod]
            public void TotalPages_NoTotalCountSpecified_IsNull()
            {
                //Arrange
                var dataPagingSpecifier = new DataPagingSpecifier();
                dataPagingSpecifier.TotalCount = null;

                //Act
                var actual = dataPagingSpecifier.TotalNumberOfPages;

                //Assert
                Assert.IsNull(actual);
            }

            [TestMethod]
            public void TotalPages_ZeroTotalCountSpecified_IsZero()
            {
                //Arrange
                var dataPagingSpecifier = new DataPagingSpecifier();
                dataPagingSpecifier.TotalCount = 0;

                //Act
                var actual = dataPagingSpecifier.TotalNumberOfPages;

                //Assert
                Assert.AreEqual<int>(0, actual.Value);
            }
        }

        #endregion
    }
}