using System.Collections.Generic;
using UnityEngine;

namespace Collectibles.Pool
{
    public abstract class GameObjectPool<T> : IGameObjectPool<T> where T : ICollectible
    {
        private Transform _transform;
        private GameObject _prefab;
        private int _initialAmount;

        private Queue<GameObject> _inactiveInstances;
        private Queue<GameObject> _activeInstances;

        public GameObjectPool(GameObjectPoolData data)
        {
            _transform = data.RootTransform;
            _prefab = data.Prefab;
            _initialAmount = data.InitialAmount;
            
            _inactiveInstances = new Queue<GameObject>(_initialAmount);
            _activeInstances = new Queue<GameObject>(_initialAmount);
        }
        
        public void InstantiateInitialElements(Transform poolsParent)
        {
            _transform = poolsParent;
            for (var i = 0; i < _initialAmount; i++)
            {
                InstantiateElement();
            }
        }

        public GameObject GetInstance(Vector3 newPosition)
        {
            if (_inactiveInstances.Count == 0)
            {
                InstantiateElement();
            }
            
            var instance = _inactiveInstances.Dequeue();
            instance.transform.position = newPosition;
            _activeInstances.Enqueue(instance);
            instance.SetActive(true);
            
            return instance;
        }

        private void InstantiateElement()
        {
            var instance = GameObject.Instantiate(_prefab, _transform.position, Quaternion.identity, _transform);
            instance.SetActive(false);
            _inactiveInstances.Enqueue(instance);
        }
    }
}