using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class CurrentName{
    public static string typePlayer;
}
public class MenuGestion : MonoBehaviour
{

    public void PlayGameKMS()
    {
        CurrentName.typePlayer = "KMS";
        SceneManager.LoadScene("GameScene");
    }
    public void PlayGameVirus()
    {
        CurrentName.typePlayer = "Virus";
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        print("Unity, please Quit!!!");
        Application.Quit();
    }
}
