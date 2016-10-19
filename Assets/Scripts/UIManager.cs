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

    public Image[] functions;
    public Color inactive;
    public Color _activ;

    public bool isGameOver;
    public bool isLevelFill;
    public bool needWait;
    public bool isMoneyFill;

    public float fillTimer;

    public GameObject gameOverPanel;

    public Image levelBar;
    public Text playerLevel;

    public float curAmount;
    public float nextAmount;

    public GameObject MoneyPanel;

    public Image moneyBar;
    public Text moneyGet;
    public Text fullMoney;

    public Button home;
    public Button replay;

    void Start()
    {
        needWait = false;
        curAmount = 0;
        isGameOver = false;
        error.gameObject.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        money.text = Game.player.money.ToString();

        changeCost.text = removeCost.text = clearCost.text = upShapeCost.text = Game.specialCost.ToString();

        if (isGameOver)
        {
            if (isLevelFill)
            {
                if(!needWait)
                {
                    if(Game.matches - 1 >= 0)
                    {
                        StartCoroutine(filler("level"));
                    }
                    else
                    {
                        isLevelFill = false;
                    }
                }

                curAmount = levelBar.fillAmount;
                if(curAmount >= nextAmount)
                {
                    needWait = false;
                }

                levelBar.fillAmount += fillTimer * Time.deltaTime;
                playerLevel.text = Game.player.lvl.ToString();
            }
            else if (isMoneyFill)
            {
                if (!needWait)
                {
                    if (Game.money - 1 >= 0)
                    {
                        StartCoroutine(filler("money"));
                    }
                    else
                    {
                        isMoneyFill = false;
                    }
                }

                curAmount = moneyBar.fillAmount;

                if (curAmount >= nextAmount)
                {
                    needWait = false;
                }

                moneyBar.fillAmount += fillTimer * Time.deltaTime;
                moneyGet.text = Game.playerMoney.ToString();
                fullMoney.text = Game.allMoney.ToString();
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
        home.interactable = false;
        replay.interactable = false;

        yield return new WaitWhile(() => isGameOver);
    }

    public IEnumerator FillLevel()
    {
        levelBar.fillAmount = 0;
        isLevelFill = true;

        yield return new WaitWhile(() => isLevelFill);
        yield return new WaitForSeconds(1.5f);

        StartCoroutine(fillMoney());
    }

    public IEnumerator filler(string operation)
    {
        needWait = true;
        switch (operation)
        {
            case "level":
                Game.matches--;
                Game.player.exp++;

                if(Game.player.exp == Game.player.exp_to_next)
                {
                    Game.player.lvlUp();
                }

                nextAmount = (float)Game.player.exp / (float)Game.player.exp_to_next;
                break;
            case "money":
                Game.money--;
                Game.playerMoney++;

                nextAmount = (float)Game.playerMoney / (float)Game.allMoney;
                break;
        }

        yield return new WaitWhile(() => needWait);

        curAmount = nextAmount;
    }

    public IEnumerator fillMoney()
    {
        MoneyPanel.SetActive(true);
        moneyBar.fillAmount = 0;
        Game.allMoney = Game.MoneyGet();
        Game.money = Game.MoneyGet() / 2;
        Game.playerMoney = 0;
        isMoneyFill = true;

        yield return new WaitWhile(() => isMoneyFill);
        yield return new WaitForSeconds(1);

        home.interactable = true;
        replay.interactable = true;
    }

    public void activeSkill(int i)
    {
        if(i == 0)
        {
            for(int j = 1; j < 4; j++)
            {
                functions[j].color = inactive;
            }
        }
        else
        {
            functions[i].color = _activ;
        }
    }
}
