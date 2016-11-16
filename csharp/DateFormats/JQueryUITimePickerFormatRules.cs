using System.Text;

namespace Deerchao.DateFormats
{
    /// <summary>
    /// <para>Formating rules for jQuery UI datepicker.</para>
    /// <para>The time part is for timepicker (http://trentrichardson.com/examples/timepicker/)</para>
    /// </summary>
    internal class JQueryUITimePickerFormatRules : DateTimeFormatRules
    {
        private JQueryUITimePickerFormatRules()
        {
        }

        public static JQueryUITimePickerFormatRules Instance = new JQueryUITimePickerFormatRules();

        public override string DayOfMonthShort
        {
            get { return null; }
        }

        public override string DayOfMonthLong
        {
            get { return null; }
        }

        public override string DayOfWeekShort
        {
            get { return null; }
        }

        public override string DayOfWeekLong
        {
            get { return null; }
        }

        public override string DayOfYearShort
        {
            get { return null; }
        }

        public override string DayOfYearLong
        {
            get { return null; }
        }

        public override string MonthOfYearShort
        {
            get { return null; }
        }

        public override string MonthOfYearLong
        {
            get { return null; }
        }

        public override string MonthNameShort
        {
            get { return null; }
        }

        public override string MonthNameLong
        {
            get { return null; }
        }

        public override string YearShort
        {
            get { return null; }
        }

        public override string YearLong
        {
            get { return null; }
        }

        public override string AmPm
        {
            get { return "TT"; }
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
            get { return null; }
        }

        public override string FractionalSecond2
        {
            get { return null; }
        }

        public override string FractionalSecond3
        {
            get { return "l"; }
        }

        public override string TimeZone
        {
            get { return "Z"; }
        }

        public override string UnixTimestamp
        {
            get { return null; }
        }


        public char[] ReservedChars
        {
            get { return "HhmslctTzZ'".ToCharArray(); }
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