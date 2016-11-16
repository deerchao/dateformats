using System.Text;

namespace Deerchao.DateFormats
{
    internal class MomentJsDateTimeFormatRules : DateTimeFormatRules
    {
        private MomentJsDateTimeFormatRules()
        {
        }

        public static MomentJsDateTimeFormatRules Instance = new MomentJsDateTimeFormatRules();


        public override string DayOfMonthShort
        {
            get { return "D"; }
        }

        public override string DayOfMonthLong
        {
            get { return "DD"; }
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
            get { return "DDD"; }
        }

        public override string DayOfYearLong
        {
            get { return "DDDD"; }
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
            get { return "YY"; }
        }

        public override string YearLong
        {
            get { return "YYYY"; }
        }

        public override string AmPm
        {
            get { return "A"; }
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
            get { return "S"; }
        }

        public override string FractionalSecond2
        {
            get { return "SS"; }
        }

        public override string FractionalSecond3
        {
            get { return "SSS"; }
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
            get { return "MoDdeEwWYgGAaHhmsSzZX".ToCharArray(); }
        }

        public override string MakeLiteral(string literal)
        {
            literal = literal.Replace("[", "(").Replace("]", ")");

            if (literal.IndexOfAny(ReservedChars) < 0)
                return literal;

            return '[' + literal + ']';
        }

        public override string ReadEscapedPart(string format, int startIndex, out int length)
        {
            if (format[startIndex] != '[')
            {
                length = 0;
                return "";
            }

            var sb = new StringBuilder();

            var index = startIndex + 1;
            while (index < format.Length)
            {
                var c = format[index++];

                if (c == ']')
                    break;

                sb.Append(c);
            }

            length = index - startIndex;

            return sb.ToString();
        }
    }
}