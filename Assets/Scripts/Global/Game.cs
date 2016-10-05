using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public static class Game
{
    public static Profile player;

    public static int matches;
    
    public static int specialCost;

    public static bool isReady = false;
    public static bool isStart = false;

    public static void GoToScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    public static void SetDefaults()
    {
        matches = 0;

        specialCost = 50;
    }

    public static void IncreaseCost()
    {
        specialCost *= 2;
    }

    public static void Pay()
    {
        player.money -= specialCost;
    }
}
