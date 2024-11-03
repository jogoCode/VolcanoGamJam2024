using UnityEngine;
using UnityEngine.AI;

public class EnemyKAMIKAZE : Enemy
{
    [SerializeField] private float _countDown;
    [SerializeField] private bool _touch�;
    [SerializeField] private GameObject _explosion;

    private void Start()
    {
        base.Start();
        _touch� = false;    
    }

    new private void Update()
    {
        if (_touch� == true)
        {
            Rigidbody rb  = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            _countDown -= Time.deltaTime;
            _agent.enabled = false;     
        }
        else
        {
            if (_agent.enabled == true)
            {
                _agent.SetDestination(_player.transform.position);
            }
        }
  
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
            _touch�=true;
        }
    }
}
