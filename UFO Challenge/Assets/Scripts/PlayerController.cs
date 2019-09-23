using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public Text loseText;
    public Text livesText;

    private Rigidbody2D rb2d;
    private SpriteRenderer sprite_rn;
    private CircleCollider2D player_collide;
    private int count;
    private int playerLives;

    void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprite_rn = GetComponent<SpriteRenderer>();
        player_collide = GetComponent<CircleCollider2D>();
        count = 0;
        playerLives = 3;
        winText.text = "";
        loseText.text = "";
        livesText.text = "";
        SetCountText();
    }

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
        Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
        rb2d.AddForce (movement * speed);

        if (Input.GetKey("escape"))
            Application.Quit();

        if (playerLives <= 0 | count >= 20)
        {
            Destroy(sprite_rn);
            Destroy(player_collide);
            rb2d.isKinematic = true;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            playerLives = playerLives - 1;
            SetCountText();
        }


    }

    void SetCountText ()
    {

        if (count == 12)
        {
            transform.position = new Vector2(50.0f, 50.0f); 

        }


        countText.text = "Count: " + count.ToString();
        livesText.text = "Lives: " + playerLives.ToString();
        if (count >= 20)
        {
            winText.text = "You Win!" + 
                "\n" +  
                "Game created by Jonathan Gonzalez!";
        }
        else if (playerLives <= 0)
        {
            loseText.text = "You Lose!" +
                "\n" +
                "Game created by Jonathan Gonzalez!";
        }
    }
}
