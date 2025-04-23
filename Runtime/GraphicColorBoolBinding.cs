using MVVM.Bindings.Base;
using MVVM.Models;
using UnityEngine;
using UnityEngine.UI;

namespace MVVM.Bindings {
    public class GraphicColorBoolBinding : BaseValueBinding<bool> {

        private readonly Graphic _label;
        private readonly Color _trueColor;
        private readonly Color _falseColor;

        public GraphicColorBoolBinding(IObservableValue<bool> observableValue, Graphic label, Color trueColor, Color falseColor) : base(observableValue) {
            _label = label;
            _trueColor = trueColor;
            _falseColor = falseColor;
        }

        public GraphicColorBoolBinding(bool value, Graphic label, Color trueColor, Color falseColor) : base(value) {
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