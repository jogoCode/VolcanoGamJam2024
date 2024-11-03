using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject m_mainMenuPannel;
    //[SerializeField] PlayerController m_player;
    [SerializeField] GameObject m_videoScreen;
    [SerializeField] VideoPlayer m_videoPlayer;
    GameManager m_gameManager;
    
    void Start()
    {
        m_gameManager = GameManager.Instance;
        m_videoPlayer.loopPointReached += EndReached;
    
    }

    // Update is called once per frame
    void Update()
    {
        if(m_videoPlayer == null) return;   
        if(m_videoPlayer.isPlaying) {
            if (Input.GetButton("Jump"))
            {
                GameManager.Instance.LoadScene(1);
                m_videoPlayer.Stop();
                StopAllCoroutines();
            }
        
        }
    }


    public void Play()
    {
        StartCoroutine(Transition());
    }

    public void Quit()
    {
        Application.Quit();     
    }

    public void BackToMainMenu()
    {
        m_gameManager.LoadScene(0);
    }

    public void Restart()
    {
        m_gameManager.LoadScene(1);
    }

    IEnumerator Transition()
    {
        m_gameManager.m_sceneTransition.SetTrigger("End");
        yield return new WaitForSeconds(0.5f);
        m_gameManager.m_sceneTransition.SetTrigger("Start");
       
        m_videoScreen.SetActive(true);
        m_videoPlayer.Play();
    }


    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.playbackSpeed = vp.playbackSpeed / 10.0F;
        m_gameManager.LoadScene(1);
    }


}
