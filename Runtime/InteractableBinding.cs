using MVVM.Bindings.Base;
using MVVM.Models;
using UnityEngine.UI;

namespace MVVM.Bindings {
    public class InteractableBinding : BaseValueBinding<bool> {
        private readonly Selectable _selectable;

        public InteractableBinding(IObservableValue<bool> observableValue, Selectable selectable) : base(observableValue)
        {
            _selectable = selectable;
        }

        public InteractableBinding(bool value, Selectable selectable):base(value) {
            _selectable = selectable;
            _selectable.interactable = value;
        }

        protected override void OnUpdate(bool value)
        {
            _selectable.interactable = value;
        }
    }
}
