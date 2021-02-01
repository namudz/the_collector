using UnityEngine;

namespace Services.Pooling
{
    public interface IGameObjectPool<T> where T : IPoolable
    {
        void InstantiateInitialElements(Transform parent);
        GameObject GetInstance(Vector3 newPosition);
        void BackToPool(GameObject instance);
    }
}