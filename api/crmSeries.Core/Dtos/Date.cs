using System;

namespace crmSeries.Core.Dtos
{
    public class Date : IEquatable<Date>
    {
        public Date()
        {

        }

        public Date(DateTime dateTime)
            : this((int) dateTime.Month, (int) dateTime.Day, (int) dateTime.Year)
        {

        }

        public Date(int month, int day, int year)
        {
            Month = month;
            Day = day;
            Year = year;
        }

        public bool IsDateValid()
        {
            return DateTime.TryParse($"{Month}/{Day}/{Year}", out var datValue);
        }

        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public DateTime ToDateTime()
        {
            var dateTime = DateTime.Parse($"{Month}/{Day}/{Year}");
            return dateTime;
        }

        public bool Equals(Date other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Day == other.Day && Month == other.Month && Year == other.Year;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Date) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Day;
                hashCode = (hashCode * 397) ^ Month;
                hashCode = (hashCode * 397) ^ Year;
                return hashCode;
            }
        }
    }
}
