using System;
using MVVM.Models;

namespace MVVM.Bindings {
    public class ActionSignalBinding : BaseSignalBinding {
        private readonly Action _action;

        public ActionSignalBinding(ISignal signal, Action action) : base(signal) {
            _action = action;
        }
        protected override void OnNotify() {
            _action.Invoke();
        }
    }
    
    public class ActionSignalBinding<T> : BaseSignalBinding<T> {
        private readonly Action<T> _action;

        public ActionSignalBinding(ISignal<T> signal, Action<T> action) : base(signal) {
            _action = action;
        }
        protected override void OnNotify(T value) {
            _action.Invoke(value);
        }
    }
}