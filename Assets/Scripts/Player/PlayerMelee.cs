using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMelee : MonoBehaviour
{
    PlayerController m_playerController;
    [SerializeField] int m_playerDamage = 2;


    public void Start()
    {
        m_playerController = GetComponentInParent<PlayerController>();
    }

    public void Attack()
    {
        if (m_playerController.GetPlayerStateManager().GetState() == PlayerStateManager.PlayerStates.DISTANCE) return;
        if (m_playerController.GetPlayerStateManager().GetState() == PlayerStateManager.PlayerStates.ATK) return;
        m_playerController.GetPlayerMovement().ApplyImpulse(m_playerController.GetModel().transform.forward, 15); // Appliquer l'impulsion vers l'avant
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            HealthSysteme healthSysteme = other.gameObject.GetComponent<HealthSysteme>();
            Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();
            NavMeshAgent agent = other.gameObject.GetComponent<NavMeshAgent>();
            Oscillator oscillator = other.gameObject.GetComponent<Oscillator>();
            if (!other.GetComponent<EnemyKAMIKAZE>()){
                healthSysteme.TakeDamages(m_playerDamage);
               
                if (oscillator == null) return;
                //TODO Ajouter un KNOCK BACK
                agent.enabled = false;
            }
            rigidbody.isKinematic = false;
            Vector3 impulseDir = new Vector3(m_playerController.GetModel().transform.forward.x, 0,m_playerController.GetModel().transform.forward.z).normalized;
            rigidbody.AddForce(impulseDir*3,ForceMode.Impulse);
            oscillator.StartOscillator(15);


        }
    }
}
