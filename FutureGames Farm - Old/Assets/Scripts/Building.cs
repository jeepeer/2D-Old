using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int power;
    public int food;
    public int money;
    private int buildingHealth = 3;
    [SerializeField]private int currentBuildingHealth;

    public bool isFarm;
    public bool isCastle;

    public float tickResources;


    GameController game;
    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<GameController>();
        currentBuildingHealth = buildingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        tickResources += Time.deltaTime;
        ResourceGeneration();
        BuildingIdentifier();
    }

    private void ResourceGeneration()
    {
        if (!game.gameIsPaused)
        {
            if (tickResources >= 2)
            {
                game.totalPower += power;
                game.totalFood += food;
                game.totalMoney += money;
                tickResources = 0;
            }
        }
    }

    private void BuildingIdentifier()
    {
        if(currentBuildingHealth <= 0)
        {
            if (isFarm) { BuildingDestroyer(1); }
            if (isCastle) { BuildingDestroyer(2); }
            if (!isFarm && !isCastle) { BuildingDestroyer(3); }
        }
    }

    private void BuildingDestroyer(int buildingType)
    {
        switch(buildingType)
        {
            case 1:
                game.farmTotal--;
                Destroy(gameObject);
                break;
            case 2:
                game.mineTotal--;
                Destroy(gameObject);
                break;
            case 3:
                game.castleTotal--;
                Destroy(gameObject);
                break;
        }
    }

    public int TakeDamage()
    {
        return currentBuildingHealth--;
    }

}
