using System;
using CongestionCharge.Business.Library;
using CongestionCharge.Business.Model;
using NUnit.Framework;

namespace CongestionChargeTests
{
    [TestFixture]
    public class DateDayEnumParserTest
    {

        [Test]
        public void Every_Day_Of_Week_Parse_Test()
        {
            var mondayDate = new DateTime(2015, 07, 20); // Monday 20 July 2015

            var monday = DateDayEnumParser.ParseDay(mondayDate);
            Assert.AreEqual(Day.Monday, monday);

            var tuesdayDate = mondayDate.AddDays(1);
            var tuesday = DateDayEnumParser.ParseDay(tuesdayDate);
            Assert.AreEqual(Day.Tuesday, tuesday);

            var wednesdayDate = tuesdayDate.AddDays(1);
            var wednesday = DateDayEnumParser.ParseDay(wednesdayDate);
            Assert.AreEqual(Day.Wednesday, wednesday);

            var thursdayDate = wednesdayDate.AddDays(1);
            var thursday = DateDayEnumParser.ParseDay(thursdayDate);
            Assert.AreEqual(Day.Thursday, thursday);

            var fridayDate = thursdayDate.AddDays(1);
            var friday = DateDayEnumParser.ParseDay(fridayDate);
            Assert.AreEqual(Day.Friday, friday);


            var saturdayDate = fridayDate.AddDays(1);
            var saturday = DateDayEnumParser.ParseDay(saturdayDate);
            Assert.AreEqual(Day.Saturday, saturday);

            var sundayDate = saturdayDate.AddDays(1);
            var sunday = DateDayEnumParser.ParseDay(sundayDate);
            Assert.AreEqual(Day.Sunday, sunday);
        }
    }
}
