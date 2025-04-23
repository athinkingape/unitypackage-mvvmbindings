using System;
using MVVM.Bindings.Base;
using MVVM.Commands;
using MVVM.Models;
using UnityEngine.UI;

namespace MVVM.Bindings
{
    public class ButtonBinding : ILifecycleBinding
    {
        private readonly Button _button;
        private readonly Action _action;
        private bool _subscribed;
        private readonly ObservableBinding<ICommand> _commandBinding;
        private ICommand _command;
        
        public ButtonBinding(Button button, ICommand command)
        {
            _button = button;
            _command = command;
        }
        
        public ButtonBinding(Button button, IObservableValue<ICommand> command)
        {
            _button = button;
            _command = command.Value;
            _commandBinding = new ObservableBinding<ICommand>(command, OnCommandChanged);
        }

        public ButtonBinding(Button button, Action action)
        {
            _button = button;
            _action = action;
        }

        public void OnEnable()
        {
            if (_subscribed) return;
            
            _button.onClick.AddListener(_command == null ? _action.Invoke : _command.Execute);
            _subscribed = true;
        }

        public void OnDisable()
        {
            if (!_subscribed) return;
            
            _button.onClick.RemoveListener(_command == null ? _action.Invoke : _command.Execute);
            _subscribed = false;
        }

        public void OnDestroy()
        {
            if (!_subscribed) return;
            
            _button.onClick.RemoveListener(_command == null ? _action.Invoke : _command.Execute);
            _subscribed = false;
            
            _commandBinding?.OnDestroy();
        }
        
        private void OnCommandChanged(ICommand command) {
            OnDisable();
            _command = command;
            OnEnable();
        }
    }
}