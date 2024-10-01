using MVVM.Bindings.Base;
using MVVM.Bindings.Utils;
using MVVM.Models;
using TMPro;

namespace MVVM.Bindings {
    public class TruncatedTmpTextBinding : BaseValueBinding<int> {

        private readonly TMP_Text _text;

        public TruncatedTmpTextBinding(IObservableValue<int> observableValue, TMP_Text text) : base(observableValue) {
            _text = text;
        }

        public TruncatedTmpTextBinding(int value, TMP_Text text) : base(value) {
            _text = text;
        }

        protected override void OnUpdate(int value) {
            _text.text = NumberFormatter.FormatNumber(value, NumberFormatter.NameOfNumber.k_KILO, false, true);
        }
    }
}
