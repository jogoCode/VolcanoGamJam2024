using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemies : MonoBehaviour
{
    public List<GameObject> _enemies;
    public bool _enemyActivated;
    public bool _enemyVinquish;

    private void Start()
    {
        _enemyActivated = false;
        _enemyVinquish = false;
    }
    private void Update()
    {
        var i = 0;
        foreach (var enemy in _enemies) 
        {
            if (_enemyActivated == false) return;
            if (enemy.gameObject.activeSelf == false) 
            { 
                i++; 
            }

        }
        if ( i == _enemies.Count )
        {
            _enemyVinquish = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (var enemy in _enemies)
            {
                if (enemy.gameObject.activeSelf == false && _enemyActivated == false)
                {
                    _enemyVinquish = false;
                    enemy.gameObject.SetActive(true);
                }
            }
            _enemyActivated = true;
        }
    }
}

