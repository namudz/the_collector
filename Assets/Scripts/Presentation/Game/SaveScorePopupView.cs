using System;
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
            var gameScoreboard = ServiceLocator.Instance.GetService<IGameScoreboard>();
            var playerName = _inputField.text;
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