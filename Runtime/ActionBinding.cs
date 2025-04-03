using System;
using MVVM.Bindings.Base;
using MVVM.Models;

namespace MVVM.Bindings
{
    public class ActionBinding<T> : BaseValueBinding<T>
    {
        private readonly Action<T> _action;
        
        public ActionBinding(IObservableValue<T> observableValue, Action<T> action) : base(observableValue)
        {
            _action = action;
        }
        
        public ActionBinding(T value, Action<T> action) : base(value)
        {
            _action = action;
            OnUpdate(value);
        }

        protected override void OnUpdate(T value)
        {
            _action.Invoke(value);
        }
    }
}