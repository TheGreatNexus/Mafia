using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string name;
public string role;
public int diamonds;

    public Player(string name,string role)
    {
        this.name = name;
        this.role = role;
        this.diamonds = 0;
    }
}
