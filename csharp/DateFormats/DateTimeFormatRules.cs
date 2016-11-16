using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deerchao.DateFormats
{
    /// <summary>
    /// <para>Represents a set of date/time formatting rules.</para>
    /// <para>Properties could be null if the rule is not supported.</para>
    /// </summary>
    public abstract class DateTimeFormatRules
    {
        /// <summary>
        ///  1 ~ 31
        ///  </summary>
        public abstract string DayOfMonthShort { get; }

        /// <summary>
        ///  01 ~ 31
        ///  </summary>
        public abstract string DayOfMonthLong { get; }

        /// <summary>
        ///  Mon ~ Sun
        ///  </summary>
        public abstract string DayOfWeekShort { get; }

        /// <summary>
        ///  Monday ~ Sunday
        ///  </summary>
        public abstract string DayOfWeekLong { get; }

        /// <summary>
        ///  1 ~ 366
        ///  </summary>
        public abstract string DayOfYearShort { get; }

        /// <summary>
        ///  001 ~ 366
        ///  </summary>
        public abstract string DayOfYearLong { get; }

        /// <summary>
        ///  1 ~ 12
        ///  </summary>
        public abstract string MonthOfYearShort { get; }

        /// <summary>
        ///  01 ~ 12
        ///  </summary>
        public abstract string MonthOfYearLong { get; }

        /// <summary>
        ///  Jan ~ Dec
        ///  </summary>
        public abstract string MonthNameShort { get; }

        /// <summary>
        ///  January ~ December
        ///  </summary>
        public abstract string MonthNameLong { get; }

        /// <summary>
        ///  00 ~ 99
        ///  </summary>
        public abstract string YearShort { get; }

        /// <summary>
        ///  0000 ~ 9999
        ///  </summary>
        public abstract string YearLong { get; }

        /// <summary>
        ///  AM ~ PM
        ///  </summary>
        public abstract string AmPm { get; }

        /// <summary>
        ///  0 ~ 23
        ///  </summary>
        public abstract string Hour24Short { get; }

        /// <summary>
        ///  00 ~ 23
        ///  </summary>
        public abstract string Hour24Long { get; }

        /// <summary>
        ///  1 ~ 12
        ///  </summary>
        public abstract string Hour12Short { get; }

        /// <summary>
        ///  01 ~ 12
        ///  </summary>
        public abstract string Hour12Long { get; }

        /// <summary>
        ///  0 ~ 59
        ///  </summary>
        public abstract string MinuteShort { get; }

        /// <summary>
        ///  00 ~ 59
        ///  </summary>
        public abstract string MinuteLong { get; }

        /// <summary>
        ///  0 ~ 59
        ///  </summary>
        public abstract string SecondShort { get; }

        /// <summary>
        ///  00 ~ 59
        ///  </summary>
        public abstract string SecondLong { get; }

        /// <summary>
        ///  0 ~ 9
        ///  </summary>
        public abstract string FractionalSecond1 { get; }

        /// <summary>
        ///  00 ~ 99
        ///  </summary>
        public abstract string FractionalSecond2 { get; }

        /// <summary>
        ///  000 ~ 999
        ///  </summary>
        public abstract string FractionalSecond3 { get; }

        /// <summary>
        ///  +08:00
        ///  </summary>
        public abstract string TimeZone { get; }

        /// <summary>
        ///  1384935963160
        ///  </summary>
        public abstract string UnixTimestamp { get; }


        public abstract string MakeLiteral(string literal);

        public abstract string ReadEscapedPart(string format, int startIndex, out int length);


        public static DateTimeFormatRules Standard
        {
            get { return StandardDateTimeFormatRules.Instance; }
        }

        public static DateTimeFormatRules DotNet
        {
            get { return DotNetDateTimeFormatRules.Instance; }
        }

        public static DateTimeFormatRules MomentJs
        {
            get { return MomentJsDateTimeFormatRules.Instance; }
        }

        public static DateTimeFormatRules JqueryDatePicker
        {
            get { return JQueryUIDatePickerFormatRules.Instance; }
        }

        public static DateTimeFormatRules JqueryTimePicker
        {
            get { return JQueryUITimePickerFormatRules.Instance; }
        }

        public static string Convert(string format, DateTimeFormatRules sourceRules, DateTimeFormatRules destRules)
        {
            if (sourceRules == destRules)
                return format;

            var sb = new StringBuilder(format.Length);

            var index = 0;
            while (index < format.Length)
            {
                string literal;
                int next;
                var token = sourceRules.LocateNextToken(format, index, out next, out literal);

                if (literal.Length > 0)
                    sb.Append(destRules.MakeLiteral(literal));

                if (token.Length > 0)
                    sb.Append(destRules.AllTokens[sourceRules.TokenMap[token]]);

                index = next;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Locate next token in the format string
        /// </summary>
        /// <param name="format">The input format string</param>
        /// <param name="begin">The beginning index in format string to search</param>
        /// <param name="nextBegin">where to search next time</param>
        /// <param name="literal">literal string before the token</param>
        /// <returns>The token, if any; otherwise string.Empty</returns>
        private string LocateNextToken(string format, int begin, out int nextBegin, out string literal)
        {
            var sb = new StringBuilder(format.Length);
            var index = begin;
            while (index < format.Length)
            {
                int length;
                var escaped = ReadEscapedPart(format, index, out length);
                if (length > 0)
                {
                    sb.Append(escaped);
                    index += length;
                    continue;
                }

                var token = TryingSequence
                    .FirstOrDefault(x => format.IndexOf(x, index, StringComparison.Ordinal) == index);

                if (token == null)
                {
                    sb.Append(format[index]);
                    index++;
                    continue;
                }

                nextBegin = index + token.Length;
                literal = sb.ToString();
                return token;
            }

            nextBegin = index;
            literal = sb.ToString();
            return "";
        }


        private string[] _tokens;
        private string[] AllTokens
        {
            get
            {
                return _tokens ?? (_tokens = new[]
                {
                    DayOfMonthShort, DayOfMonthLong,
                    DayOfWeekShort, DayOfWeekLong,
                    DayOfYearShort, DayOfYearLong,
                    MonthOfYearShort, MonthOfYearLong,
                    MonthNameShort, MonthNameLong,
                    YearShort, YearLong,
                    AmPm,
                    Hour24Short, Hour24Long,
                    Hour12Short, Hour12Long,
                    MinuteShort, MinuteLong,
                    SecondShort, SecondLong,
                    FractionalSecond1, FractionalSecond2, FractionalSecond3,
                    TimeZone,
                    UnixTimestamp,
                });
            }
        }

        private Dictionary<string, int> _tokenMap;
        private Dictionary<string, int> TokenMap
        {
            get
            {
                return _tokenMap ?? (_tokenMap = AllTokens
                    .Select((token, index) => new
                    {
                        index,
                        token,
                    })
                    .Where(x => x.token != null)
                    .ToDictionary(x => x.token, x => x.index));
            }
        }

        private string[] _tryingSequence;
        private string[] TryingSequence
        {
            get
            {
                return _tryingSequence ?? (_tryingSequence = TokenMap.Keys
                    .OrderByDescending(x => x.Length)
                    .ToArray());
            }
        }
    }
}
