using System.Collections.Generic;
using DomainLayer;

namespace InterfaceAdapters.UseCases
{
    public interface ILoadLevelsRepositoryUseCase
    {
        void LoadLevels(IEnumerable<Level> levels);
    }
}