using System;
using MVVM.Bindings.Base;
using MVVM.Commands;
using UnityEngine.UI;

namespace MVVM.Bindings
{
    public class ButtonBinding : ILifecycleBinding
    {
        private readonly Button _button;
        private readonly ICommand _command;
        private readonly Action _action;
        
        public ButtonBinding(Button button, ICommand command)
        {
            _button = button;
            _command = command;
        }

        public ButtonBinding(Button button, Action action)
        {
            _button = button;
            _action = action;
        }

        public void OnEnable()
        {
            _button.onClick.AddListener(_command == null ? _action.Invoke : _command.Execute);
        }

        public void OnDisable()
        {
            _button.onClick.RemoveListener(_command == null ? _action.Invoke : _command.Execute);
        }

        public void OnDestroy()
        {
            _button.onClick.RemoveListener(_command == null ? _action.Invoke : _command.Execute);
        }
    }
}