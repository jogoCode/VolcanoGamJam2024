using System.Collections;
using UnityEngine;

public class HealthSysteme : MonoBehaviour
{
    [SerializeField] private int _health;
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
            _health -= damages;
        }
    }
    IEnumerator InvincibleCoolDown()
    {
        _invincible = true ;
        yield return new WaitForSeconds(_invincibilityFrames);
        _invincible = false ;
    }
}
