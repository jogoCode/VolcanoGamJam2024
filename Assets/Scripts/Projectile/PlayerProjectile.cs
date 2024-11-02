using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{

    Rigidbody m_rb;
    [SerializeField] float m_projSpeed;


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

 
}
