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

    public GameObject gameOverPanel;
    public Text matches;
    public Text moneyGet;

    public Text[] counts;

    void Start()
    {
        error.gameObject.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        money.text = Game.player.money.ToString();

        changeCost.text = removeCost.text = clearCost.text = upShapeCost.text = Game.specialCost.ToString();

        for(int i = 0; i < 6; i++)
        {
            counts[i].text = Game.shapes[i].ToString();
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

    public void GameOver()
    {
        matches.text = Game.matches.ToString();
        moneyGet.text = Game.MoneyGet().ToString();

        gameOverPanel.SetActive(true);
    }
}
