using MVVM.Bindings.Base;
using MVVM.Models;
using TMPro;
using UnityEngine;

namespace MVVM.Bindings {
    public class TruncatedTimeTmpTextBinding : BaseValueBinding<float> {

        private readonly TMP_Text _text;

        public TruncatedTimeTmpTextBinding(IObservableValue<float> observableValue, TMP_Text text) : base(observableValue) {
            _text = text;
        }

        public TruncatedTimeTmpTextBinding(float value, TMP_Text text) : base(value) {
            _text = text;
        }

        protected override void OnUpdate(float value) {
            _text.text = FormatAbbreviatedTimeSpanFromSeconds((long)Mathf.Floor(value));
        }

        private static string FormatAbbreviatedTimeSpanFromSeconds(long totalSeconds, bool shorten = true,
                                                                  int unitSize = 0) {
            long s = totalSeconds;

            long seconds = s % 60;
            s /= 60;
            long minutes = s % 60;
            s /= 60;
            long hours = s % 24;
            s /= 24;
            long days = s;
            return FormatAbbreviatedTimeSpanHelper(days, hours, minutes, seconds, shorten, unitSize);
        }

        private const int TIME_SPAN_DAYS_THRESHOLD = 10;

        private static string FormatAbbreviatedTimeSpanHelper(long days, long hours, long minutes, long seconds,
                                                              bool shorten = true, int unitSize = 0,
                                                              bool shortenToMinutes = false) {
            string unitSizeOpen = "";
            string unitSizeClose = "";
            if (unitSize != 0) {
                unitSizeOpen = string.Format(format: "<size={0}>", unitSize);
                unitSizeClose = "</size>";
            }

            string format;
            if (days > 0) {
                if (shorten) {
                    if (days >= TIME_SPAN_DAYS_THRESHOLD) {
                        format = "{0} {4}Days{5}";
                    } else if (shortenToMinutes) {
                        format = "{0}{4}d{5} {1}{4}h{5} {2}{4}m{5}";
                    } else {
                        format = "{0}{4}d{5} {1}{4}h{5}";
                    }
                } else {
                    format = "{0}{4}d{5} {1}{4}h{5} {2}{4}m{5} {3}{4}s{5}";
                }
            } else if (hours > 0) {
                if (shorten) {
                    format = minutes + seconds > 0 ? "{1}{4}h{5} {2}{4}m{5}" : "{1}{4}h{5}";
                } else {
                    format = "{1}{4}h{5} {2}{4}m{5} {3}{4}s{5}";
                }
            } else if (minutes > 0) {
                if (shorten) {
                    format = seconds > 0 ? "{2}{4}m{5} {3}{4}s{5}" : "{2}{4}m{5}";
                } else {
                    format = "{2}{4}m{5} {3}{4}s{5}";
                }
            } else {
                format = "{3}{4}s{5}";
            }

            return string.Format(format, days, hours, minutes, seconds, unitSizeOpen, unitSizeClose);
        }
    }
}
