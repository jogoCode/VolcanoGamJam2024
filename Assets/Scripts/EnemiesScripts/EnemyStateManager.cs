using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStateManager;

public class EnemyStateManager : MonoBehaviour
{

    EnemyStates m_actualState = EnemyStates.IDLE;
    public enum EnemyStates
    {
        IDLE,
        MOVE,
        JUMP,
        FALL,
        ATK,
        DISTANCE,
        POSTATK
    }
  
    void Update()
    {
        
    }
    public void SetState(EnemyStates newState)
    {
        m_actualState = newState;
    }
    public EnemyStates GetState() => m_actualState;
}
