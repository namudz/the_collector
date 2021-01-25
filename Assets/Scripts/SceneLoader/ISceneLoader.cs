using UnityEngine.SceneManagement;

namespace SceneLoader
{
    public interface ISceneLoader
    {
        void LoadScene(SceneConstants.Scene scene, LoadSceneMode mode = LoadSceneMode.Single);
    }
}