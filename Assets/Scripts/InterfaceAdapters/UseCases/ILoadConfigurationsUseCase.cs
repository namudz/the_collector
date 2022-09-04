using System;
using System.Collections.Generic;
using DomainLayer;
using InterfaceAdapters.Game.Level;

namespace InterfaceAdapters.UseCases
{
    public interface ILoadConfigurationsUseCase
    {
        void Load(IEnumerable<Level> levels, Action onCompleted);
    }
}