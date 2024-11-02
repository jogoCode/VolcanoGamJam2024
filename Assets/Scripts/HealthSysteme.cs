using System.Collections;
using UnityEngine;

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
            FeedBackManager.Instance.FreezeFrame(0.05f, 0.5f);
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
    }

    public void CheckCanDie()
    {
        if(_health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
