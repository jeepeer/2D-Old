using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    private int randomNumber;

    [SerializeField] private float animalMovementSpeed;

    public float walktime;
    private bool stop;
    public int randomFood;
    public GameObject animalMenu;
    Rigidbody2D rb2D;

    GameController game;
    public GameObject gameControllerObject;
    Spawner spawn;
    public GameObject spawnObject;

    private void Start()
    {
        game = gameControllerObject.GetComponent<GameController>();
        spawn = spawnObject.GetComponent<Spawner>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        randomFood = Random.Range(1, 5);
        if (!stop) { randomNumber = Random.Range(0, 5); }

        walktime += Time.deltaTime;
        if(walktime >= 2) { Walk(); walktime = 0; }

    }
 
    private void Walk()
    {
        Vector2 velocity = Vector2.zero;
        switch (randomNumber)
        {
            case 0:
                velocity += Vector2.up;
                Debug.Log("up");
                break;
            case 1:
                velocity += Vector2.down;
                Debug.Log("down");
                break;
            case 2:
                velocity += Vector2.left;
                Debug.Log("left");
                break;
            case 3:
                velocity += Vector2.right;
                Debug.Log("right");
                break;
            case 4:
                velocity += Vector2.zero;
                Debug.Log("stop");
                break;
        }
        rb2D.velocity = velocity.normalized * animalMovementSpeed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // stops the animal from moving 
        if (collision.gameObject.tag == "Player")
        {
            randomNumber = 5;
            stop = true;
            animalMenu.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // resets the movement pattern
        if (collision.gameObject.tag == "Player")
        {
            stop = false;
            animalMenu.SetActive(false);
        }
    }

    public void AnimalDie()
    {
        // REMINDER to see if 1-5 food is enough
        game.totalFood += randomFood;
        spawn.currentAnimalAmount--;
        Destroy(gameObject);
        Debug.Log("food");
    }


}
