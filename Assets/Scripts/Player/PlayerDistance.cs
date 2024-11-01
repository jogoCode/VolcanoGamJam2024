using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistance : MonoBehaviour
{

    [SerializeField] GameObject m_bulletOrigin;
    [SerializeField] PlayerProjectile m_bullet;

    public event Action OnProjectile;


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
        if(m_playerController.GetPlayerStateManager().GetState() == PlayerStateManager.PlayerStates.ATK) { return; }
        PlayerProjectile playerProjectile = Instantiate(m_bullet,m_bulletOrigin.transform.position,m_playerController.GetModel().transform.rotation);
    }
    


}
