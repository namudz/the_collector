using UnityEngine;

public class HeroSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private GameObject _hero;

    private void Start() 
    {
        Spawn();   
    }

    public void Spawn()
    {
        _hero.SetActive(true);
    }
}