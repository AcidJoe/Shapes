using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public static class Game
{
    public static Profile player;
    public static SocialManager sm;

    public static int matches;
    public static int money;
    public static int playerMoney;
    public static int allMoney;
    
    public static int specialCost;

    public static bool isReady = false;
    public static bool isStart = false;

    public static int[] shapes;

    public static void GoToScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    public static void SetDefaults()
    {
        matches = 0;
        money = 0;
        playerMoney = 0;
        allMoney = 0;

        specialCost = 50;

        shapes = new int[] { 0, 0, 0, 0, 0, 0, 0};
    }

    public static void IncreaseCost()
    {
        specialCost *= 2;
    }

    public static void Pay()
    {
        sm.StartCoroutine(sm.pay());
    }

    public static int MoneyGet()
    {
        int _money = 0;

        for(int i = 0; i < 6; i++)
        {
            if(i < 3)
            {
                _money += shapes[i] * 2;
            }
            else
            {
                _money += shapes[i] * 3;
            }
        }
        return _money * 2;
    }
}
