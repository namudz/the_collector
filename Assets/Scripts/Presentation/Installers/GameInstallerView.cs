using Game;
using UnityEngine;

namespace Presentation.Installers
{
    public class GameInstallerView : MonoBehaviour
    {
        private IGame _game;

        private void Awake()
        {
            _game = ServiceLocator.Instance.GetService<IGame>();
        }

        private void Start()
        {
            _game.Load();
        }

        private void Update()
        {
            _game.Tick();
        }
    }
}