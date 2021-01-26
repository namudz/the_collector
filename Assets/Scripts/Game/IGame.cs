using System.Collections.Generic;

public interface IGame
{
    void Load();
    void Start();
    void Reset();
    void InjectSpawners(ISpawner heroSpawner, IEnumerable<ISpawner> collectibleSpawners);
}