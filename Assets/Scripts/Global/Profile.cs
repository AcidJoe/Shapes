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

    public Profile(string n)
    {
        _name = n;

        lvl = 1;
        exp = 0;

        exp_to_next = 20;

        money = 100000;
    }
}
