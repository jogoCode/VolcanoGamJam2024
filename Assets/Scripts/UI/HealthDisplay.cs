using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] HealthSysteme m_healthSysteme;
    [SerializeField] Slider m_slider;


    public event Action OnHealthChanged;

    private void Start()
    {
        m_slider = GetComponent<Slider>();
        m_healthSysteme.OnHealthChanged += ChangedBarValue;
        m_slider.maxValue = m_healthSysteme.GetHealth();
        m_slider.value = m_healthSysteme.GetHealth();
    }

    public void ChangedBarValue()
    {
        m_slider.value = m_healthSysteme.GetHealth();
    }
}
