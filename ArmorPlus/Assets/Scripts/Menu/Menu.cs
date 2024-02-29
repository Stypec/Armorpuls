using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Odist()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene("");
    }

    public void Fullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
