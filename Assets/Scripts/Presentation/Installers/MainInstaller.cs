using EventDispatcher;
using SceneLoader;
using UnityEngine;

namespace Presentation.Installers
{
    public class MainInstaller : MonoBehaviour
    {
        private void Awake()
        {
            InitializeDependencies();
        }

        private static void InitializeDependencies()
        {
            var eventDispatcher = new EventDispatcher.EventDispatcher();
            var sceneLoader = new SceneLoader.SceneLoader(eventDispatcher);

            ServiceLocator.Instance.RegisterService<IEventDispatcher>(eventDispatcher);
            ServiceLocator.Instance.RegisterService<ISceneLoader>(sceneLoader);
        }
    }
}