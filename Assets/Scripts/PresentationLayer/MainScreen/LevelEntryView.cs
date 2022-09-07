using InterfaceAdapters.Game;
using InterfaceAdapters.Game.Level;
using InterfaceAdapters.Services;
using InterfaceAdapters.Services.EventDispatcher;
using InterfaceAdapters.Services.SceneLoader;
using InterfaceAdapters.Signals;
using PresentationLayer.MainScreen.Leaderboard;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PresentationLayer.MainScreen
{
    public class LevelEntryView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelNameText;
        [SerializeField] private LeaderboardEntryView[] _entries;
        [SerializeField] private Button _playButton;
        [SerializeField] private CanvasGroup _newTagCanvasGroup;
        
        private const float AlphaVisible = 1f;
        private const float AlphaInvisible = 0f;
        
        private IGame _iGame;
        private Level _levelData;

        private void Awake()
        {
            _playButton.onClick.AddListener(PlayLevel);
        }

        public void SetLevelData(Level level)
        {
            _levelData = level;
            
            _levelNameText.SetText(level.DisplayName);
            for (var i = 0; i < _entries.Length; i++)
            {
                _entries[i].SetEntryData(i, level.Leaderboard.Entries[i]);
            }

            _newTagCanvasGroup.alpha = _levelData.IsNew ? AlphaVisible : AlphaInvisible;
        }

        private void PlayLevel()
        {
            var game = ServiceLocator.Instance.GetService<IGame>();
            game.CurrentLevel = _levelData;

            var eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcher>();
            eventDispatcher.Dispatch(new ShowLoadingScreenSignal(true));
            
            var sceneLoader = ServiceLocator.Instance.GetService<ISceneLoader>();
            sceneLoader.LoadScene(SceneConstants.Scene.Game, LoadSceneMode.Single);
        }
    }
}