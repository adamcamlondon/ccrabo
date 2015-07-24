using System;
using CongestionCharge.Business.Library;
using NUnit.Framework;

namespace CongestionChargeTests
{
    [TestFixture]
    public class DateTimeSpliterTest
    {

        [Test]
        public void SpliterIntoDays_Works_With_One_Day()
        {
            var start = new DateTime(2015,07,1,13,30,0);    //13:30 1/07/2015
            var end = new DateTime(2015, 07, 1, 13, 30, 0); //18:30 1/07/2015

            var days = DateTimeSpliter.SplitIntoDays(start, end);

            Assert.AreEqual(1, days.Count);
            Assert.AreEqual(start, days[0].Start);
            Assert.AreEqual(end, days[0].End);
        }

        [Test]
        public void SpliterIntoDays_Works_Over_Two_Days()
        {
            var start = new DateTime(2015, 07, 1, 13, 30, 0);    //13:30 1/07/2015
            var end = new DateTime(2015, 07, 2, 13, 30, 0);      //13:30 1/08/2015

            var days = DateTimeSpliter.SplitIntoDays(start, end);

            Assert.AreEqual(2, days.Count);

            Assert.AreEqual(start, days[0].Start);
            Assert.AreEqual(new DateTime(2015, 07, 2, 0, 0, 0), days[0].End);

            Assert.AreEqual(new DateTime(2015, 07, 2, 0, 0, 0), days[1].Start);
            Assert.AreEqual(end, days[1].End);
        }

        [Test]
        public void SpliterIntoDays_Works_Over_Two_Days_Various_Times()
        {
            var start = new DateTime(2015, 07, 1, 9, 30, 0);    //9:30 1/07/2015
            var end = new DateTime(2015, 07, 2, 14, 0, 0); //14:00 1/08/2015

            var days = DateTimeSpliter.SplitIntoDays(start, end);

            Assert.AreEqual(2, days.Count);

            Assert.AreEqual(start, days[0].Start);
            Assert.AreEqual(new DateTime(2015, 07, 2, 0, 0, 0), days[0].End);

            Assert.AreEqual(new DateTime(2015, 07, 2, 0, 0, 0), days[1].Start);
            Assert.AreEqual(end, days[1].End);
        }


        [Test]
        public void SpliterIntoDays_Works_Over_Five_Days_Spans_Month()
        {
            var start = new DateTime(2015, 07, 30, 10, 30, 0);    //10:30 30/07/2015
            var end = new DateTime(2015, 08, 3, 15, 17, 0);       //15:17 03/08/2015

            var days = DateTimeSpliter.SplitIntoDays(start, end);

            Assert.AreEqual(5, days.Count);

            //Day 1
            Assert.AreEqual(start, days[0].Start);
            Assert.AreEqual(new DateTime(2015, 07, 31, 0, 0, 0), days[0].End);

            //Day 2
            Assert.AreEqual(new DateTime(2015, 07, 31, 0, 0, 0), days[1].Start);
            Assert.AreEqual(new DateTime(2015, 08, 01, 0, 0, 0), days[1].End);

            //Day 3
            Assert.AreEqual(new DateTime(2015, 08, 01, 0, 0, 0), days[2].Start);
            Assert.AreEqual(new DateTime(2015, 08, 02, 0, 0, 0), days[2].End);

            //Day 4
            Assert.AreEqual(new DateTime(2015, 08, 02, 0, 0, 0), days[3].Start);
            Assert.AreEqual(new DateTime(2015, 08, 03, 0, 0, 0), days[3].End);

            //Day 5
            Assert.AreEqual(new DateTime(2015, 08, 03, 0, 0, 0), days[4].Start);
            Assert.AreEqual(end, days[4].End);
        }
    }
}
