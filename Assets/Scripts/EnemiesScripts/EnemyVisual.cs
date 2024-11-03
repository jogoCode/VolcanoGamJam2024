using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyVisual : MonoBehaviour
{
    [SerializeField] public  Animator m_animator;
    [SerializeField] NavMeshAgent m_agent;

  
    private void Update()
    {
        m_animator.SetFloat("Movement",m_agent.speed);
    }

    public void OnAtkAnimation()
    {
        m_animator.SetTrigger("isAttack");
    }

}
