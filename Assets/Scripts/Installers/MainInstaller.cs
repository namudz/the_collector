using EventDispatcher;
using SceneLoader;

namespace Installers
{
    public class MainInstaller
    {
        public static MainInstaller Instance => _instance ?? (_instance = new MainInstaller());
        public ISceneLoader SceneLoader { get; private set; }
        public IEventDispatcher EventDispatcher { get; private set; }
        
        private static MainInstaller _instance;
        private readonly MainViews _mainViews;

        private MainInstaller()
        {
            _instance = this;
        }
        
        public MainInstaller(MainViews mainViews): this()
        {
            _mainViews = mainViews;
        }

        public void InjectDependencies()
        {
            InitializeDependencies();
            _mainViews.MainScreenView.InjectDependencies(SceneLoader);
        }

        private void InitializeDependencies()
        {
            EventDispatcher = new EventDispatcher.EventDispatcher();
            SceneLoader = new SceneLoader.SceneLoader(EventDispatcher);
        }
    }
}