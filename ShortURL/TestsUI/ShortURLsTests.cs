using NUnit.Framework;
using ShortURL.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortURL.TestsUI
{
    public class ShortURLsTests : BaseTests
    {

        const int defaultNumberOfShortURLs = 3;

        [Test]
        public void VerrifyShortURLsElements()
        {
            var shortUrlsPage = new ShortURLsPage(driver);
            shortUrlsPage.Open();

            Assert.IsTrue(shortUrlsPage.IsPageOpen(), 
                          "Short URLs Page is not opened.");
            Assert.AreEqual("Short URLs", shortUrlsPage.GetPageHeader(),
                            "Expected header of page is different.");

            int listOfShortUrls = shortUrlsPage.ShortUrls.Count;

            Assert.IsTrue(listOfShortUrls >= defaultNumberOfShortURLs, 
                          "Number of Short URLs are different." );

        }
    }
}
