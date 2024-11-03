using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DardDisplay : MonoBehaviour
{
    [SerializeField] PlayerDistance m_playerDistance;
    [SerializeField] TextMeshProUGUI m_textMeshPro;
    void Start()
    {
        m_playerDistance = FindObjectOfType<PlayerDistance>();
        m_textMeshPro =  m_textMeshPro.GetComponentInChildren<TextMeshProUGUI>();
        m_playerDistance.OnAmmoChanged += UpdateDardCount;
    }

    void UpdateDardCount()
    {

        m_textMeshPro.text = m_playerDistance.Ammo.ToString();
    }
}
