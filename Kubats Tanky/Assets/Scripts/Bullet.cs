using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public float speed = 20f; //bullet speed
    public Rigidbody2D rb;  //rigid body of bullet
    public GameObject fire; //fire effect
    public GameObject impact; //impact effect
    public int damage; //damage in impact
    

    public void Start()
    {
        rb.velocity = transform.right * speed; //add velocity to bullet
        Instantiate(fire, transform.position, transform.rotation); //spawn fire effect
        
    }

    //on collision
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>(); //import enemy script

        //if its enemy, take damage
        if (enemy != null)
        {
            enemy.TakeDamage(damage);   //take damage


           

            
            
                if (GameObject.Equals(GameRun.currentPlayerStatic, GameRun.player1Static))
                {
                    GameRun.player2Points++;
                }
                if (GameObject.Equals(GameRun.currentPlayerStatic, GameRun.player2Static))
                {
                    GameRun.player1Points++;
                }
            

        }

        

        Instantiate(impact, transform.position, transform.rotation);    //spawn impact effect
        Destroy(gameObject);    //destroy bullet
    }
}
