using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Controller : MonoBehaviour
{
    public void ToGame()
    {
        SceneManager.LoadScene("MapCreation");
    }

    public void ToIntro()
    {
        SceneManager.LoadScene("StoryIntro");
    }

    public void ToEndGame()
    {
        SceneManager.LoadScene("EndGame");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
