using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class HealthSysteme : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _invincibilityFrames;
    [SerializeField] private bool _invincible;


    public event Action OnHealthChanged;
    private void Start()
    {
        _invincible = false;
    }


    public void IncreaseHealth(int amount)
    {
        _health += amount;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        OnHealthChanged?.Invoke();
    }
    public void TakeDamages(int damages)
    {
        if(_invincible == false)
        {
            StartCoroutine(InvincibleCoolDown());
            SoundManager.Instance.PlaySFX("Impact");
            Oscillator oscillator = gameObject.GetComponent<Oscillator>();
            if (oscillator != null)
            {
                //TODO Ajouter un KNOCK BACK
                oscillator.StartOscillator(5);
            }
            //FeedBackManager.Instance.FreezeFrame(0.007f, 0.001f);
            FeedBackManager.Instance.InstantiateParticle(FeedBackManager.Instance.m_impactVfx,new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z),transform.rotation);
            _health -= damages;
            OnHealthChanged?.Invoke();
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
            FeedBackManager.Instance.InstantiateParticle(FeedBackManager.Instance.m_diedSmoke,transform.position,transform.rotation);
            if(gameObject.GetComponent<PlayerController>() != null)
            {
                GameManager gm = GameManager.Instance;
                gm.LoadScene(2);
            }
        }
    }


    public int GetHealth() => _health;
}
