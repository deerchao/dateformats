using System.Text;

namespace Deerchao.DateFormats
{
    internal class JQueryUIDatePickerFormatRules : DateTimeFormatRules
    {
        private JQueryUIDatePickerFormatRules()
        {
        }

        public static JQueryUIDatePickerFormatRules Instance = new JQueryUIDatePickerFormatRules();

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
            get { return "D"; }
        }

        public override string DayOfWeekLong
        {
            get { return "DD"; }
        }

        public override string DayOfYearShort
        {
            get { return "o"; }
        }

        public override string DayOfYearLong
        {
            get { return "oo"; }
        }

        public override string MonthOfYearShort
        {
            get { return "m"; }
        }

        public override string MonthOfYearLong
        {
            get { return "mm"; }
        }

        public override string MonthNameShort
        {
            get { return "M"; }
        }

        public override string MonthNameLong
        {
            get { return "MM"; }
        }

        public override string YearShort
        {
            get { return "y"; }
        }

        public override string YearLong
        {
            get { return "yy"; }
        }

#region Time
        public override string AmPm
        {
            get { return null; }
        }

        public override string Hour24Short
        {
            get { return null; }
        }

        public override string Hour24Long
        {
            get { return null; }
        }

        public override string Hour12Short
        {
            get { return null; }
        }

        public override string Hour12Long
        {
            get { return null; }
        }

        public override string MinuteShort
        {
            get { return null; }
        }

        public override string MinuteLong
        {
            get { return null; }
        }

        public override string SecondShort
        {
            get { return null; }
        }

        public override string SecondLong
        {
            get { return null; }
        }

        public override string FractionalSecond1
        {
            get { return null; }
        }

        public override string FractionalSecond2
        {
            get { return null; }
        }

        public override string FractionalSecond3
        {
            get { return null; }
        }

        public override string TimeZone
        {
            get { return null; }
        }
#endregion


        public override string UnixTimestamp
        {
            get { return "@"; }
        }


        public char[] ReservedChars
        {
            get { return "dDomMy@'".ToCharArray(); }
        }


        public override string MakeLiteral(string literal)
        {
            if (literal.IndexOfAny(ReservedChars) < 0)
                return literal;

            return "'" + literal.Replace("'", "''") + "'";
        }

        public override string ReadEscapedPart(string format, int startIndex, out int length)
        {
            if (format[startIndex] != '\'')
            {
                length = 0;
                return "";
            }

            var sb = new StringBuilder();
            var index = startIndex;
            while (++index < format.Length)
            {
                var c = format[index];
                if (c == '\'')
                {
                    index++;
                    if (index == format.Length)
                    {
                        break;
                    }

                    if (format[index] == '\'')
                    {
                        sb.Append(c);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    sb.Append(c);
                }
            }

            length = index - startIndex;
            return sb.ToString();
        }
    }
}