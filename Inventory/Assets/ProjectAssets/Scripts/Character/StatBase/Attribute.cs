using UnityEngine;
using System.Collections;

public enum AttributeName
{
    Strength,
    Endurance,
    Agility,
    Speed,
    Focus,
    Will,
    Charisma
}

public class Attribute : BaseStat {

    public Attribute()
    {
        ExpToLevel = 50;
        LevelModifier = 1.05f;
    }
}
