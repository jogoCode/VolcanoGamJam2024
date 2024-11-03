using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField]Enemy enemy ;
    void Start()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
     
        if (other.tag == "Player")
        {
            Debug.Log("AAAAAAAAAAAAAA");
            HealthSysteme playerHealth = other.gameObject.GetComponent<HealthSysteme>();
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.ApplyImpulse(transform.forward, 15f);
            playerHealth.TakeDamages(enemy.DAMAGE);

        }
    }
}
