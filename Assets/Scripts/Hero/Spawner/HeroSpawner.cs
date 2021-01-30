using UnityEngine;

public class HeroSpawner : Spawner
{
    [Header("Components")]
    [SerializeField] private GameObject _heroPrefab;

    private GameObject _instance;

    public override void Spawn()
    {
        _instance = Instantiate(_heroPrefab, _spawnPoint.position, Quaternion.identity, transform);
        _instance.SetActive(true);
    }
}