using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapsMenu : MonoBehaviour
{
    //Script for maps choosing menu
    public void PlayPlains()
    {
        GameRun.player1Points = 0;
        GameRun.player2Points = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayHills()
    {
        GameRun.player1Points = 0;
        GameRun.player2Points = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void PlayDesert()
    {
        GameRun.player1Points = 0;
        GameRun.player2Points = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }
}
