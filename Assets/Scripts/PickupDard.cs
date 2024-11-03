using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDard : MonoBehaviour
{

    [SerializeField] int m_value = 2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerDistance playerDistance = other.gameObject.GetComponent<PlayerDistance>();
            playerDistance.IncreaseAmmo(m_value);
            Destroy(gameObject);
        }
    }
}
