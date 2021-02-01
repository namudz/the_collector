using Game;
using Game.Level;
using InterfaceAdapters;
using Services;
using Services.DataPersistence;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.Game
{
    public class SaveScorePopupView : MonoBehaviour
    {
        [SerializeField] private Canvas _myCanvas;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _cancelBackgroundButton;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _cancelButton;

        private void Awake()
        {
            _confirmButton.onClick.AddListener(SaveScore);
            _cancelBackgroundButton.onClick.AddListener(Hide);
            _cancelButton.onClick.AddListener(Hide);
            Hide();
        }
        
        public void Show()
        {
            EnableCanvas(true);
        }

        private void SaveScore()
        {
            var useCase = new SaveScoreUseCase(
                ServiceLocator.Instance.GetService<IGame>(),
                ServiceLocator.Instance.GetService<IGameScoreboard>(),
                ServiceLocator.Instance.GetService<ILevelsRepository>(),
                ServiceLocator.Instance.GetService<IDataPersistence>()
            );
            
            useCase.SaveScore(_inputField.text);
            
            EnableCanvas(false);
        }

        private void Hide()
        {
            EnableCanvas(false);
        }
        
        private void EnableCanvas(bool isEnabled)
        {
            _myCanvas.enabled = isEnabled;
        }
    }
}