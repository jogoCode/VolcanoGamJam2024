using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    [SerializeField] int m_playerDamage = 2;


    public void Attack()
    {
        PlayerController playerController = GetComponentInParent<PlayerController>();
        playerController.GetPlayerMovement().ApplyImpulse(playerController.GetModel().transform.forward); // Appliquer l'impulsion vers l'avant
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
