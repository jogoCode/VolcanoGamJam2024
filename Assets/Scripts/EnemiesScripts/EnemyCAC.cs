using UnityEngine;
using UnityEngine.AI;

public class EnemyCAC : Enemy { 



    private void Update()
    {
        if (_agent.enabled == true)
        {
            _agent.SetDestination(_player.transform.position);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            HealthSysteme playerHealth = other.gameObject.GetComponent<HealthSysteme>();
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.ApplyImpulse(transform.forward,15f);
            playerHealth.TakeDamages(_damages);

      
        }
    }
}
