using System.Collections;
using UnityEngine;

public class EnemyRangeProjectil : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private int _damages;
    Rigidbody rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(LifeTime());
        Oscillator oscillator = GetComponent<Oscillator>();
        oscillator.StartOscillator(15);
        SoundManager.Instance.PlaySFX("LaunchSlurp");
    }
    void Update()
    {
        rb.velocity = transform.forward * _speed;
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            HealthSysteme playerHealth = other.gameObject.GetComponent<HealthSysteme>();
            playerHealth.TakeDamages(_damages);
            SoundManager.Instance.PlaySFX("Slurp");
            Destroy(gameObject);
        }
    }
}
