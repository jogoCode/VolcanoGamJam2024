using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] float m_cameraSpeed = 5;
    [SerializeField] Vector3 m_offset;
    [SerializeField] GameObject m_target;
    Vector3 m_pos;
    void Start()
    {
      
    }

    void Update()
    {
        Vector3 targetPos = m_target.transform.position;     
        
        m_pos = Vector3.Lerp(transform.position, targetPos + m_offset, m_cameraSpeed * Time.deltaTime);
        transform.position = m_pos;
    }
}
