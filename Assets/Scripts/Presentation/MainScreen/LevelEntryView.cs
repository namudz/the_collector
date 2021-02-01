using Game;
using Game.Level;
using Services;
using Services.SceneLoader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.MainScreen
{
    public class LevelEntryView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelNameText;
        [SerializeField] private LeaderboardEntryView[] _entries;
        [SerializeField] private Button _playButton;
        
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
        }
        
        private void PlayLevel()
        {
            var game = ServiceLocator.Instance.GetService<IGame>();
            game.SetCurrentLevelData(_levelData);
            
            var sceneLoader = ServiceLocator.Instance.GetService<ISceneLoader>();
            sceneLoader.LoadScene(SceneConstants.Scene.Game);
        }
    }
}