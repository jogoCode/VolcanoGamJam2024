using UnityEngine;
using UnityEngine.AI;

public class EnemyCAC : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private GameObject _player;

    private void Update()
    {
        _agent.SetDestination(_player.transform.position);
    }
}
