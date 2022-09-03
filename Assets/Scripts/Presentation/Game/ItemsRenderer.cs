using Services.GameObjectPooling;
using UnityEngine;

namespace Presentation.Game
{
    public class ItemsRenderer : MonoBehaviour
    {
        [SerializeField] private GameObjectPoolCollection _poolsCollection;

        private void Awake()
        {
            _poolsCollection.Initialize(true);
        }

        public void RenderItems()
        {
            
        }
    }
}