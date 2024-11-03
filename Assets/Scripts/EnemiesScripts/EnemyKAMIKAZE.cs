using UnityEngine;
using UnityEngine.AI;

public class EnemyKAMIKAZE : Enemy
{
    [SerializeField] private float _countDown;
    [SerializeField] private bool _touché;
    [SerializeField] private GameObject _explosion;

    private void Start()
    {
        base.Start();
        _touché = false;    
    }

    private void Update()
    {
        if (_touché == true)
        {
            _countDown -= Time.deltaTime;
        }
        base.Update();
  
        if (_countDown <= 0)
        {
            Instantiate(_explosion, transform.position + new Vector3(0,1,0), transform.rotation);
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _agent.speed = 0;  
            _touché=true;
        }
    }
}
