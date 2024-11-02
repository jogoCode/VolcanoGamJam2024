using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    int m_playerId;
    bool m_isReady = false;

    [SerializeField] PlayerMovement m_playerMovement;
    [SerializeField] PlayerVisual m_playerVisual;
    PlayerStateManager m_playerStateManager;
    PlayerDistance m_playerDistance;
    PlayerMelee m_playerMelee;


    Vector2 m_inputDir = Vector2.zero;
    Vector2 m_lastInputDir = Vector2.zero;
    bool m_jumped = false;


    Vector3 m_mousePos;
    string m_controllerMode;

    public event Action OnJustGrounded;
    public event Action OnJumped;
    public event Action OnAction;
    public event Action OnDistance;
    public event Action<float,float> OnMoved;
    public event Action OnDashed;
    public event Action<bool> OnReady;

    public event Action OnWeaponModeChanged;


    WeaponMode m_currentWeaponMode = WeaponMode.MELEE;
    public enum WeaponMode
    {
        MELEE,
        DISTANCE
    }


    public bool IsReady
    {
        get {return m_isReady;}
    }
    public int PlayerId
    {
        get { return m_playerId;}
    }

    public string ControllerMode
    {
        get { return m_controllerMode; }
    }

    public PlayerVisual PlayerVisual
    {
        get { return m_playerVisual;}
    }

    #region INPUT EVENTS
    public void OnInputMove(InputAction.CallbackContext context)
    {
        m_inputDir = context.ReadValue<Vector2>();
        OnMoved?.Invoke(m_inputDir.magnitude,m_playerMovement.Speed);
        if(m_inputDir != Vector2.zero)
        {
            m_lastInputDir = new Vector2(m_inputDir.x, m_inputDir.y).normalized;
        }
    }

    public void OnInputJump(InputAction.CallbackContext context)
    {
        if (context.action.triggered ){
            m_playerMovement.ResetJumpBufferTimer();    
        }
    }

    public void OnInputAction(InputAction.CallbackContext context)
    {
        var control = context.control;
        m_controllerMode = control.ToString();
        if (context.action.triggered){
            OnAction?.Invoke();
        }
    }

    public void OnInputDash(InputAction.CallbackContext context)
    {
        if (context.action.triggered){
            OnDashed?.Invoke();
        }
    }

    public void OnInputDistance(InputAction.CallbackContext context)
    {
        var control = context.control;
        m_controllerMode = control.ToString();
        if (context.action.IsPressed())
        {
            SetWeaponMode(WeaponMode.DISTANCE);
            DistanceMode();
            m_playerVisual.OnDistanceAnimation();
        }
        if (context.action.WasReleasedThisFrame())
        {
            m_playerVisual.OffDistanceAnimation();
            SetWeaponMode(WeaponMode.MELEE);
        }
    }

    public void OnInputReady(InputAction.CallbackContext context)
    {
        if (context.action.triggered){
            m_isReady = !m_isReady;
            OnReady?.Invoke(m_isReady);
        }
    }

    #endregion

    void Awake()
    {
        m_playerMovement = GetComponent<PlayerMovement>();
        m_playerVisual = GetComponent<PlayerVisual>();
        m_playerStateManager = GetComponentInChildren<PlayerStateManager>();
        m_playerDistance = GetComponentInChildren<PlayerDistance>();
        m_playerMelee = GetComponentInChildren<PlayerMelee>();


        OnWeaponModeChanged += WeaponModeChanged;
    
        OnDashed += m_playerMovement.Dash;
       

        OnJustGrounded += m_playerVisual.JustGrounded;
        OnMoved += m_playerVisual.MoveAnimation;
        OnAction += m_playerVisual.AttackAnimation;

        OnAction += m_playerDistance.CreateProjectile;
        OnAction += m_playerMelee.Attack;


    }

    public void Update()
    {
        m_mousePos = new Vector3(RaycastFromMousePosition().x, 0 , RaycastFromMousePosition().z).normalized;
    }

    public void Jump()
    {
        if (m_playerMovement.CoyoteTimer > 0)
        {
            m_playerMovement.Jump();
        }
    }

    public void SetLayer(int newLayer)
    {
        gameObject.layer = newLayer;
    }

    public void SetPlayerID(int newId)
    {
        m_playerId = newId;
    }

    Vector3 warpPosition = Vector3.zero;
    public void WarpToPosition(Vector3 newPosition)
    {
        m_playerMovement.GetCharacterController().enabled = false;
        warpPosition = newPosition;
        transform.position = warpPosition;
        m_playerMovement.GetCharacterController().enabled = true;
    }

    public void JustGrounded()
    {
        OnJustGrounded?.Invoke();
    }

    public Vector3 RaycastFromMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;     
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {  
           return hit.point;
        }
        return Vector3.zero;
    }


    void DistanceMode()
    {    
        GetPlayerVisual().SetPlayerDir(m_mousePos.normalized);

    }


    void WeaponModeChanged()
    {
        switch (m_currentWeaponMode)
        {
            case WeaponMode.MELEE:
                GetPlayerStateManager().SetState(PlayerStateManager.PlayerStates.IDLE);
                break;
            case WeaponMode.DISTANCE:
                GetPlayerStateManager().SetState(PlayerStateManager.PlayerStates.DISTANCE);
                break;
        }
    }

    public void SetWeaponMode(WeaponMode newMode)
    {
        m_currentWeaponMode = newMode;
        OnWeaponModeChanged?.Invoke();
    }

    public void ApplyImpulse(Vector3 direction,float impulseForce)
    {
        m_playerMovement.ApplyImpulse(direction, impulseForce);
    }

    #region ACCESORS
    public PlayerVisual GetPlayerVisual() => m_playerVisual;

    public PlayerMovement GetPlayerMovement() => m_playerMovement;

    public PlayerStateManager GetPlayerStateManager() => m_playerStateManager;

    public Vector2 GetInputDir()=> m_inputDir;

    public Vector2 GetLastInputDir() => m_lastInputDir;

    public float GetVerticalVelY() => m_playerMovement.GetVerticalVelY();

    public bool GetJumped() => m_jumped;

    public GameObject GetModel() => m_playerVisual.GetModel();
    #endregion

}
