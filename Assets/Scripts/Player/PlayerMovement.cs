using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;


[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    PlayerController m_playerController;
    CharacterController m_characterController;
    

    [SerializeField] float m_jumpForce = 10;
    [SerializeField] float m_gravity = 9.81f;
    [SerializeField] float m_speed = 4;
    [SerializeField] float m_coyoteTime = 0.1f;
    [SerializeField] float m_jumpBufferTime = 0.1f;


    [SerializeField] float m_impulseFriction = 2;
    [SerializeField] float m_impulseForce= 15;
    [SerializeField] Vector3 m_velocity;

    [SerializeField] float m_dashSpeed = 20;
    [SerializeField] float m_dashDuration = 0.2f;
    [SerializeField] float m_dashCooldown = 1f;

    float m_dashTimeRemaining;
    float m_dashCooldownRemaining;
    bool m_isDashing;
    bool m_isHit;
    [SerializeField] float m_hitSpeed = 200;
    Vector3 m_dashDir;

    float m_coyoteTimer;
    float m_jumpBufferTimer = 0;


    Vector3 m_vVel;


    [SerializeField] float m_vSpeed = 0;
    [SerializeField] float m_vVelFactor = 4f;


    bool m_canJump = true;
    bool m_wasGrounded;

    public event Action JustGrounded;

  

    public float CoyoteTimer
    {
        get { return m_coyoteTimer; }
    }

    public float Speed
    {
        get { return m_speed; } 
    }


    #region BUILT-IN
    void Awake()
    {
        m_playerController = GetComponent<PlayerController>();
        m_characterController = GetComponent<CharacterController>();
    }


    void Update()
    {

        PlayerController pc = m_playerController;
        HandleDash();

        Vector2 inputDir = m_playerController.GetInputDir();
        bool jumped = m_playerController.GetJumped();
        Vector3 dir = new Vector3(inputDir.x,m_vVel.y, inputDir.y);
        ImpulseHandler();
        bool isGrounded =  m_characterController.isGrounded;
        pc.PlayerVisual.CheckGrounded(m_characterController.isGrounded);
        if (m_playerController.GetPlayerStateManager().GetState() == PlayerStateManager.PlayerStates.DISTANCE) return;
        if (m_playerController.GetPlayerStateManager().GetState() == PlayerStateManager.PlayerStates.ATK) return;
        if (m_playerController.GetPlayerStateManager().GetState() == PlayerStateManager.PlayerStates.POSTATK) return;
        if (!m_wasGrounded && isGrounded) {
            m_vVel.y = -1;
            pc.JustGrounded();         

        }
        m_wasGrounded = isGrounded;


        if (!m_characterController.isGrounded){      
            gravity();
            m_coyoteTimer -= Time.deltaTime;
        }else{   
            
            m_vSpeed = 0f;
            ResetCoyoteTimer();
        }

        HandleJumpBuffer();
        Movement(dir, m_speed);       
    }

    #endregion


    void ImpulseHandler() {

        m_velocity.y = 0;
        // Appliquer le mouvement en fonction de la vitesse
        m_characterController.Move(m_velocity * Time.deltaTime);

        // Réduire progressivement la vitesse horizontale avec la "décélération"
        m_velocity.x = Mathf.Lerp(m_velocity.x, 0, m_impulseFriction * Time.deltaTime);
        
        m_velocity.z = Mathf.Lerp(m_velocity.z, 0, m_impulseFriction * Time.deltaTime);

    }


    public void ApplyImpulse(Vector3 direction)
    {
        // Ajouter l'impulsion dans la direction donnée
        Debug.Log("oko "+direction);
        m_velocity = direction.normalized * m_impulseForce;
    }

    void gravity()
    {
        m_vSpeed += m_vVelFactor * Time.deltaTime;
        m_vVel += Vector3.down * m_vSpeed * m_gravity * Time.deltaTime;
        m_characterController.Move(m_vVel*Time.deltaTime);
    }

    void Movement(Vector3 direction, float speed)
    {
        m_characterController.Move(direction*speed*Time.deltaTime);
    }


    public void HandleDash()
    {
        if (m_dashTimeRemaining > 0)
        {
            m_dashTimeRemaining -= Time.deltaTime;
            if (m_dashTimeRemaining <= 0)
            {
                m_isDashing = false;
                m_isHit = false ;
            }
        }

        if (m_dashCooldownRemaining > 0)
        {
            m_dashCooldownRemaining -= Time.deltaTime;
        }

        if(m_isDashing)
        {    
            m_characterController.Move(m_dashDir * m_dashSpeed * Time.deltaTime);
        }

    }

    public void Dash()
    {
        Vector2 inputDir = m_playerController.GetLastInputDir();
        Vector3 dir = new Vector3(inputDir.x, /*m_vVel.y*/0, inputDir.y);
        m_dashDir = dir;
 
        if (m_dashCooldownRemaining <= 0)
        {
            m_playerController.GetPlayerVisual().Oscillator.StartOscillator(10);
            m_isDashing = true;
            m_dashTimeRemaining = m_dashDuration;
            m_dashCooldownRemaining = m_dashCooldown;
        }
    }

  

    public void HandleJumpBuffer()
    {
        if(m_jumpBufferTimer > 0 && m_coyoteTimer> 0)
        {
            Jump();
            
        }
        if (m_jumpBufferTimer >0)
        {
            m_jumpBufferTimer -= Time.deltaTime;
        }
    }

    public void Jump()
    {
        m_jumpBufferTimer = 0;
        ResetCoyoteTimer();
        m_vSpeed = 0;
        m_vVel.y = 0;
        m_vVel.y += m_jumpForce;
        m_playerController.JustGrounded();
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body != null && !body.isKinematic)
            body.velocity += (m_characterController.velocity*Time.deltaTime/body.mass);
    }

    public bool IsGrounded()
    {
        return m_characterController.isGrounded;
    }

    public void ResetJumpBufferTimer()
    {
        m_jumpBufferTimer = m_jumpBufferTime;
    }

    public void ResetCoyoteTimer()
    {
        m_coyoteTimer = m_coyoteTime;
    }

    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject != gameObject) //ON hit
    //    {
    //        Quaternion otherRot = other.transform.rotation;
    //        Vector3 dir = otherRot * Vector3.forward;
    //        Debug.Log(dir);
    //        Hit(dir);
    //    }
    //}


    #region Get Variables
    public CharacterController GetCharacterController() => m_characterController;

    public float GetVerticalVelY() => m_vVel.y;
    #endregion




}
