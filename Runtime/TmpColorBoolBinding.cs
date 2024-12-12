using MVVM.Bindings.Base;
using MVVM.Models;
using TMPro;
using UnityEngine;

namespace Bindings {
    public class TmpColorBoolBinding : BaseValueBinding<bool> {

        private readonly TMP_Text _label;
        private readonly Color _trueColor;
        private readonly Color _falseColor;

        public TmpColorBoolBinding(IObservableValue<bool> observableValue, TMP_Text label, Color trueColor, Color falseColor) : base(observableValue) {
            _label = label;
            _trueColor = trueColor;
            _falseColor = falseColor;
        }

        public TmpColorBoolBinding(bool value, TMP_Text label, Color trueColor, Color falseColor) : base(value) {
            _label = label;
            _trueColor = trueColor;
            _falseColor = falseColor;

            _label.color = value ? _trueColor : _falseColor;
        }

        protected override void OnUpdate(bool value) {
            _label.color = value ? _trueColor : _falseColor;
        }
    }
}
