using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest
{   public int diamonds;
    public int loyalHenchmens;
    public int agents;
    public int drivers;
    public int jokers;

    public Chest(int loyalHenchmens, int agents, int drivers, int jokers)
    {
        this.diamonds = 15;
        this.loyalHenchmens = loyalHenchmens;
        this.agents = agents;
        this.drivers = drivers;
        this.jokers = jokers;
    }
}
