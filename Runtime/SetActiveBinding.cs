using MVVM.Bindings.Base;
using MVVM.Models;
using UnityEngine;

namespace MVVM.Bindings
{
    public class SetActiveBinding : BaseValueBinding<bool>
    {
        private readonly GameObject _target;
        private readonly bool _isInverted;
        
        public SetActiveBinding(IObservableValue<bool> observableValue, GameObject target, bool isInverted = false) : base(observableValue)
        {
            _target = target;
            _isInverted = isInverted;
        }

        protected override void OnUpdate(bool value)
        {
            _target.SetActive(_isInverted ? !value : value);
        }
    }
}