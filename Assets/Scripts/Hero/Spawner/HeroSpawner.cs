using UnityEngine;

public class HeroSpawner : Spawner
{
    [Header("Components")]
    [SerializeField] private GameObject _heroPrefab;

    public override void Spawn()
    {
        var instance = Instantiate(_heroPrefab, _spawnPoint.position, Quaternion.identity, transform);
        instance.SetActive(true);
    }
}