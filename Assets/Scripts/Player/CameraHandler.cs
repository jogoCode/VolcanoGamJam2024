using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        m_pos.x = Mathf.Lerp(m_pos.x, targetPos.x,m_cameraSpeed*Time.deltaTime);
        m_pos.z = Mathf.Lerp(m_pos.z, targetPos.z,m_cameraSpeed*Time.deltaTime);
        transform.position = targetPos + m_offset;
    }
}
