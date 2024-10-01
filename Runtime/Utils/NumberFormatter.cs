using System;
using System.Collections.Generic;
using System.Text;

namespace MVVM.Bindings.Utils {
    public class NumberFormatter {
        private static StringBuilder _amountFormat = new();
        private static StringBuilder _finalStringFormat = new();

        public static string FormatNumber(double amount, ulong shortenFromThisNumber, bool truncated, bool isDouble) {
            double displayAmount = 0;
            string formatString = string.Empty;

            if (amount < shortenFromThisNumber) {
                // if amount is less than shortenFromThisNumber, no shortening
                shortenFromThisNumber = NameOfNumber.NONE;
            }

            displayAmount = amount * 1.0d;
            if (shortenFromThisNumber != NameOfNumber.NONE) {
                // find the biggest number that is smaller than amount and shorten the amount using that.
                foreach (ulong nameOfNum in DESCENDING_NAMES_OF_NUMBERS) {
                    if (nameOfNum <= amount) {
                        shortenFromThisNumber = nameOfNum;
                        break;
                    }
                }

                displayAmount /= (double) shortenFromThisNumber;
            }

            formatString = numberFormats[shortenFromThisNumber];

            if (truncated) {
                displayAmount = displayAmount < 10f
                    ? Math.Truncate(displayAmount * 100) / 100d
                    : Math.Truncate(displayAmount * 10) / 10d;
            }

            string amountFormat = !isDouble && shortenFromThisNumber == NameOfNumber.NONE
                ? "{0:n0}"
                : (displayAmount < 9.995f ? "{0:0.##}" : "{0:0.#}");

            _amountFormat.Clear();
            _amountFormat.AppendFormat(amountFormat, displayAmount);

            _finalStringFormat.Clear();
            _finalStringFormat.AppendFormat(formatString, _amountFormat);

            return _finalStringFormat.ToString();
        }

        private static List<ulong> DESCENDING_NAMES_OF_NUMBERS = new List<ulong>() {
            NameOfNumber.QI_QUINTILLION,
            NameOfNumber.QA_QUADRILLION,
            NameOfNumber.T_TRILLION,
            NameOfNumber.B_BILLION,
            NameOfNumber.M_MILLION,
            NameOfNumber.k_KILO,
            NameOfNumber.NONE,
        };

        private static Dictionary<ulong, string> numberFormats = new Dictionary<ulong, string> {
            {NameOfNumber.NONE, "{0}"},
            {NameOfNumber.k_KILO, "{0}k"},
            {NameOfNumber.M_MILLION, "{0}M"},
            {NameOfNumber.B_BILLION, "{0}B"},
            {NameOfNumber.T_TRILLION, "{0}T"},
            {NameOfNumber.QA_QUADRILLION, "{0}Qa"},
            {NameOfNumber.QI_QUINTILLION, "{0}Qi"},
        };

        public static class NameOfNumber {
            public readonly static ulong NONE = 0;
            public readonly static ulong k_KILO = 1000;
            public readonly static ulong M_MILLION = 1000000;
            public readonly static ulong B_BILLION = 1000000000;
            public readonly static ulong T_TRILLION = 1000000000000;
            public readonly static ulong QA_QUADRILLION = 1000000000000000;
            public readonly static ulong QI_QUINTILLION = 1000000000000000000;
        }
    }
}
