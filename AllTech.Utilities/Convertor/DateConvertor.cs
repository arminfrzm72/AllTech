using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace AllTech.Utilities.Convertor
{
    public static class DateConvertor
    {
        public static string ToShamsi(this DateTime value)
        {
            PersianCalendar pCalender = new PersianCalendar();
            return pCalender.GetYear(value) + "/" + pCalender.GetMonth(value).ToString("00") + "/" + pCalender.GetDayOfMonth(value).ToString("00");
        }
    }
}
