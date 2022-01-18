using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGestion : MonoBehaviour
{

    public void PlayGameKMS()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void PlayGameVirus()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        print("Unity, please Quit!!!");
        Application.Quit();
    }
}
