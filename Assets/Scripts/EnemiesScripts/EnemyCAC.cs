using UnityEngine;
using UnityEngine.AI;

public class EnemyCAC : Enemy { 



    private void Update()
    {
        if(m_enemyStateManager.GetState()== EnemyStateManager.EnemyStates.ATK|| m_enemyStateManager.GetState() == EnemyStateManager.EnemyStates.POSTATK)
        {
            _agent.enabled = false;
        }
        else
        {
            _agent.enabled = true;
        }
        if (_agent.enabled == true)
        {

            _agent.SetDestination(_player.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            if (m_enemyStateManager.GetState() == EnemyStateManager.EnemyStates.ATK) return;
            Visual.m_animator.SetTrigger("isAttack");
            SoundManager.Instance.PlaySFX("CanneASucre");
        }
    }
}
