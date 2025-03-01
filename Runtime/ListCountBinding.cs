using System;
using System.Collections.Generic;
using System.Linq;
using MVVM.Bindings.Base;
using MVVM.Models;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MVVM.Bindings {
    public class ListCountBinding : BaseValueBinding<int> {
        private readonly Transform _container;
        private readonly GameObject _prefab;

        private HashSet<GameObject> _instantiatedViews;

        public ListCountBinding(GameObject prefab, Transform container, IObservableValue<int> observableValue) : base(
            observableValue) {
            _prefab = prefab;
            _container = container;
            _instantiatedViews = new();
        }


        public ListCountBinding(GameObject prefab, Transform container, int value) : base(value) {
            _prefab = prefab;
            _container = container;
            _instantiatedViews = new();
        }


        protected override void OnUpdate(int value) {
            ManageInstantiatedViews(value);
        }

        private GameObject Instantiate() {
            if (_prefab == null) throw new ArgumentNullException("Prefab is not specified, but tried to instantiate");
            if (_container == null) throw new ArgumentNullException("Container for new element is null");
            return Object.Instantiate(_prefab, _container);
        }

        private void ManageInstantiatedViews(int count) {
            var max = Mathf.Max(_instantiatedViews.Count, count);

            var viewsEnumerator = _instantiatedViews.ToList().GetEnumerator();

            while (max > 0) {
                var view = viewsEnumerator.MoveNext() ? viewsEnumerator.Current : null;

                if (view == null) {
                    view = Instantiate();
                    _instantiatedViews.Add(view);
                }

                view.gameObject.SetActive(max <= count);
                max--;
            }

            viewsEnumerator.Dispose();
        }
    }
}