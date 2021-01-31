using UnityEngine;

namespace Collectibles.Pool
{
    public interface IGameObjectPool<T> where T : ICollectible
    {
        void InstantiateInitialElements(Transform parent);
        GameObject GetInstance(Vector3 newPosition);
    }
}