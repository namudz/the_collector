using System;
using System.Collections.Generic;
using DomainLayer;

namespace InterfaceAdapters.UseCases
{
    public interface ILoadConfigurationsUseCase
    {
        void Load(IEnumerable<Level> levels, Action onCompleted);
    }
}