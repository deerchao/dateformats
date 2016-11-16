using System.Linq;
using System.Text;

namespace Deerchao.DateFormats
{
    /// <summary>
    /// My own standard date time format rules
    /// </summary>
    internal class StandardDateTimeFormatRules : DateTimeFormatRules
    {
        private StandardDateTimeFormatRules()
        {
        }

        public static StandardDateTimeFormatRules Instance = new StandardDateTimeFormatRules();


        public override string DayOfMonthShort
        {
            get { return "d"; }
        }

        public override string DayOfMonthLong
        {
            get { return "dd"; }
        }

        public override string DayOfWeekShort
        {
            get { return "ddd"; }
        }

        public override string DayOfWeekLong
        {
            get { return "dddd"; }
        }

        public override string DayOfYearShort
        {
            get { return "D"; }
        }

        public override string DayOfYearLong
        {
            get { return "DD"; }
        }

        public override string MonthOfYearShort
        {
            get { return "M"; }
        }

        public override string MonthOfYearLong
        {
            get { return "MM"; }
        }

        public override string MonthNameShort
        {
            get { return "MMM"; }
        }

        public override string MonthNameLong
        {
            get { return "MMMM"; }
        }

        public override string YearShort
        {
            get { return "yy"; }
        }

        public override string YearLong
        {
            get { return "yyyy"; }
        }

        public override string AmPm
        {
            get { return "tt"; }
        }

        public override string Hour24Short
        {
            get { return "H"; }
        }

        public override string Hour24Long
        {
            get { return "HH"; }
        }

        public override string Hour12Short
        {
            get { return "h"; }
        }

        public override string Hour12Long
        {
            get { return "hh"; }
        }

        public override string MinuteShort
        {
            get { return "m"; }
        }

        public override string MinuteLong
        {
            get { return "mm"; }
        }

        public override string SecondShort
        {
            get { return "s"; }
        }

        public override string SecondLong
        {
            get { return "ss"; }
        }

        public override string FractionalSecond1
        {
            get { return "f"; }
        }

        public override string FractionalSecond2
        {
            get { return "ff"; }
        }

        public override string FractionalSecond3
        {
            get { return "fff"; }
        }

        public override string TimeZone
        {
            get { return "Z"; }
        }

        public override string UnixTimestamp
        {
            get { return "X"; }
        }


        public char[] ReservedChars
        {
            get { return "dDMytHhmsfZX\\".ToCharArray(); }
        }


        public override string MakeLiteral(string literal)
        {
            if (literal.IndexOfAny(ReservedChars) < 0)
                return literal;

            var sb = new StringBuilder(literal.Length * 2);
            foreach (char c in literal)
            {
                if (ReservedChars.Contains(c))
                    sb.Append('\\');

                sb.Append(c);
            }
            return sb.ToString();
        }

        public override string ReadEscapedPart(string format, int startIndex, out int length)
        {
            var sb = new StringBuilder();
            var index = startIndex;
            while (index < format.Length)
            {
                var c = format[index];

                if (c == '\\')
                {
                    sb.Append(index == format.Length - 1 ? '\\' : format[++index]);

                    index++;
                    continue;
                }

                break;
            }

            length = index - startIndex;

            return sb.ToString();
        }
    }
}