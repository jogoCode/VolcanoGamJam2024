using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHeard : MonoBehaviour
{

    [SerializeField] int m_value = 2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HealthSysteme playerDistance = other.gameObject.GetComponent<HealthSysteme>();
            SoundManager.Instance.PlaySFX("Coeur+");
            playerDistance.IncreaseHealth(m_value);
            Destroy(gameObject);
        }
    }
}
