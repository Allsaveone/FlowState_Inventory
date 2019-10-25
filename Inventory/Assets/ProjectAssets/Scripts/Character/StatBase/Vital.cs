using UnityEngine;
using System.Collections;

public enum VitalName
{
    Health,
    Armour,
    //Mana,
    Damage    
}

public class Vital : ModifiedStat {

    private int _currentValue; 

	public Vital(){
        _currentValue = 0;
        ExpToLevel = 50;
        LevelModifier = 1.1f;//10%
	}

    public int CurrentValue
    {
        get {
            if (_currentValue > AdjustedBaseValue)
            {
                _currentValue = AdjustedBaseValue;
            }

            return _currentValue;
        }
        set { _currentValue = value; }
    }

  
}
