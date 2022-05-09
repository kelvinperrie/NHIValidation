using NUnit.Framework;
using System.Collections.Generic;

namespace NHIValidation.Tests
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void IsNhiValid_PassOldFormatValidNhis()
        {
            var oldFormatValidNhis = new List<string> {
                "ZZZ0016", "ZZZ0024", "PRP1660", "ZBX7621"
            };

            foreach (var nhi in oldFormatValidNhis)
            {
                var result = NHIValidation.IsNhiValid(nhi);

                Assert.IsTrue(result);
            }
        }
        [Test]
        public void IsNhiValid_PassOldFormatInValidNhis()
        {
            var oldFormatInvalidNhis = new List<string> {
                "ZZZ0000", "ABC1234", "ZZZ0044", "ZZZ00243", "Hello"
            };

            foreach (var nhi in oldFormatInvalidNhis)
            {
                var result = NHIValidation.IsNhiValid(nhi);

                Assert.IsFalse(result);
            }
        }

        [Test]
        public void IsNhiValid_PassNewFormatValidNhis()
        {
            // taken from nhi_test_data_v1_mar2021.xlsx
            var newFormatValidNhis = new List<string> {
                "ZRJ15RZ","ZVU27KZ","ZFW82UY","ZGE49PF","ZGT56KB","ZHS91BR","ZHW58CN","ZHY48KS","ZLP86TT","ZLV86AX","ZMB24NR","ZMQ76GJ","ZMZ50WK","ZNE51MT","ZNK30DV","ZNY96HA","ZPR06CD","ZPR16BB","ZQS06AW","ZQU93YM","ZQX24SK","ZRG64XE","ZRK71RG","ZRN78JJ","ZRS54VL","ZTK62JK","ZTR5696","ZTZ82UH","ZUF11LU","ZVD34XH","ZWT49FS","ZXB26JF","ZXD67TG","ZXH68CP","ZYB54DD","ZYV14AH","ZZB17BJ","ZZD25FS","ZZG78TW","ZZK09PQ",
            };

            foreach (var nhi in newFormatValidNhis)
            {
                var result = NHIValidation.IsNhiValid(nhi);

                Assert.IsTrue(result);
            }
        }
        [Test]
        public void IsNhiValid_PassNewFormatInvalidNhis()
        {
            var newFormatInvalidNhis = new List<string> {
                "ZZZ00AD", "ZZZ00AXA", "HOWDY"
            };

            foreach (var nhi in newFormatInvalidNhis)
            {
                var result = NHIValidation.IsNhiValid(nhi);

                Assert.IsFalse(result);
            }
        }
    }
}