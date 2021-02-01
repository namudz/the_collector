using Services.EventDispatcher;

namespace Services.SceneLoader
{
    public class SceneLoadedSignal : ISignal
    {
        public readonly SceneConstants.Scene Scene;

        public SceneLoadedSignal(SceneConstants.Scene scene)
        {
            Scene = scene;
        }
    }
}