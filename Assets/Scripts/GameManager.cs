using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public Animator m_sceneTransition;
    void Awake()
    {
       
        if (Instance != null)
        {
            Debug.LogError("Plus d'une instance game manager dans la scene");
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
        m_sceneTransition.SetTrigger("Start");
        yield return new WaitForSeconds(0.5f);
        m_sceneTransition.SetTrigger("End");
        SceneManager.LoadScene(scene);
    }
}
