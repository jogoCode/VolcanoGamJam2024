using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    EnemyRANGE m_enemyRange;
    public void Start()
    {
        m_enemyRange = GetComponentInParent<EnemyRANGE>();
    }

    public void CreateProjectile()
    {
        m_enemyRange.CreateProjectile();
    }

    public void PlaySound(string sound)
    {
        SoundManager.Instance.PlaySFX(sound);
    }
}
