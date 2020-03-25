using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public GameObject normalBullet;
    public GameObject heavyBullet;
    public GameObject currentBullet;

    //Ammo script, it changes ammo
    public void SetNormalBullet()
    {
        currentBullet = normalBullet;
        GetComponent<GameRun>().currentBullet = currentBullet;
    }

    public void SetHeavyBullet()
    {
        currentBullet = heavyBullet;
        GetComponent<GameRun>().currentBullet = currentBullet;
    }
}
