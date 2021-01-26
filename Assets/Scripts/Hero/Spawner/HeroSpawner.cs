using UnityEngine;

public class HeroSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _hero;

    public void Spawn()
    {
        // TODO - Get the reference of the object to spawn differently
        _hero.transform.position = _spawnPoint.position;
        _hero.SetActive(true);
    }
}