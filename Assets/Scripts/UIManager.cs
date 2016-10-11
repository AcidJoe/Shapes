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
    public bool needWait;

    public float fill;
    public float fillTimer;

    public GameObject gameOverPanel;

    public Image levelBar;
    public Text playerLevel;

    public float curAmount;
    public float nextAmount;
    public float changeBar;

    public GameObject MoneyPanel;

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
        fill += fillTimer * Time.deltaTime;
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
                        StartCoroutine(filler());
                    }
                    else
                    {
                        isLevelFill = false;
                    }
                }
            }

            levelBar.fillAmount = changeBar;
            playerLevel.text = Game.player.lvl.ToString();
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

    public IEnumerator filler()
    {
        needWait = true;
        Game.matches--;
        Game.player.exp++;

        nextAmount = (float)Game.player.exp / (float)Game.player.exp_to_next;

        changeBar = Mathf.Lerp(curAmount, nextAmount, fill);

        yield return new WaitForSeconds(fillTimer);

        curAmount = nextAmount;
        needWait = false;
    }
}
