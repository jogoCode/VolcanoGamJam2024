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
            Destroy(gameObject);
        }
    }
}
