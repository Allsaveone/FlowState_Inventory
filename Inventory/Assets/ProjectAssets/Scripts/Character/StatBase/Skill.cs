using UnityEngine;
using System.Collections;

public enum SkillName {

    M_Att,
    M_Def,
    R_Att,
    R_Def,
    P_Att,
    P_Def
}

public class Skill : ModifiedStat {

    private bool _known;

    public Skill() {
        _known = false;
        ExpToLevel = 25; 
    }

    public bool Known
    {
        get { return _known; }
        set { _known = value; }
    }
}
