using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharChange : MonoBehaviour
{
    public static string player1Sprite = "blue";
    public static string player2Sprite = "red";

    // Update is called once per frame
    public void SetBluePlayerOne()
    {
        player1Sprite = "blue";
    }

    public void SetRedPlayerOne()
    {
        player1Sprite = "red";
    }

    public void SetGreenPlayerOne()
    {
        player1Sprite = "green";
    }

    public void SetYellowPlayerOne()
    {
        player1Sprite = "yellow";
    }

    public void SetPinkPlayerOne()
    {
        player1Sprite = "pink";
    }

    public void SetBluePlayerTwo()
    {
        player2Sprite = "blue";
    }

    public void SetRedPlayerTwo()
    {
        player2Sprite = "red";
    }

    public void SetGreenPlayerTwo()
    {
        player2Sprite = "green";
    }

    public void SetYellowPlayerTwo()
    {
        player2Sprite = "yellow";
    }

    public void SetPinkPlayerTwo()
    {
        player2Sprite = "pink";
    }
}
