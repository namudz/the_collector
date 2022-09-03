using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainLayer;

namespace InterfaceAdapters.UseCases
{
    public class LoadConfigurationsUseCase : ILoadConfigurationsUseCase
    {
        private readonly ILoadLevelsRepositoryUseCase _loadLevelsRepositoryUseCase;

        public LoadConfigurationsUseCase(ILoadLevelsRepositoryUseCase loadLevelsRepositoryUseCase)
        {
            _loadLevelsRepositoryUseCase = loadLevelsRepositoryUseCase;
        }

        public async void Load(IEnumerable<Level> levels, Action onCompleted)
        {
            // For loading things like levels, server configs, etc.
            
            // Since is not possible to access to PlayerPrefs outside the main thread (in order to load level leaderboards)
            // I'm simulating other async loadings
            _loadLevelsRepositoryUseCase.LoadLevels(levels);

            await Task.Run(SimulateLoadingStuff);
            
            onCompleted?.Invoke();
        }

        private static async Task SimulateLoadingStuff()
        {
            await Task.Delay(TimeSpan.FromSeconds(3f));
        }
    }
}