using System.Collections.Generic;
using DomainLayer;
using InterfaceAdapters.Game.Level;

namespace InterfaceAdapters.UseCases
{
    public interface ILoadLevelsRepositoryUseCase
    {
        void LoadLevels(IEnumerable<Level> levels);
    }
}