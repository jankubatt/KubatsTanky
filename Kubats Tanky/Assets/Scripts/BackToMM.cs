using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMM : MonoBehaviour
{
    //just script for going back to Main Menu
    public void GoBack()
    {
        SceneManager.LoadScene(0);
    }
}
