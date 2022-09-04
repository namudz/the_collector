using InterfaceAdapters.Services.EventDispatcher;
using UnityEngine;

namespace InterfaceAdapters.Signals
{
    public class GameSceneLoadedSignal : ISignal
    {
    }
    
    public class GameStartedSignal : ISignal
    {
    }
    
    public class GameOverSignal : ISignal
    {
    }
    
    public class GameResetSignal : ISignal
    {
    }

    public class LoadMazeItemsToRestartSignal : ISignal
    {
    }
    
    public class GameDestroyedSignal : ISignal
    {
    }

    public class ShowCoinFxSignal : ISignal
    {
        public readonly Vector3 SpawnPosition;
        public readonly int Amount;

        public ShowCoinFxSignal(Vector3 spawnPosition, int amount)
        {
            SpawnPosition = spawnPosition;
            Amount = amount;
        }
    }

    public class RespawnCollectibleSignal : ISignal
    {
        public readonly float RespawnTime;
        public readonly Vector3 Position;

        public RespawnCollectibleSignal(float respawnTime, Vector3 position)
        {
            RespawnTime = respawnTime;
            Position = position;
        }
    }
}