using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class HealthSysteme : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _invincibilityFrames;
    [SerializeField] private bool _invincible;



    private void Start()
    {
        _invincible = false;
    }
    public void TakeDamages(int damages)
    {
        if(_invincible == false)
        {
            StartCoroutine(InvincibleCoolDown());
            Oscillator oscillator = gameObject.GetComponent<Oscillator>();
            if (oscillator == null) return;
            //TODO Ajouter un KNOCK BACK
            oscillator.StartOscillator(5);
            //FeedBackManager.Instance.FreezeFrame(0.007f, 0.001f);
            FeedBackManager.Instance.InstantiateParticle(FeedBackManager.Instance.m_impactVfx,transform.position,transform.rotation);
            _health -= damages;
            _health = Mathf.Clamp(_health,0,_maxHealth);
            CheckCanDie();
        }
    }
    IEnumerator InvincibleCoolDown()
    {
        _invincible = true ;
        yield return new WaitForSeconds(_invincibilityFrames);
        _invincible = false ;
        EnableNavMeshAgent();
    }

    public void EnableNavMeshAgent()
    {
        NavMeshAgent navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        if (navMeshAgent == null) return;
        rigidbody.isKinematic = true;
        navMeshAgent.enabled = true;
        rigidbody.velocity = Vector3.zero;
       
    }

    public void CheckCanDie()
    {
        if(_health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
