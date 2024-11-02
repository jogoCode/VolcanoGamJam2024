using UnityEngine;
using UnityEngine.AI;

public class EnemyKAMIKAZE : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _countDown;
    [SerializeField] private bool _touch�;
    [SerializeField] private GameObject _explosion;

    private void Start()
    {
        _touch� = false;
    }

    private void Update()
    {
        if (_touch� == true)
        {
            _countDown -= Time.deltaTime;
        }
        else
        {
            _agent.SetDestination(_player.transform.position);
        }

        if (_countDown <= 0)
        {
            Instantiate(_explosion, transform.position + new Vector3(0,1,0), transform.rotation);
            Destroy(gameObject);
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
