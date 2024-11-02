using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{

    Rigidbody m_rb;
    [SerializeField] float m_projSpeed;
    [SerializeField] int m_projDamage;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    public void Start()
    {
        AddForce();
        Debug.Log("lets goooo!!");
        Destroy(gameObject, 2);
    }

    public void AddForce()
    {
        m_rb.AddForce(transform.forward*m_projSpeed,ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            HealthSysteme healthSysteme = other.gameObject.GetComponent<HealthSysteme>();
            Collider collider = gameObject.GetComponentInChildren<Collider>();
            healthSysteme.TakeDamages(m_projDamage);
            Oscillator oscillator = other.gameObject.GetComponent<Oscillator>();
            if (oscillator == null) return;
            //TODO Ajouter un KNOCK BACK
            oscillator.StartOscillator(15);
            m_rb.velocity = Vector3.zero;
            m_rb.isKinematic = true;

            gameObject.layer = 5;
            transform.SetParent(other.transform);
            collider.enabled = false;
        }
    }

}
