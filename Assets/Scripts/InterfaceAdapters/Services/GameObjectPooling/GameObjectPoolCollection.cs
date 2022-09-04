using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InterfaceAdapters.Services.GameObjectPooling
{
    public class GameObjectPoolCollection : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private GameObjectPoolDataConfig2 _poolsConfig;

        private Transform _transform;
        private Dictionary<string, Stack<PoolableGameObject>> _pools;

        public GameObjectPoolDataConfig2 Config => _poolsConfig;

        public void Initialize(bool instantiateInitialElements)
        {
            _transform = transform;
            InitializePools();
            
            if(instantiateInitialElements)
            {
                InstantiateInitialElements();
            }
        }

        public PoolableGameObject GetInstance(string objectId, Vector3 newPosition, bool useLocalPosition = true)
        {
            if (!_pools.ContainsKey(objectId) || _pools[objectId] == null)
            {
                return null;
            }

            if (_pools[objectId].Count == 0)
            {
                if (!InstantiateElement(objectId))
                {
                    return null;
                }
            }

            var instance = _pools[objectId].Pop();
            instance.Activate(newPosition, useLocalPosition);

            return instance;
        }

        public void BackToPool(string objectId, PoolableGameObject instance)
        {
            if (_pools.ContainsKey(objectId))
            {
                instance.Deactivate();
                _pools[objectId].Push(instance);
            }
        }

        private void InitializePools()
        {
            _pools = new Dictionary<string, Stack<PoolableGameObject>>(_poolsConfig.Pools.Length);
            foreach (var poolData in _poolsConfig.Pools)
            {
                _pools.Add(poolData.Id, new Stack<PoolableGameObject>(poolData.InitialAmount));
            }
        }

        private void InstantiateInitialElements()
        {
            foreach (var poolData in _pools.Select(pool => GetPoolData(pool.Key)))
            {
                for (var i = 0; i < poolData.InitialAmount; i++)
                {
                    InstantiateElement(poolData.Id);
                }
            }
        }

        private bool InstantiateElement(string objectId)
        {
            var poolData = GetPoolData(objectId);

            var instance = Instantiate(poolData.GameObject, _transform.position, Quaternion.identity, _transform);
            instance.PoolData = new PoolableGameObjectDto(objectId, this);

            BackToPool(objectId, instance);
            return true;
        }

        private GameObjectPoolData2 GetPoolData(string objectId)
        {
            var poolData = _poolsConfig.GetPoolData(objectId);
            if (poolData == null)
            {
                throw new NullReferenceException($"GameObjectPoolCollection - Pool not found with given id {objectId}");
            }

            return poolData;
        }
    }
}