using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb2D;
    [SerializeField] private float playerMovementSpeed;
    public Sprite[] character;

    SpriteRenderer spriteRenderer;
    GameController game;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        game = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }


    //Player movement
    private void PlayerMovement()
    {
        Vector2 velocity = Vector2.zero;

        if (!game.gameIsPaused)
        {        // 4 directions, forward, backward, left, right + diagonal
            if (Input.GetKey(KeyCode.W))
            {
                velocity += Vector2.up;
                spriteRenderer.sprite = character[1];
            }
            if (Input.GetKey(KeyCode.S))
            {
                velocity += Vector2.down;
                spriteRenderer.sprite = character[0];
            }
            if (Input.GetKey(KeyCode.D))
            {
                velocity += Vector2.right;
                spriteRenderer.sprite = character[2];
            }
            if (Input.GetKey(KeyCode.A))
            {
                velocity += Vector2.left;
                spriteRenderer.sprite = character[3];
            }
            rb2D.velocity = velocity.normalized * playerMovementSpeed;
        }  
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 5);
    }


}

