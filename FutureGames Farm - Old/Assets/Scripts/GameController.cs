using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // building resources
    public int totalPower;
    public int totalFood;
    public int totalMoney;
    public int farmTotal;
    public int mineTotal;
    public int castleTotal;
    public int futureSpendableFood;
    public int futureSpendableMoney;
    public int futureCastles;
    public float enemyCurrentPower;
    // build mode
    public GameObject gameUI;
    public bool buildModeOn;
    private bool mouseExitOn;
    private GameObject cursor;
    public GameObject[] cursors;
    // pause
    public bool gameIsPaused;
    public GameObject pauseScreen;
    //timer
    public int minutes;
    public float timer;
    //win lose
    public GameObject winUI;
    public GameObject loseUI;


    public GameObject spawnerObject;
    Spawner spawn;
    public GameObject BuildingObject;
    Building building;
    // Start is called before the first frame update
    void Start()
    {
        spawn = spawnerObject.GetComponent<Spawner>();
        building = BuildingObject.GetComponent<Building>();
        cursor = cursors[1];

    }

    // Update is called once per frame
    void Update()
    {
        MouseCursor();
        SwitchCursor();

        if (Input.GetKeyDown(KeyCode.X))
        {
            totalPower += 1000;
        }
        GameTimer();
        PauseGame();
    }
    // sets mouseExitOn to true or false
    private void OnMouseExit()
    {
        mouseExitOn = true;
    }
    private void OnMouseEnter()
    {
        mouseExitOn = false;
    }
    //spawns
    private void OnMouseDown()
    {
        //spawns the selected building and removes cost from approriate "total"
        if (buildModeOn)
        {
            //farm
            if(spawn.building == spawn.buildings[0] && totalMoney >= spawn.cost)
            {
                spawn.SpawnBuilding(GetSquareClicked());
                BuildMode();
                totalMoney -= spawn.cost;
                mineTotal++;
            }
            //mine
            if (spawn.building == spawn.buildings[1] && totalFood >= spawn.cost)
            {
                spawn.SpawnBuilding(GetSquareClicked());
                BuildMode();
                totalFood -= spawn.cost;
                farmTotal++;
            }
            //castle
            if (spawn.building == spawn.buildings[2] && totalMoney >= spawn.cost && totalFood >= spawn.cost)
            {
                spawn.SpawnBuilding(GetSquareClicked());
                BuildMode();
                totalMoney -= spawn.cost;
                totalFood -= spawn.cost;
                castleTotal++;
            }
        }
    }
    // "cursor" follows the mouse as a custom mousecursor
    private void MouseCursor()
    {
        cursor.transform.position = GetSquareClicked();
    }
    // switchting between cursors
    private void SwitchCursor()
    {
        if (buildModeOn && !mouseExitOn) { cursor = cursors[0]; ActiveCursor(0); }
        else if(!mouseExitOn && !buildModeOn) { cursor = cursors[1]; ActiveCursor(1); }
        else if(mouseExitOn) { cursor = cursors[2]; ActiveCursor(2); }
    }

    public Vector2 GetSquareClicked()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        Vector2 gridPos = SnapToGrid(worldPos);
        return gridPos;
    }

    private Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.RoundToInt(rawWorldPos.y);
        return new Vector2(newX, newY);
    }

    //build mode
    public void BuildMode()
    {
        buildModeOn = !buildModeOn;
        gameUI.SetActive(!buildModeOn);
    }
    // sets the active cursor in the cursor array to only be visible
    private void ActiveCursor(int index)
    {
        for(int i = 0; i < cursors.Length; i++)
        {
            cursors[i].gameObject.SetActive(i == index);
        }
    }

    public void WinGame()
    {
        gameIsPaused = true;
        winUI.SetActive(true);
    }
    private void LoseGame()
    {
        gameIsPaused = true;
        loseUI.SetActive(true);
    }
    private void GameTimer()
    {
        if (!gameIsPaused)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                minutes--;
                timer = 60;
            }
            else if (minutes < 0)
            {
                LoseGame();
            }
        }
    }
    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            pauseScreen.SetActive(gameIsPaused);
        }

    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    
}
