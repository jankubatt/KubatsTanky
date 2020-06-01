using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameRun : MonoBehaviour
{
    private bool canFire = true;        //if tank can fire, that means if bullet exist, nobody can fire another bullet
    private bool canUseHeavy = false;
    private bool canUseKorona = false;
    private Vector3 cameraOffset;       //camera offset
    public GameObject playerCamera;     //player camera
    public GameObject mapCamera;        //camera for the whole map
    public GameObject bulletCamera;        //camera for the whole map
    public GameObject player1;          //player 1
    public GameObject player2;          //player 2
    public SpriteRenderer player1Render;
    public SpriteRenderer player2Render;
    public Sprite tankBlue;
    public Sprite tankRed;
    public Sprite tankGreen;
    public Sprite tankYellow;
    public Sprite tankPink;
    public GameObject currentPlayer;    //current player
    public static GameObject player1Static;          //player 1
    public static GameObject player2Static;          //player 2
    public static GameObject currentPlayerStatic;    //current player
    public GameObject prevPlayer;       //prev player
    public GameObject canonPlayer1;     //canon of player 1
    public GameObject canonPlayer2;     //canon of player 2
    public GameObject currentCanon;     //current canon
    public GameObject prevCanon;        //prev canon
    public Transform firePointPlayer1;  //fire point for canon of player 1
    public Transform firePointPlayer2;  //fire point for canon of player 2
    public Transform currentFirePoint;  //current fire point
    public Transform prevFirePoint;     //prev fire point
    public GameObject normalBullet;     //normal bullet
    public GameObject heavyBullet;      //heavy bullet
    public GameObject koronaBullet;      //heavy bullet
    public GameObject currentBulletP1;  //current bullet of player 1
    public GameObject currentBulletP2;  //current bullet of player 2
    public GameObject currentBullet;    //type of bullet, that is currently used
    public GameObject uiPanel;          //ui panel is panel with almost all ui elements
    public GameObject btnBullet;        //button for normal bullet
    public GameObject btnHeavyBullet;   //button for heavy bullet
    public GameObject btnKoronaBullet;   //button for heavy bullet
    public GameObject pointsPanel;
    public GameObject camBorder;
    public Slider strength;             //slider for strength of tanks
    public Slider healthP1Slider;       //bar for health of player 1
    public Slider healthP2Slider;       //bar for health of player 2
    public Text pointsText;
    public Slider timeleftSlider;
    public float tankSpeed = 4f;        //speed of tanks
    public float bulletSpeed = 20;      //speed of bullets
    public int tiltConst = 0;           //tilt constant of canon, if side changes, we need to increase this by 180
    public int prevTiltConst = 180;     //prev tilt constant
    public int healthP1 = 100;          //health of player 1
    public int healthP2 = 100;          //health of player 2
    public static int player1Points;
    public static int player2Points;
    public static int currentPlayerPoints;
    public int pointp1;
    public int pointp2;
    float timeleft = 15f;


    void Start()
    {
        cameraOffset = playerCamera.transform.position - currentPlayer.transform.position;  //camera offset calculation
        uiPanel.SetActive(false);   //hide panel on start
        bulletCamera.SetActive(false);
        camBorder.SetActive(false);
        strength.value = 20;    //set strength to 20
        healthP1 = 100; //set 100 health
        healthP2 = 100; //set 100 health
        ChangeSprite();
    }

    void Update()
    {
        //if one player is missing, end the game
        if (!GameObject.Find("player1") || !GameObject.Find("player2"))
        {
            SceneManager.LoadScene(0);
        }

        timeleft -= Time.deltaTime;
        timeleftSlider.value = timeleft;

        if (timeleft < 0 && playerCamera.activeInHierarchy == true)
        {
            ChangePlayer();
        }

        if (mapCamera.activeInHierarchy == true)
        {
            timeleft = 15f;
        }

        bulletSpeed = strength.value;   //bullet speed is equal to slider value
        healthP1 = GameObject.Find("player1").GetComponent<Enemy>().health; //I am calculating demage in different script, so health is equal to health in enemy script
        healthP2 = GameObject.Find("player2").GetComponent<Enemy>().health; /*-//-*/
        healthP1Slider.value = healthP1; //health bar is equal to health of player
        healthP2Slider.value = healthP2; /*-//-*/
        player1Static = player1;
        player2Static = player2;
        currentPlayerStatic = currentPlayer;
        pointp1 = player1Points;
        pointp2 = player2Points;

        //decides which player is choosing bullet
        if (GameObject.Equals(currentPlayer, player1))
        {
            currentBullet = currentBulletP1;
            pointsText.text = player1Points.ToString() + " coins";
            currentPlayerPoints = player1Points;
        }

        if (GameObject.Equals(currentPlayer, player2))
        {
            currentBullet = currentBulletP2;
            pointsText.text = player2Points.ToString() + " coins";
            currentPlayerPoints = player2Points;
        }

        if (currentPlayerPoints >= 2)
        {
            canUseHeavy = true;
        }
        else
        {
            canUseHeavy = false;
        }

        if (currentPlayerPoints >= 4)
        {
            canUseKorona = true;
        }
        else
        {
            canUseKorona = false;
        }

        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        if (playerCamera.transform.position != currentPlayer.transform.position + cameraOffset)
        {
            playerCamera.transform.position = currentPlayer.transform.position + cameraOffset;
        }
        

        //movement

        if (Vector3.Dot(player1.transform.up, Vector3.down) > 0.05)
        {
            SceneManager.LoadScene(0);
        }
        if (Vector3.Dot(player2.transform.up, Vector3.down) > 0.05)
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKey(KeyCode.A))    //left
            {
                currentPlayer.transform.position += Vector3.left * tankSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))    //right
            {
                currentPlayer.transform.position += Vector3.right * tankSpeed * Time.deltaTime;
            }
        
        

        //canon tilt
        Vector3 mouseScreen = Input.mousePosition;
        Vector3 mouse = Camera.main.ScreenToWorldPoint(mouseScreen);
        currentCanon.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mouse.y - currentCanon.transform.position.y, mouse.x - currentCanon.transform.position.x) * Mathf.Rad2Deg - tiltConst);

        //if I is pressed show or hide ui panel
        if (Input.GetKeyDown(KeyCode.I))
        {
            uiPanel.SetActive(!uiPanel.activeSelf);

            if (canUseHeavy == false)
            {
                btnHeavyBullet.SetActive(false);
            }

            if (canUseHeavy == true)
            {
                btnHeavyBullet.SetActive(true);
            }

            if (canUseKorona == false)
            {
                btnKoronaBullet.SetActive(false);
            }

            if (canUseKorona == true)
            {
                btnKoronaBullet.SetActive(true);
            }
        }

        //if ui panel is not active and canFire is true, player can fire
        if (uiPanel.activeSelf == false && canFire == true)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Instantiate(currentBullet, currentFirePoint.position, currentFirePoint.rotation);   //create bullet
                playerCamera.SetActive(false);  //camera for player is disabled so the only camera active is map camera
                mapCamera.SetActive(true);  //map camera enable
                canFire = false;    //cant fire if fired once
                ChangePlayer(); //change player function
            }
        }

        //if bullet exists, player can not fire
        if (GameObject.Find("bullet(Clone)"))
        {
            GameObject.Find("bullet(Clone)").GetComponent<Bullet>().speed = bulletSpeed; //get the speed of bullet from another script
            playerCamera.SetActive(false);  //disable player camera
            mapCamera.SetActive(true);  //enable map camera
            bulletCamera.SetActive(true);
            camBorder.SetActive(true);
            pointsPanel.SetActive(false);
            canFire = false;    //can not fire
            Vector3 bulletCamPos = new Vector3(GameObject.Find("bullet(Clone)").transform.position.x, GameObject.Find("bullet(Clone)").transform.position.y, -30);
            bulletCamera.transform.position = bulletCamPos;
        }
        else if (GameObject.Find("bulletHeavy(Clone)")) //same as normal bullet
        {
            GameObject.Find("bulletHeavy(Clone)").GetComponent<Bullet>().speed = bulletSpeed;
            playerCamera.SetActive(false);
            mapCamera.SetActive(true);
            bulletCamera.SetActive(true);
            camBorder.SetActive(true);
            canFire = false;
            pointsPanel.SetActive(false);
            Vector3 bulletCamPos = new Vector3(GameObject.Find("bulletHeavy(Clone)").transform.position.x, GameObject.Find("bulletHeavy(Clone)").transform.position.y, -30);
            bulletCamera.transform.position = bulletCamPos;
        }
        else if (GameObject.Find("bulletKorona(Clone)")) //same as normal bullet
        {
            GameObject.Find("bulletKorona(Clone)").GetComponent<Bullet>().speed = bulletSpeed;
            playerCamera.SetActive(false);
            mapCamera.SetActive(true);
            bulletCamera.SetActive(true);
            camBorder.SetActive(true);
            canFire = false;
            pointsPanel.SetActive(false);
            Vector3 bulletCamPos = new Vector3(GameObject.Find("bulletKorona(Clone)").transform.position.x, GameObject.Find("bulletKorona(Clone)").transform.position.y, -30);
            bulletCamera.transform.position = bulletCamPos;
        }
        else //if none of the bullets exist, player can fire and player camera is activated, map camera deactivated
        {
            playerCamera.SetActive(true);
            mapCamera.SetActive(false);
            canFire = true;
            pointsPanel.SetActive(true);
            if (bulletCamera.activeSelf == true)
            {
                Invoke("SetBulletCamFalse", 0.25f);
            }
            
        }
    }

    public void SetBulletCamFalse()
    {
        bulletCamera.SetActive(false);
        camBorder.SetActive(false);
    }

    public void ChangeSprite()
    {
        if (CharChange.player1Sprite == "blue")
        {
            player1Render.sprite = tankBlue;
        }

        if (CharChange.player1Sprite == "red")
        {
            player1Render.sprite = tankRed;
        }

        if (CharChange.player1Sprite == "green")
        {
            player1Render.sprite = tankGreen;
        }

        if (CharChange.player1Sprite == "yellow")
        {
            player1Render.sprite = tankYellow;
        }

        if (CharChange.player1Sprite == "pink")
        {
            player1Render.sprite = tankPink;
        }


        if (CharChange.player2Sprite == "blue")
        {
            player2Render.sprite = tankBlue;
        }

        if (CharChange.player2Sprite == "red")
        {
            player2Render.sprite = tankRed;
        }

        if (CharChange.player2Sprite == "green")
        {
            player2Render.sprite = tankGreen;
        }

        if (CharChange.player2Sprite == "yellow")
        {
            player2Render.sprite = tankYellow;
        }

        if (CharChange.player2Sprite == "pink")
        {
            player2Render.sprite = tankPink;
        }
    }

    //function for changing players, switches everything to prev player and sets prev player the current player. You know what I mean
    public void ChangePlayer()
    {
        timeleft = 15f;
        currentPlayer = prevPlayer;
        currentCanon = prevCanon;
        currentFirePoint = prevFirePoint;
        tiltConst = prevTiltConst;

        if (GameObject.Equals(currentPlayer, player1))
        {
            prevPlayer = player2;
        }

        if (GameObject.Equals(currentPlayer, player2))
        {
            prevPlayer = player1;
        }

        if (GameObject.Equals(currentCanon, canonPlayer1))
        {
            prevCanon = canonPlayer2;
        }

        if (GameObject.Equals(currentCanon, canonPlayer2))
        {
            prevCanon = canonPlayer1;
        }

        if (GameObject.Equals(currentFirePoint, firePointPlayer1))
        {
            prevFirePoint = firePointPlayer2;
        }

        if (GameObject.Equals(currentFirePoint, firePointPlayer2))
        {
            prevFirePoint = firePointPlayer1;
        }

        if (tiltConst == 0)
        {
            prevTiltConst = 180;
        }

        if (tiltConst == 180)
        {
            prevTiltConst = 0;
        }
    }

    //function for setting ammo to normal bullet
    public void SetNormalBullet()
    {
        if (GameObject.Equals(currentPlayer, player1))
        {
            currentBulletP1 = normalBullet; 
        }

        if (GameObject.Equals(currentPlayer, player2))
        {
            currentBulletP2 = normalBullet;
        }
    }

    //function for setting ammo to heavy bullet
    public void SetHeavyBullet()
    {
        if (GameObject.Equals(currentPlayer, player1))
        {
            currentBulletP1 = heavyBullet;
        }

        if (GameObject.Equals(currentPlayer, player2))
        {
            currentBulletP2 = heavyBullet;
        }
    }

    public void SetKoronaBullet()
    {
        if (GameObject.Equals(currentPlayer, player1))
        {
            currentBulletP1 = koronaBullet;
        }

        if (GameObject.Equals(currentPlayer, player2))
        {
            currentBulletP2 = koronaBullet;
        }
    }

}
