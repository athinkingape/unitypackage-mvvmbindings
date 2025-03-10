using MVVM.Bindings.Base;
using MVVM.Models;
using UnityEngine;

namespace MVVM.Bindings {
    public class SetEnabledBinding : BaseValueBinding<bool>
    {
        private readonly Behaviour _target;
        private readonly bool _isInverted;
        
        public SetEnabledBinding(IObservableValue<bool> observableValue, Behaviour target, bool isInverted = false) : base(observableValue)
        {
            _target = target;
            _isInverted = isInverted;
        }

        public SetEnabledBinding(bool value, Behaviour target, bool isInverted = false) : base(value)
        {
            _target = target;
            _isInverted = isInverted;
        }

        protected override void OnUpdate(bool value)
        {
            _target.enabled = _isInverted ? !value : value;
        }
    }
}