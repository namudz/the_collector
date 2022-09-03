using System;
using UnityEngine;

namespace Services.GameObjectPooling
{
    public class PoolableGameObject : MonoBehaviour
    {
        public event Action OnActivated;
        public event Action OnDeactivated;
        
        public PoolableGameObjectDto PoolData { get; set; }

        public void BackToPool()
        {
            PoolData.PoolCollection.BackToPool(PoolData.Id, this);
        }

        public void Activate(Vector3 position, bool useLocalPosition = false)
        {
            if (useLocalPosition)
            {
                transform.localPosition = position;
            }
            else
            {
                transform.position = position;
            }
            
            gameObject.SetActive(true);

            OnActivated?.Invoke();
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);

            OnDeactivated?.Invoke();
        }
    }
}