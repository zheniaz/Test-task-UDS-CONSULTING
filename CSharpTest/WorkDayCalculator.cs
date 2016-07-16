using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            DateTime returnDate = startDate;
            Console.WriteLine(returnDate);
            DateTime endDateCalc = startDate.AddDays(dayCount);
            DateTime tempDate;

            List<DateTime> tempWeekDays = new List<DateTime>();
            if (weekEnds != null)
            {
                // создаем список выходных, которые попадают в рассматриваемое время 
                foreach (var item in weekEnds)
                {
                    int weekDays = 0;
                    // вычисляем, если несколько подряд выходных или тольно один
                    if (item.StartDate != item.EndDate)
                    {
                        tempDate = item.StartDate;
                        do
                        {
                            weekDays++;
                            tempDate = tempDate.AddDays(1);
                        }
                        while (tempDate != item.EndDate.AddDays(1));
                    }
                    else weekDays = 1;

                    tempDate = item.StartDate;
                    // формируем список выходных, которые попадают в рассматриваемое время 
                    for (int i = 0; i < weekDays; i++)
                    {
                        if (startDate <= tempDate && tempDate <= endDateCalc)
                        {
                            tempWeekDays.Add(tempDate);
                        }

                        tempDate = tempDate.AddDays(1);
                    }
                }

                // Проходим по каждому дню, начиная с startDate и до endDateCalc отсеивая все выходные
                tempDate = startDate;
                bool dateIn = false;
                while (tempDate != endDateCalc.AddDays(1))
                {
                    if (tempWeekDays.Contains(tempDate))
                    {
                        if (tempDate == startDate)
                            returnDate = returnDate.AddDays(-1);
                        tempDate = tempDate.AddDays(1);
                    }
                    else
                    {
                        if (tempDate != startDate)
                        {
                            returnDate = returnDate.AddDays(1);
                            tempDate = tempDate.AddDays(1);
                            dateIn = true;
                        }
                        else
                        {
                            tempDate = tempDate.AddDays(1);
                            dateIn = true;
                        }
                    }
                }
                if (dateIn)
                    return returnDate;
                else
                    return returnDate.AddDays(1);
            }
            else
            {
                returnDate = returnDate.AddDays(dayCount);
            }

            return returnDate;
        }
    }
}
