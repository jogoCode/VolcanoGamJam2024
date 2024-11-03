using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistance : MonoBehaviour
{

    [SerializeField] GameObject m_bulletOrigin;
    [SerializeField] PlayerProjectile m_bullet;
    [SerializeField] float m_shootCoolDown = 0.5f;

    [SerializeField] int m_maxAmmo;
    [SerializeField] int m_ammo;
    bool m_canShoot = true;

    public event Action OnProjectile;


    Coroutine m_shootCoroutine;

    [SerializeField] PlayerController m_playerController;
   

    void Start()
    {
        m_playerController = GetComponent<PlayerController>();
        OnProjectile += CreateProjectile;
    }

    void Update()
    {
        
    }


    public void CreateProjectile()
    {
        if (m_ammo <= 0) return; 
        if(m_playerController.GetPlayerStateManager().GetState() == PlayerStateManager.PlayerStates.ATK) { return; }
        if (m_playerController.GetPlayerStateManager().GetState() != PlayerStateManager.PlayerStates.DISTANCE) { return; }
        if (!m_canShoot) { return; }
        m_ammo--;

        m_ammo = Mathf.Clamp(m_ammo,0,m_maxAmmo);
        m_playerController.GetPlayerVisual().Oscillator.StartOscillator(3);
        SoundManager.Instance.PlaySFX("Sarbacane");
        PlayerProjectile playerProjectile = Instantiate(m_bullet,m_bulletOrigin.transform.position,m_playerController.GetModel().transform.rotation);
        m_shootCoroutine = StartCoroutine(ShootCoolDown());
    }

    

    IEnumerator ShootCoolDown()
    {
        m_canShoot = false;
        yield return new WaitForSeconds(m_shootCoolDown);
        m_canShoot = true;  
    }
    


}
