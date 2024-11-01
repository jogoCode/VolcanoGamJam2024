using System.Collections;
using UnityEngine;

public class EnemyRangeProjectil : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
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
}
