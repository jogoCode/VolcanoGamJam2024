using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyVisual : MonoBehaviour
{
    [SerializeField] Animator m_animator;
    [SerializeField] NavMeshAgent m_agent;


    private void Update()
    {
        m_animator.SetFloat("Movement",m_agent.speed);
    }
}
