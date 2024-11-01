using UnityEngine;
using UnityEngine.AI;

public class EnemyRANGE : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _safeZone;
    

    private void Update()
    {
        _agent.SetDestination(_player.transform.position);
    }
}
