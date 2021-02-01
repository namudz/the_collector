using UnityEngine;

namespace Collectibles.Pool
{
    public interface IGameObjectPool<T> where T : IPoolable
    {
        void InstantiateInitialElements(Transform parent);
        GameObject GetInstance(Vector3 newPosition);
        void BackToPool(GameObject instance);
    }

    public interface IPoolable
    {
    }
}