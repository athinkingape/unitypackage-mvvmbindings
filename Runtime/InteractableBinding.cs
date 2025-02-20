using MVVM.Bindings.Base;
using MVVM.Models;
using UnityEngine.UI;

namespace MVVM.Bindings {
    public class InteractableBinding : BaseValueBinding<bool> {
        private readonly Selectable _selectable;
        private readonly bool _isInverted;

        public InteractableBinding(IObservableValue<bool> observableValue, Selectable selectable, bool isInverted = false) : base(observableValue)
        {
            _selectable = selectable;
            _isInverted = isInverted;
        }

        public InteractableBinding(bool value, Selectable selectable, bool isInverted = false):base(value) {
            _selectable = selectable;
            _selectable.interactable = value;
            _isInverted = isInverted;
        }

        protected override void OnUpdate(bool value)
        {
            _selectable.interactable = _isInverted ? !value : value;
        }
    }
}
