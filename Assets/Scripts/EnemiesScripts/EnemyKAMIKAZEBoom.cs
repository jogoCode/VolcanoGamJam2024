using UnityEngine;

public class EnemyKAMIKAZEBoom : MonoBehaviour
{
    [SerializeField] private int _damages;
    [SerializeField] private float _lifeTime;
    [SerializeField] private GameObject _boomEffect;

    private void Start()
    {
        Instantiate(_boomEffect,transform.position, Quaternion.identity);
        SoundManager.Instance.PlaySFX("EnemyExplosion");
    }
    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
        {
            
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            HealthSysteme playerHealth = other.gameObject.GetComponent<HealthSysteme>();
            playerHealth.TakeDamages(_damages);
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.ApplyImpulse(transform.forward, 80f); // impulse force;
            playerHealth.TakeDamages(_damages);
        }
    }
}
