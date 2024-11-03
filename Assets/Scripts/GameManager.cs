using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public Animator m_sceneTransition;
    GameObject m_videoDeFin;
    void Awake()
    {
       
        if (Instance != null)
        {
            Debug.LogWarning("Plus d'une instance game manager dans la scene");
            Destroy(gameObject);
            return;
        }
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
      

    }


    public void LoadScene(int scene)
    {
        StartCoroutine(LoadSceneCoroutine(scene));
    }


    IEnumerator LoadSceneCoroutine(int scene)
    {
        m_sceneTransition.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        m_sceneTransition.SetTrigger("Start");
        SceneManager.LoadScene(scene);
    }
}
