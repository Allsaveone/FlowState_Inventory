using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct ModifingAttribute {

    public Attribute attribute;
    public float ratio;//percentage added

    public ModifingAttribute(Attribute att, float rat)//Constructor for structs!! Wuuuuuuuut?
    {
        attribute = att;
        ratio = rat;
    }
}

public class ModifiedStat : BaseStat {

    private List<ModifingAttribute> _modifiers; //The attributes that modify this stat
    private int _modiferValue; //ammount added to baseStat

    public ModifiedStat() {
        _modifiers = new List<ModifingAttribute>();
        _modiferValue = 0;
	}

    public void AddModifier(ModifingAttribute mod)
    {
        _modifiers.Add(mod);
    }

    private void CalculateModifierValue()
    {
        _modiferValue = 0;

        if (_modifiers.Count > 0)
        {
            foreach (ModifingAttribute att in _modifiers)
            {
                _modiferValue += (int)(att.attribute.AdjustedBaseValue * att.ratio);
            }
        }
    }

    public new int AdjustedBaseValue
    {
        get { return BaseValue + BuffValue + _modiferValue; }
    }

    public void UpdateModifer()
    {
        CalculateModifierValue();
    }
}
