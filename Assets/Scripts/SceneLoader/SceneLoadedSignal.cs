using EventDispatcher;

namespace SceneLoader
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