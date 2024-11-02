using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{

    PlayerController m_playerController;
    Animator m_animator;
    Oscillator m_ocscillator;

    [SerializeField]GameObject m_model;
    Material m_material;
    [SerializeField] float m_rotationSpeed;

    public const float SPEED_ANIM_RATIO = 10;


   

   
    public event Action OnGrounded;


    Vector3 m_playerDir;
    Quaternion m_targetRotation;


    public Oscillator Oscillator
    {
        get { return m_ocscillator; }
    }

    #region BUILT-IN
    void Awake()
    {
        m_playerController = GetComponent<PlayerController>();  
        m_animator = GetComponentInChildren<Animator>();
        m_ocscillator = GetComponent<Oscillator>();
    }

    void Update()
    {
        VerticalMoveAnimation();
        RotateModel();    
    }
    #endregion

    void RotateModel()
    {
        if (m_playerController.GetPlayerStateManager().GetState() == PlayerStateManager.PlayerStates.ATK && m_playerController.ControllerMode.Contains("Mouse"))
        {
            m_model.transform.LookAt(new Vector3(m_playerController.RaycastFromMousePosition().x, 0, m_playerController.RaycastFromMousePosition().z));
            //TODO BUG QUANT ON PASSE MANNETTE A SOURIS
        }
        if (m_playerController.GetInputDir() != Vector2.zero)
        {
            m_playerDir = new Vector3(m_playerController.GetInputDir().x, 0, m_playerController.GetInputDir().y);
            m_targetRotation = Quaternion.LookRotation(m_playerDir);
            m_model.transform.rotation = Quaternion.Slerp(m_model.transform.rotation, m_targetRotation, m_rotationSpeed * Time.deltaTime);
        }
        else if(m_playerController.GetPlayerStateManager().GetState() == PlayerStateManager.PlayerStates.DISTANCE && m_playerController.ControllerMode.Contains("Mouse"))
        {
            //m_targetRotation = Quaternion.LookRotation(m_playerDir);
            m_model.transform.LookAt(new Vector3(m_playerController.RaycastFromMousePosition().x,0, m_playerController.RaycastFromMousePosition().z));
        }
  
       
       
    }


    #region Animation

    public void AttackAnimation()
    {
        if (m_playerController.GetPlayerStateManager().GetState() == PlayerStateManager.PlayerStates.DISTANCE) return;
        if (m_playerController.GetPlayerStateManager().GetState() == PlayerStateManager.PlayerStates.ATK) return;
        Oscillator.StartOscillator(5);
        m_animator.SetTrigger("isAttack");
    }


    public void MoveAnimation(float x,float speedPercent) // x = xvel for horizontalAnim . y = yVel for verticalAnim . speedPercent = Speed ratio
    {
        if (m_playerController.GetPlayerStateManager().GetState() == PlayerStateManager.PlayerStates.DISTANCE) return;
        float speed = speedPercent / SPEED_ANIM_RATIO;
        if(x == 0)
        {
            speed = 1;
        }
    
        m_animator.SetFloat("SpeedPercent", speed);
        m_animator.SetFloat("Movement", x);
    }


    public void OnDistanceAnimation()
    {

    }

    public void VerticalMoveAnimation()
    {
        PlayerMovement playerMovement = m_playerController.GetPlayerMovement();
       
    }

#endregion



    public void SetPlayerDir(Vector3 newDir)
    {
        m_playerDir = newDir;
    }

    #region
    public void JustGrounded()
    {
        //Oscillator.StartOscillator(1);
    }


    public void CheckGrounded(bool isGrounded)
    {
        if (m_animator != null)
        {
            
        }
    }

    public GameObject GetModel() => m_model;

    #endregion
}
