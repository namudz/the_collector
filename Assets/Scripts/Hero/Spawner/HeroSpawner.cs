using UnityEngine;

public class HeroSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _heroPrefab;

    public void Spawn()
    {
        var instance = Instantiate(_heroPrefab, _spawnPoint.position, Quaternion.identity, transform);
        instance.SetActive(true);
    }
}