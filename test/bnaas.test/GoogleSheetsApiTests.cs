using System;
using Xunit;

namespace bnaas.test
{
    public class GoogleSheetsApiTests
    {
        [Fact]
        public void Can_get_names()
        {
            var names = new BandNameGetter().Get();

            Assert.NotNull(names);
            Assert.True(names.Count > 0);
        }
    }
}
