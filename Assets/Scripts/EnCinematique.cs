using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EnCinematique : MonoBehaviour
{
    [SerializeField] GameObject m_videoScreen;
    [SerializeField] VideoPlayer m_videoPlayer;
    GameManager m_gameManager;
    void Start()
    {
        m_gameManager = GameManager.Instance;
        m_videoPlayer.loopPointReached += EndReached;

    }
    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.playbackSpeed = vp.playbackSpeed / 10.0F;
        m_gameManager.LoadScene(0);
    }

}
