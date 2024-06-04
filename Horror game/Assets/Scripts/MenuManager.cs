using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject QuitButton;
    void Start()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            // Disable the game object
            QuitButton.SetActive(false);
        }
    }
    public void PlayGame()
    {
        //Application.LoadedLevel("SampleScene");
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
