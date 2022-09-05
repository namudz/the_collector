using InterfaceAdapters.Game.Level;
using InterfaceAdapters.Services;
using UnityEngine;

namespace PresentationLayer.MainScreen
{
    public class MainScreenView : MonoBehaviour
    {
        [SerializeField] private Transform _layoutParent;
        [SerializeField] private LevelEntryView _levelEntryPrefab;
        
        private ILevelsRepository _levelsRepository;

        private void Awake()
        {
            _levelsRepository = ServiceLocator.Instance.GetService<ILevelsRepository>();
            
            LoadLevelEntries();
        }

        private void LoadLevelEntries()
        {
            var count = _levelsRepository.LevelsCount;
            
            for (var i = 0; i < count; i++)
            {
                var instance = Instantiate(_levelEntryPrefab, _layoutParent);
                instance.SetLevelData(_levelsRepository.GetLevel(i));
            }
        }
    }
}