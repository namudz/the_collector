using UnityEngine;

namespace PresentationLayer.Game.Spawners
{
    public abstract class Spawner : MonoBehaviour, ISpawner
    {
        [Header("Base Components")]
        [SerializeField] protected Transform _spawnPoint;

        public abstract void Spawn();
        public abstract void Reset();
    }
}