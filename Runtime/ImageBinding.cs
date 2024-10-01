using MVVM.Bindings.Base;
using MVVM.Models;
using UnityEngine;
using UnityEngine.UI;

namespace MVVM.Bindings {
    public class ImageBinding : BaseValueBinding<Sprite> {
        private readonly Image _image;
        
        public ImageBinding(IObservableValue<Sprite> observableValue, Image image) : base(observableValue)
        {
            _image = image;
        }
        
        public ImageBinding(Sprite sprite, Image image):base(sprite) {
            _image = image;
            _image.sprite = sprite;
        }

        protected override void OnUpdate(Sprite value)
        {
            _image.sprite = value;
        }
    }
}
