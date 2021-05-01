using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    public Text currentFoodText;
    public Text currentMoneyText;
    public Text currentPowerText;
    public Text minuteText;
    public Text secondsText;

    GameController game;
    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        currentFoodText.text = game.totalFood.ToString();
        currentMoneyText.text = game.totalMoney.ToString();
        currentPowerText.text = game.totalPower.ToString();
        minuteText.text = game.minutes.ToString();
        secondsText.text = game.timer.ToString();
    }
}

