using InterfaceAdapters.Services.GameObjectPooling;
using UnityEngine;

namespace PresentationLayer.Game
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