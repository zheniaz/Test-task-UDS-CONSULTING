using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    [TestClass]
    public class WorkDayCalculatorTests
    {

        [TestMethod]
        public void TestNoWeekEnd()
        {
            DateTime startDate = new DateTime(2014, 12, 1);
            int count = 10;

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, null);

            Assert.AreEqual(startDate.AddDays(count), result);
        }

        [TestMethod]
        public void TestWeekEnd()
        {
            DateTime startDate = new DateTime(2014, 12, 1);
            int count = 10;
            WeekEnd[] week =
            {
                new WeekEnd(startDate,startDate)
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, week);

            Assert.AreEqual(startDate.AddDays(count - 1), result);
        }

        // в случае если все дни выходные, не очень ясно какой должен быть возвзврат, 
        // и потому я выбрал качестве возврата дата начала + 0
        [TestMethod]
        public void TestWeekAll()
        {
            DateTime startDate = new DateTime(2014, 12, 1);
            DateTime endDate = new DateTime(2014, 12, 11);
            int count = 10;
            WeekEnd[] week =
            {
                new WeekEnd(startDate,endDate)
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, week);

            Assert.AreEqual(startDate, result);
        }

        [TestMethod]
        public void TestWeekPart()
        {
            DateTime startDate = new DateTime(2014, 12, 1);
            int count = 10;
            WeekEnd[] week =
            {
                new WeekEnd(new DateTime(2014, 12, 5), new DateTime(2014, 12, 15))
            };
            

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, week);

            Assert.AreEqual(startDate.AddDays(3), result);
        }

        [TestMethod]
        public void TestWeekDays()
        {
            DateTime startDate = new DateTime(2014, 12, 1);
            int count = 30;
            WeekEnd[] week =
            {
                new WeekEnd(new DateTime(2014, 12, 6), new DateTime(2014, 12, 7)),
                new WeekEnd(new DateTime(2014, 12, 13), new DateTime(2014, 12, 14)),
                new WeekEnd(new DateTime(2014, 12, 20), new DateTime(2014, 12, 21)),
                new WeekEnd(new DateTime(2014, 12, 27), new DateTime(2014, 12, 28))
            };


            DateTime result = new WorkDayCalculator().Calculate(startDate, count, week);

            Assert.AreEqual(startDate.AddDays(22), result);
        }

    }
}
