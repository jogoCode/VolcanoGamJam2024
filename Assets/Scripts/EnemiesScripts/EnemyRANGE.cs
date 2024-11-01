using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyRANGE : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _safeZone;
    [SerializeField] private bool _inSafeZone;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _shootPoint;
    [SerializeField] private bool _canShoot;
    [SerializeField] private float _shootCoolDown;

    private void Start()
    {
        _inSafeZone = false;
        _canShoot = true;
    }
    private void Update()
    {
        transform.LookAt(new Vector3(_player.transform.position.x,0,_player.transform.position.z));
        if(_inSafeZone == false)
        {
            _agent.SetDestination(_player.transform.position);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        _inSafeZone = true;
        if (other.gameObject == _safeZone)
        {
            var direction =  _player.transform.position - gameObject.transform.position ;
            
            _agent.SetDestination(transform.position - direction);
            StartCoroutine(Shoot());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _inSafeZone = false;
    }
    IEnumerator Shoot()
    {
        if(_canShoot == true)
        {
            Instantiate(_projectile,_shootPoint.transform.position,_shootPoint.transform.rotation);
            Debug.Log(transform.rotation);
            _canShoot = false;
            yield return new WaitForSeconds(_shootCoolDown);
            _canShoot = true;
        }
    }
}
