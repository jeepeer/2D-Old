using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // buildings
    public GameObject building;
    public GameObject farm;
    public GameObject mine;
    public GameObject castle;
    public GameObject[] buildings;
    public int cost = 5;

    //animal spawner
    public BoxCollider2D spawnCollider;
    public Vector2 animalSpawnVector;
    public Vector2 spawnColliderVector;
    public Transform spawnColliderTransform;
    private Vector2 centerVector;
    public GameObject animal;
    [SerializeField] private int totalAnimals = 5;
    [SerializeField] public int currentAnimalAmount;
    // coords
    private float xx;
    private float yy;
    private float Cxx;
    private float Cyy;

    public GameObject gameControllerObject;
    GameController game;

    // Start is called before the first frame update
    void Start()
    {
        game = gameControllerObject.GetComponent<GameController>();

        ColliderSizeAndTransform();
        InvokeRepeating("SpawnAnimal", 0f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        SelectBuilding();
        SpawnArea();
        // reminder...
        /*if (Input.GetKeyDown(KeyCode.C))
        { Instantiate(animal, animalSpawnVector, Quaternion.identity); }*/
    }
    //selects which building should be placed down
    public void SelectBuilding()
    {
        if(!game.gameIsPaused)
        {   //mine
            if (Input.GetKeyDown(KeyCode.Alpha1)) { Farm(); }
            //farm
            else if (Input.GetKeyDown(KeyCode.Alpha2)) { Mine(); }
            //castle
            else if (Input.GetKeyDown(KeyCode.Alpha3)) { Castle(); }
        }

    }
    // how buildings spawn
    public void SpawnBuilding(Vector2 worldPos)
    {
        Instantiate(building, worldPos, Quaternion.identity);
    }

    public void Farm()
    {
        building = buildings[0];
        game.BuildMode();
    }
    public void Mine()
    {
        building = buildings[1];
        game.BuildMode();
    }
    public void Castle()
    {
        building = buildings[2];
        game.BuildMode();
    }

    // animal spawn
    public void SpawnArea()
    {
        animalSpawnVector = new Vector2(Random.Range((Cxx - (xx * 0.5f)), (Cxx + (xx / 2))),
            Random.Range((Cyy - (yy * 0.5f)), (Cyy + (yy / 2))));
    }

    private void ColliderSizeAndTransform()
    {
        // size of box collider
        spawnColliderVector = spawnCollider.bounds.size;
        xx = spawnColliderVector.x;
        yy = spawnColliderVector.y;

        // position of collider transform
        centerVector = spawnColliderTransform.position;
        Cxx = centerVector.x;
        Cyy = centerVector.y;
    }
    private void SpawnAnimal()
    {
        if(currentAnimalAmount < totalAnimals)
        {
            Instantiate(animal, animalSpawnVector, Quaternion.identity);
            currentAnimalAmount++;
        }
    }
}
