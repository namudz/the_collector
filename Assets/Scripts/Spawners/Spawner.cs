using UnityEngine;

public abstract class Spawner : MonoBehaviour, ISpawner
{
    [Header("Base Components")]
    [SerializeField] protected Transform _spawnPoint;

    public abstract void Spawn();
}