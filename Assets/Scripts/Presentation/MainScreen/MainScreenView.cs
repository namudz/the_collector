using Game.Level;
using Services;
using UnityEngine;

namespace Presentation.MainScreen
{
    public class MainScreenView : MonoBehaviour
    {
        [SerializeField] private LevelEntryView[] _levelEntries;

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
    }
}