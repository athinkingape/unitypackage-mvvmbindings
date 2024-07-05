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
        private readonly Transform _container;
        private readonly TView _prefab;
        
        private HashSet<TView> _instantiatedViews; 

        public ListBinding(IEnumerable<TView> instantiatedViews, IObservableValue<IEnumerable<TViewModel>> observableValue) : base(observableValue)
        {
            _instantiatedViews = new HashSet<TView>(instantiatedViews);
            _container = instantiatedViews.First().transform;
        }
        
        public ListBinding(IEnumerable<TView> instantiatedViews, IEnumerable<TViewModel> value) : base(value)
        {
            _instantiatedViews = new HashSet<TView>(instantiatedViews);
            _container = instantiatedViews.First().transform;
        }

        public ListBinding(Transform container, TView prefab, IObservableValue<IEnumerable<TViewModel>> observableValue) : base(
            observableValue)
        {
            _instantiatedViews = new HashSet<TView>();
            _container = container;
            _prefab = prefab;
        }

        protected override void OnUpdate(IEnumerable<TViewModel> value)
        {
            ManageInstantiatedViews(value);
        }

        private TView Instantiate() => Object.Instantiate(_prefab, _container);

        private void ManageInstantiatedViews(IEnumerable<TViewModel> value)
        {
            var max = Mathf.Max(_instantiatedViews.Count, value.Count());

            var viewsEnumerator = _instantiatedViews.ToList().GetEnumerator();
            var viewModelsEnumerator = value.GetEnumerator();

            while (max > 0)
            {
                var view = viewsEnumerator.MoveNext() ? viewsEnumerator.Current : null;
                var viewModel = viewModelsEnumerator.MoveNext() ? viewModelsEnumerator.Current : null;

                if (view == null)
                {
                    view = Instantiate();
                    _instantiatedViews.Add(view);
                }

                if (viewModel == null)
                {
                    view.gameObject.SetActive(false);
                }
                else
                {
                    view.Setup(viewModel);
                }

                max--;
            }
            
            viewsEnumerator.Dispose();
            viewModelsEnumerator.Dispose();
        }
    }
}