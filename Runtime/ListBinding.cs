using System.Collections.Generic;
using System.Linq;
using MVVM.Bindings.Base;
using MVVM.Models;
using MVVM.ViewModels;
using MVVM.Views;
using UnityEngine;

namespace MVVM.Bindings
{
    public class ListBinding<TView, TViewModel> : BaseValueBinding<IEnumerable<TViewModel>> 
        where TViewModel : BaseViewModel
        where TView : BaseView<TViewModel>
    {
        private readonly IEnumerable<TView> _instantiatedViews;
        private readonly Transform _container;
        private readonly TView _prefab;

        public ListBinding(IEnumerable<TView> instantiatedViews, IObservableValue<IEnumerable<TViewModel>> observableValue) : base(observableValue)
        {
            _instantiatedViews = instantiatedViews;
        }
        
        public ListBinding(IEnumerable<TView> instantiatedViews, IEnumerable<TViewModel> value) : base(value)
        {
            _instantiatedViews = instantiatedViews;
        }

        public ListBinding(Transform container, TView prefab, IObservableValue<IEnumerable<TViewModel>> observableValue) : base(
            observableValue)
        {
            _container = container;
            _prefab = prefab;
        }

        protected override void OnUpdate(IEnumerable<TViewModel> value)
        {
            if (_instantiatedViews == null)
            {
                InstantiateViews(value);
            }
            else
            {
                ManageInstantiatedViews(value);
            }
        }

        private void InstantiateViews(IEnumerable<TViewModel> value)
        {
            var max = value.Count();
            var viewModelsEnumerator = value.GetEnumerator();

            while (max > 0)
            {
                viewModelsEnumerator.MoveNext();

                var viewModel = viewModelsEnumerator.Current;
                var view = Object.Instantiate<TView>(_prefab, _container);
                
                view.Setup(viewModel);

                max--;
            }
            
            viewModelsEnumerator.Dispose();
        }

        private void ManageInstantiatedViews(IEnumerable<TViewModel> value)
        {
            var max = _instantiatedViews.Count();

            var viewsEnumerator = _instantiatedViews.GetEnumerator();
            var viewModelsEnumerator = value.GetEnumerator();

            while (max > 0)
            {
                viewsEnumerator.MoveNext();
                viewModelsEnumerator.MoveNext();
                
                var view = viewsEnumerator.Current;
                var viewModel = viewModelsEnumerator.Current;

                view.Setup(viewModel);

                max--;
            }
            
            viewsEnumerator.Dispose();
            viewModelsEnumerator.Dispose();
        }
    }
}