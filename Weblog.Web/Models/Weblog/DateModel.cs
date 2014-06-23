using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Weblog.Core.DataAccess.Weblog;

namespace Weblog.Web.Models.Weblog
{
    public class DateModel
    {
        public int Month { get; set; }
        public int Year { get; set; }

        public string DateString
        {
            get
            {
                DateTime date = new DateTime(Year, Month, 1);
                string result = date.ToString("MMMM") + " " + Year;
                return result;
            }
        }

        public DateModel(int month,int year)
        {
            this.Month = month;
            this.Year = year;
        }

        public DateModel()
        {
        }

        public static bool operator ==(DateModel a, DateModel b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.Month == b.Month && a.Year == b.Year;
        }

        public static bool operator !=(DateModel a, DateModel b)
        {
            return !(a == b);
        }

    }
}