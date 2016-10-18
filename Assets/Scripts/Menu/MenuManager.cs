using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject loginPanel;
    public GameObject mainMenu;

    public Text PlayerLevel;
    public Text Money;

    public string playerName = "Тестер";

	void Start ()
    {
        if (!Game.isReady)
        {
            loginPanel.SetActive(true);
            mainMenu.SetActive(false);
        }
	}
	
	void Update ()
    {
        if (Game.isReady)
        {
            PlayerLevel.text = Game.player.lvl.ToString();
            Money.text = Game.player.money.ToString();
        }
	}

    public void SetName(string s)
    {
        playerName = s;
    }

    public void CreateProfile()
    {
        Game.player = new Profile(playerName);
        loginPanel.SetActive(false);
        Game.isReady = true;
        mainMenu.SetActive(true);
    }

    public void StartGame()
    {
        Game.SetDefaults();
        Game.GoToScene(1);
    }
}
