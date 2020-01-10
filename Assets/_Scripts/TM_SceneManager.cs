using UnityEngine;
using UnityEngine.SceneManagement;

public class TM_SceneManager : MonoBehaviour
{

    void Update()
    {
        //if Escape is pressed, the game Quits
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    /// <summary>
    /// Load scene zero
    /// </summary>
    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Load scene based on number
    /// </summary>
    /// <param name="num">Number of scene to load</param>
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
