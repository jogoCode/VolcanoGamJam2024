using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log("zizi");
        if(m_playerController.GetPlayerStateManager().GetState() == PlayerStateManager.PlayerStates.ATK) return;
        m_playerController.GetPlayerMovement().ApplyImpulse(m_playerController.GetModel().transform.forward, 15); // Appliquer l'impulsion vers l'avant
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            HealthSysteme healthSysteme = other.gameObject.GetComponent<HealthSysteme>();
          
            healthSysteme.TakeDamages(m_playerDamage);
            Oscillator oscillator = other.gameObject.GetComponent<Oscillator>();
            if (oscillator == null) return;
            //TODO Ajouter un KNOCK BACK
            oscillator.StartOscillator(15);

        }
    }
}
