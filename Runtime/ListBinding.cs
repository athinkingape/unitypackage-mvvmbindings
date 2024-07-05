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
        private readonly List<TView> _viewsToDestroy = new(); 

        public ListBinding(IEnumerable<TView> instantiatedViews, IObservableValue<IEnumerable<TViewModel>> observableValue) : base(observableValue)
        {
            _instantiatedViews = new HashSet<TView>(instantiatedViews);
        }
        
        public ListBinding(IEnumerable<TView> instantiatedViews, IEnumerable<TViewModel> value) : base(value)
        {
            _instantiatedViews = new HashSet<TView>(instantiatedViews);
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
            _instantiatedViews = new HashSet<TView>();
            
            var max = value.Count();
            var viewModelsEnumerator = value.GetEnumerator();

            while (max > 0)
            {
                viewModelsEnumerator.MoveNext();

                var viewModel = viewModelsEnumerator.Current;
                var view = Instantiate();
                
                view.Setup(viewModel);

                max--;
            }
            
            viewModelsEnumerator.Dispose();
        }

        private TView Instantiate()
        {
            var view = Object.Instantiate(_prefab, _container);
            _instantiatedViews.Add(view);
            return view;
        }

        private void ManageInstantiatedViews(IEnumerable<TViewModel> value)
        {
            var max = Mathf.Max(_instantiatedViews.Count, value.Count());

            var viewsEnumerator = _instantiatedViews.GetEnumerator();
            var viewModelsEnumerator = value.GetEnumerator();

            while (max > 0)
            {
                viewsEnumerator.MoveNext();
                viewModelsEnumerator.MoveNext();
                
                var view = viewsEnumerator.Current;
                var viewModel = viewModelsEnumerator.Current;

                if (view == null)
                {
                    view = Instantiate();
                }

                if (viewModel == null)
                {
                    _viewsToDestroy.Add(view);
                }
                
                view.Setup(viewModel);

                max--;
            }
            
            viewsEnumerator.Dispose();
            viewModelsEnumerator.Dispose();

            DestroyUnusedViews();
        }

        private void DestroyUnusedViews()
        {
            foreach (var view in _viewsToDestroy)
            {
                _instantiatedViews.Remove(view);
                Object.Destroy(view);
            }
            
            _viewsToDestroy.Clear();
        }
    }
}