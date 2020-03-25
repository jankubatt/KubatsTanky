using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;    //health of enemy
    public GameObject deathEffect;  //death particle
   


    //function for taking damage
    public void TakeDamage(int damage)
    {
        health -= damage;   //health is health minus damage

        //if health is bellow or equal to 0, player dies
        if (health <= 0)
        {
            Die();
        }
    }

    //Die function
    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);  //create death effect
        Destroy(gameObject);    //destroy current game object
    }
}
