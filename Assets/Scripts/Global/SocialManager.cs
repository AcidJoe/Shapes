using UnityEngine;
using System.Collections;
using SimpleJSON;

public class SocialManager : MonoBehaviour
{
    public int uid;
    public string playerName;
    private string _profile = "https://www.acidjoe.ru/Games/Shapes/PHP/profile.php";

    public bool isDone = false;

    public void Initialize()
    {
        Application.ExternalCall("Init");
    }

    public void InitDone(string id, string n)
    {
        uid = int.Parse(id);
        playerName = n;
    }

    public void loadProfile()
    {
        StartCoroutine(load());
    }

    public IEnumerator load()
    {
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
}
