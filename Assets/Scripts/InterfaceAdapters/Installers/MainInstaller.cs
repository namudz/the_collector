using EventDispatcher;
using SceneLoader;

namespace InterfaceAdapters.Installers
{
    public class MainInstaller : IMainInstaller
    {
        public void Register()
        {
            ServiceLocator.Instance.RegisterService(this);
        }

        public void InstallDependencies()
        {
            var eventDispatcher = new EventDispatcher.EventDispatcher();
            var sceneLoader = new SceneLoader.SceneLoader(eventDispatcher);

            ServiceLocator.Instance.RegisterService<IEventDispatcher>(eventDispatcher);
            ServiceLocator.Instance.RegisterService<ISceneLoader>(sceneLoader);
        }
    }
}