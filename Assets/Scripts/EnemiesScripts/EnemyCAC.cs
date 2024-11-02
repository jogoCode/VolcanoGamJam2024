using UnityEngine;
using UnityEngine.AI;

public class EnemyCAC : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private GameObject _player;
    [SerializeField] private int _damages;


    private void Update()
    {
        _agent.SetDestination(_player.transform.position);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            HealthSysteme playerHealth = other.gameObject.GetComponent<HealthSysteme>();
            playerHealth.TakeDamages(_damages);

      
        }
    }
}
