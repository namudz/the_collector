using EventDispatcher;
using SceneLoader;

namespace Installers
{
    public class MainInstaller
    {
        private readonly MainViews _mainViews;
        private ISceneLoader _sceneLoader;
        private IEventDispatcher _eventDispatcher;

        public MainInstaller(MainViews mainViews)
        {
            _mainViews = mainViews;
        }

        public void InjectDependencies()
        {
            InitializeDependencies();
            _mainViews.MainScreenView.InjectDependencies(_sceneLoader);
        }

        private void InitializeDependencies()
        {
            _eventDispatcher = new EventDispatcher.EventDispatcher();
            _sceneLoader = new SceneLoader.SceneLoader(_eventDispatcher);
        }
    }
}