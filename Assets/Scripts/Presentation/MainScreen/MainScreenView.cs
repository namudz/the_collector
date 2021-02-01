using Game.Level;
using Services;
using TMPro;
using UnityEngine;

namespace Presentation.MainScreen
{
    public class MainScreenView : MonoBehaviour
    {
        [SerializeField] private LevelEntryView[] _levelEntries;
        [SerializeField] private TextMeshProUGUI _versionText;

        private void Awake()
        {
            SetGameVersion();
        }

        private void Start()
        {
            UpdateLevelEntries();
        }
        
        private void UpdateLevelEntries()
        {
            var levelsRepository = ServiceLocator.Instance.GetService<ILevelsRepository>();
            for (var i = 0; i < _levelEntries.Length; i++)
            {
                _levelEntries[i].SetLevelData(levelsRepository.GetLevel(i));
            }
        }

        private void SetGameVersion()
        {
            _versionText.SetText($"v{Application.version}");
        }
    }
}