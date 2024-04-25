using MVVM.Bindings.Base;
using MVVM.Commands;
using UnityEngine.UI;

namespace MVVM.Bindings
{
    public class ButtonBinding : ILifecycleBinding
    {
        private readonly Button _button;
        private readonly ICommand _command;
        
        public ButtonBinding(Button button, ICommand command)
        {
            _button = button;
            _command = command;
        }

        public void OnEnable()
        {
            _button.onClick.AddListener(_command.Execute);
        }

        public void OnDisable()
        {
            _button.onClick.RemoveListener(_command.Execute);
        }

        public void OnDestroy()
        {
            _button.onClick.RemoveListener(_command.Execute);
        }
    }
}