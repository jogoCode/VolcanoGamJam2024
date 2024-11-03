using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyRANGE : Enemy
{
    [SerializeField] private GameObject _safeZone;
    [SerializeField] private bool _inSafeZone;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _shootPoint;
    [SerializeField] private bool _canShoot;
    [SerializeField] private float _shootCoolDown;

    private void Start()
    {
        base.Start();
        _safeZone = _player.GetComponentInChildren<SphereCollider>().gameObject;
        _inSafeZone = false;
        _canShoot = true;
    }
    private void Update()
    {
        transform.LookAt(new Vector3(_player.transform.position.x,0,_player.transform.position.z));
        if(_inSafeZone == false)
        {
            if (m_enemyStateManager.GetState() == EnemyStateManager.EnemyStates.ATK) { Debug.Log("zizi"); };
            if (!_agent.enabled) return;
            _agent.SetDestination(_player.transform.position);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        _inSafeZone = true;
        if (other.gameObject.GetComponent<PlayerProjectile>() != null) return;
        if (other.gameObject == _safeZone)
        {
            var direction =  _player.transform.position - gameObject.transform.position ;

            if (_agent.enabled == true)
            {
                _agent.SetDestination(transform.position - direction);
            }
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
            m_enemyVisual.OnAtkAnimation();
            Debug.Log(transform.rotation);
            _canShoot = false;
            yield return new WaitForSeconds(_shootCoolDown);
            _canShoot = true;
        }
    }


    public void CreateProjectile(){
        Instantiate(_projectile, _shootPoint.transform.position, _shootPoint.transform.rotation);
        SoundManager.Instance.PlaySFX("Slurp");
    }
}
