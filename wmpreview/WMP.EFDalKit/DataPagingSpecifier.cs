using System;

namespace WMP.EFDalKit
{
    /// <summary>
    /// A class that specifies desired paging information for the Data Layer to use
    /// </summary>
    public class DataPagingSpecifier
    {
        #region Constructors

        public DataPagingSpecifier()
                : this(0, DEFAULT_PAGE_SIZE)
        {
        }

        public DataPagingSpecifier(int pageIndex)
                : this(pageIndex, DEFAULT_PAGE_SIZE)
        {
        }

        public DataPagingSpecifier(int pageIndex, int pageSize)
                : this(pageIndex, pageSize, null)
        {
        }

        public DataPagingSpecifier(int pageIndex, int pageSize, int? totalCount)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalCount = totalCount;
        }

        #endregion

        /// <summary>
        /// Where is paging numbering starting from -- 0 based
        /// </summary>
        public const int PageCountBase = 0;

        /// <summary>
        /// Specifies the number of records that loaded per page.  If not specified
        /// will be set to a default page size value
        /// </summary>
        public int PageSize
        {
            get { return _PageSize; }
            set
            {
                ValidatePageSize(value);
                PageIndex = AdjustPageIndexForNewPageSize(_PageSize, value);
                _PageSize = value;
            }
        }

        /// <summary>
        /// A zero-based page index value.          
        /// TODO: If PageIndex is greater than total count
        /// </summary>
        public int PageIndex
        {
            get { return _PageIndex; }
            set
            {
                ValidatePageIndexValue(value);
                ValidatePageIndexAgainstTotalCount(value);
                if (value != _PageIndex)
                {
                    _PageIndex = value;
                }
            }
        }

        /// <summary>
        /// The total number of records in the set.  If not set, this will 
        /// be considered indeterminate
        /// </summary>
        public int? TotalCount
        {
            get { return _TotalCount; }
            set { _TotalCount = value; }
        }

        /// <summary>
        /// Calculates the total number of pages
        /// If the Total count is not supplied will return null
        /// If the total count is 0, will return 0
        /// </summary>
        public int? TotalNumberOfPages
        {
            get
            {
                if (!this.TotalCount.HasValue)
                {
                    return null;
                }

                int? result = null;

                if (this.TotalCount.Value > 0)
                {
                    result = (int) Math.Ceiling((double) TotalCount.Value / PageSize);
                }
                else
                {
                    result = 0;
                }
                return result;
            }
        }


        /// <summary>
        /// Provides the Take value for Linq queries
        /// </summary>
        /// <returns></returns>
        public int GetTake()
        {
            return PageSize;
        }

        public int GetSkip()
        {
            int result = SkipCountBase;

            if (!IsStartPage())
            {
                result = PageIndex * PageSize;
            }

            return result;
        }


        private void ValidatePageSize(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("value", "Page size must be a non-negative, non-zero value");
            }
        }


        /// <summary>
        /// Helper to ask if we're on the first page or not
        /// </summary>
        /// <returns></returns>
        public bool IsStartPage()
        {
            return PageIndex == PageCountBase;
        }

        #region Private fields

        private const int SkipCountBase = 0;
        private const int DEFAULT_PAGE_SIZE = 10;
        private int _PageIndex;
        private int _PageSize;
        private int? _TotalCount;

        #endregion

        #region Private methods

        private int AdjustPageIndexForNewPageSize(int oldPageSize, int newPageSize)
        {
            //Example ... If we're on page 2 and 10 items per page with new size of 30, we'll be 
            //  page 2 = 20 to 29 (pageIndex * PageSize) to (pageIndex  * pageSize) + (pageSize - 1)
            //  Thereforwe we should be on page 1

            //Another Example:  if we're on page 2 and there's 30 records per page (60 - 99) and we switch to 10 items per page
            //  we should start on page 6 (60-69)       

            if (!IsStartPage())
            {
                var basePageRowNumber = oldPageSize * PageIndex;
                var newPageIndex = (int) (basePageRowNumber / newPageSize);
                return newPageIndex;
            }
            return 0;
        }


        private void ValidatePageIndexAgainstTotalCount(int value)
        {
            if (TotalCount.HasValue)
            {
                //Validate that the page index doesn't take us above the total
                throw new NotImplementedException();
            }
        }

        private void ValidatePageIndexValue(int value)
        {
            if (value < PageCountBase)
            {
                throw new ArgumentException("value", "The Page Index must be a non-zero, non-negative number");
            }
        }

        #endregion
    }
}