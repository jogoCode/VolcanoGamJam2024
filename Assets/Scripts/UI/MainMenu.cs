using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject m_mainMenuPannel;
    [SerializeField] PlayerController m_player;
    void Start()
    {
        PlayerInput playerInput = m_player.gameObject.GetComponent<PlayerInput>();
        playerInput.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Play()
    {
        PlayerInput playerInput = m_player.gameObject.GetComponent<PlayerInput>();
        playerInput.enabled = true;
        m_mainMenuPannel.SetActive(false);
    }
}
