using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TM_SceneManager : MonoBehaviour
{

    void Update()
    {
        //Quit game
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadSceneInt(int num)
    {
        SceneManager.LoadScene(num);
    }

    public void LoadNextScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex + 1);
    }
}
