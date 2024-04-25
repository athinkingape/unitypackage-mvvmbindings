using MVVM.Bindings.Base;
using MVVM.Models;
using UnityEngine.UI;

namespace MVVM.Bindings
{
    public class FilledImageBinding : BaseValueBinding<float>
    {
        private readonly Image _image;
        
        public FilledImageBinding(IObservableValue<float> observableValue, Image image) : base(observableValue)
        {
            _image = image;
        }

        protected override void OnUpdate(float value)
        {
            _image.fillAmount = value;
        }
    }
}