using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent _agent;
    [SerializeField] protected GameObject _player;
    [SerializeField] protected int _damages;
    [SerializeField] public EnemyStateManager m_enemyStateManager;
    [SerializeField] protected EnemyVisual m_enemyVisual;


    public EnemyVisual Visual
    {
        get { return m_enemyVisual; }
    }
    public int DAMAGE
    {
        get { return _damages; }
    }

    virtual protected void Start()
    {
        m_enemyStateManager = GetComponentInChildren<EnemyStateManager>();
        m_enemyVisual = GetComponent<EnemyVisual>();
        _player = FindObjectOfType<PlayerController>().gameObject;
    }

    protected void Update()
    {
       
        if (_agent.enabled == true)
        {
            
            _agent.SetDestination(_player.transform.position);
        }
    }

    private void OnTriggerStay(Collider other)
    {
    
        if (other.tag == "Player")
        {
            
            return;
            HealthSysteme playerHealth = other.gameObject.GetComponent<HealthSysteme>();
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.ApplyImpulse(transform.forward, 15f);
            playerHealth.TakeDamages(_damages);


        }
    }

}
