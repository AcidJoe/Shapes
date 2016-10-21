using UnityEngine;
using System.Collections;
using SimpleJSON;

public class SocialManager : MonoBehaviour
{
    public int uid;
    public string _profile = "https://www.acidjoe.ru/Games/Shapes/PHP/profile.php";
    public string _update = "https://www.acidjoe.ru/Games/Shapes/PHP/update.php";

    public bool isDone = false;

    public void Initialize()
    {
        Application.ExternalCall("Init");
    }

    public void InitDone(string id)
    {
        uid = int.Parse(id);
    }

    public void loadProfile()
    {
        StartCoroutine(load());
    }

    public IEnumerator load()
    {
        if(Game.player.id != 0)
        {
            uid = Game.player.id;
        }
        isDone = false;

        WWWForm form = new WWWForm();
        form.AddField("method", "profile");
        form.AddField("uid", uid);
        WWW www = new WWW(_profile, form);
        yield return www;
        var _result = JSON.Parse(www.text);
        Game.player = new Profile(
            uid,
            _result[0]["lvl"].AsInt,
            _result[0]["exp"].AsInt,
            _result[0]["exp_to_next"].AsInt,
            _result[0]["money"].AsInt,
            _result[0]["premium"].AsInt
            );

        isDone = true;
    }

    public IEnumerator pay()
    {
        WWWForm form = new WWWForm();
        form.AddField("method", "pay");
        form.AddField("uid", Game.player.id);
        form.AddField("cost", Game.specialCost);
        WWW www = new WWW(_update, form);
        yield return www;
        loadProfile();
    }

    public IEnumerator endGame()
    {
        WWWForm form = new WWWForm();
        form.AddField("method", "endGame");
        form.AddField("uid", Game.player.id);
        form.AddField("matches", Game.matches);
        form.AddField("money", Game.MoneyGet());
        WWW www = new WWW(_update, form);
        yield return www;
        loadProfile();
    }
}
