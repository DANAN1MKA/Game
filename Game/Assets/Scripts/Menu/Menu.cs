using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void ButtonPlayDown()
    {
        SceneManager.LoadScene("Level_2_rand", LoadSceneMode.Single);
    }

    public void ButtonExitDown()
    {
        Application.Quit();
    }

    public void ButtonMenuDown()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
