using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{

    private Vector2 buildingPosition;
    public LayerMask buildingLayer;
    private Vector2 attackerPosition;
    private float maxRange = 20f;
    private float movementSpeed = 5f;


    public LayerMask wall;
    public LayerMask attackerlayer;
    private GameObject closestBuilding;
    Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        attackerPosition = gameObject.transform.position;
        if(Input.GetKeyDown(KeyCode.L)) { LookForBuilding(); }
        LookForBuilding(); 

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "building")
        {
            closestBuilding.GetComponent<Building>().TakeDamage();
            //rb2D.AddForce(-buildingPosition * 10f);
        }
    }

    private void GoTowardsBuilding()
    {
        //moves attacker towards the nearest building
        transform.position = Vector2.MoveTowards
            (attackerPosition, buildingPosition, movementSpeed * Time.deltaTime);
    }

    private void LookForBuilding()
    {
        //looks for closest building
        Collider2D[] collisions = Physics2D.OverlapCircleAll(attackerPosition, maxRange, buildingLayer);
        foreach (var collision in collisions)
        {
            buildingPosition = collision.transform.position;
            closestBuilding = collision.gameObject;
            if(collisions.Length >= 1)
            {
                GoTowardsBuilding();
            }
            Debug.Log(buildingPosition + "hej");
            Debug.Log(collisions.Length + "array length");
            Debug.Log(closestBuilding.transform.position + "closest");
        }
    }


    //look for building
    //follow player
    //lose player


}
