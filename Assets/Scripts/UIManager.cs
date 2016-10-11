using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text money;
    public Text changeCost;
    public Text removeCost;
    public Text clearCost;
    public Text upShapeCost;

    public Text error;

    public bool isGameOver;
    public bool isLevelFill;

    float timer;
    public float fillTimer;

    public GameObject gameOverPanel;

    public Image levelBar;
    public Text playerLevel;

    public GameObject MoneyPanel;

    void Start()
    {
        timer = 5;
        isGameOver = false;
        error.gameObject.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        money.text = Game.player.money.ToString();

        changeCost.text = removeCost.text = clearCost.text = upShapeCost.text = Game.specialCost.ToString();

        if (isGameOver)
        {
            if (isLevelFill)
            {
                levelBar.fillAmount = (float)Game.player.exp / (float)Game.player.exp_to_next;
                playerLevel.text = Game.player.lvl.ToString();

                if(timer <= 0)
                {
                    if(Game.matches - 1 >= 0)
                    {
                        Game.matches--;
                        Game.player.exp++;

                        timer = fillTimer;
                    }
                    else
                    {
                        isLevelFill = false;
                    }
                }
            }
        }
    }

    public IEnumerator Error(int i)
    {
        string errorText = "";

        if (i == 1)
            errorText = "Нехватает денег";
        if (i == 2)
            errorText = "Нехватает фигур";

        error.text = errorText;
        error.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        error.gameObject.SetActive(false);
    }

    public IEnumerator GameOver()
    {
        MoneyPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        isGameOver = true;
        StartCoroutine(FillLevel());

        yield return new WaitWhile(() => isGameOver);
    }

    public IEnumerator FillLevel()
    {
        fillTimer = 3 / (float)Game.matches;
        Debug.Log(fillTimer);
        isLevelFill = true;

        yield return new WaitWhile(() => isLevelFill);
        yield return new WaitForSeconds(2);

        MoneyPanel.SetActive(true);
    }
}
