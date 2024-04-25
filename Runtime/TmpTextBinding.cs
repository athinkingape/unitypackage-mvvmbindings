using MVVM.Bindings.Base;
using MVVM.Models;
using TMPro;

namespace MVVM.Bindings
{
    public class TmpTextBinding : BaseValueBinding<string>
    {
        private TMP_Text _label;

        public TmpTextBinding(IObservableValue<string> observableValue, TMP_Text label) : base(observableValue)
        {
            _label = label;
        }
        
        public TmpTextBinding(string text, TMP_Text label):base((string)text)
        {
            _label = label;
        }

        protected override void OnUpdate(string value)
        {
            if (_label.text == value)
            {
                return;
            }
            
            _label.text = value;
        }
    }
}