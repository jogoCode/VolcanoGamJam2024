using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerGates : MonoBehaviour
{
    [SerializeField] private List<GameObject> _gates;
    [SerializeField] private SpawnerEnemies _spawnerEnemies;
    [SerializeField] private bool _zoneTerminated;

    private void Start()
    {
        _zoneTerminated = false;
    }
    private void Update()
    {
        if(_spawnerEnemies._enemyVinquish == true && _zoneTerminated == false)
        {
            foreach (GameObject g in _gates)
            {
                g.SetActive(false);
                _zoneTerminated = true;
            }
            Debug.Log("gg");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && _zoneTerminated == false)
        {
            foreach (var gate in _gates)
            {
                if (gate.gameObject.activeSelf == false)
                {
                    gate.gameObject.SetActive(true);
                }
            }
        }
    }
}
