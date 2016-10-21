using UnityEngine;
using System.Collections;

public class Profile
{
    public int id;
    public string _name;
    public string photo;

    public int money;

    public int lvl;
    public int exp;
    public int exp_to_next;

    public int premium;

    public Profile(string n)
    {
        _name = n;

        lvl = 1;
        exp = 0;

        exp_to_next = 10 + (lvl * 5);

        money = 100000;
    }

    public Profile(int _id, int lv, int ex, int etn, int mon, int pr)
    {
        id = _id;

        lvl = lv;
        exp = ex;

        exp_to_next = etn;

        money = mon;
        premium = pr;
    }

    public void lvlUp()
    {
        lvl++;
        exp = 0;
        exp_to_next = 10 + (lvl * 5);
    }
}
