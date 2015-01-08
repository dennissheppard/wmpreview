using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WMP.WebApi.Tests
{
    public class WebApiTest
    {
        protected static BadRequestErrorMessageResult AssertIsBadRequestErrorMessage(IHttpActionResult result)
        {
            Assert.IsInstanceOfType(result, typeof (BadRequestErrorMessageResult));
            var content = (BadRequestErrorMessageResult) result;
            return content;
        }
    }
}