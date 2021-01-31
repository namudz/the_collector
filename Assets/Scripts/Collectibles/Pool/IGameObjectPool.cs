using UnityEngine;

namespace Collectibles.Pool
{
    public interface IGameObjectPool
    {
        void InstantiateInitialElements(Transform parent);
        GameObject GetInstance(Vector3 newPosition);
        void BackToPool(GameObject instance);
    }
}