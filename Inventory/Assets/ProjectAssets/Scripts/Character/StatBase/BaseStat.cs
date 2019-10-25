using UnityEngine;
using System.Collections;

public class BaseStat  {

    private int _baseValue; //bace value of stat
    private int _buffValue; //buff ammount for this stat
    private int _expToLevel; //xp to raise stat
    private float _levelModifier;//level xp modifer for increaseing stat

    public BaseStat()
    {
        _baseValue = 0;
        _buffValue = 0;
        _levelModifier = 1.1f;
        _expToLevel = 100;
    }
    #region 
    public int BaseValue
    {
        get { return _baseValue; }
        set { _baseValue = value; }
    }

    public int BuffValue
    {
        get { return _buffValue; }
        set { _buffValue = value; }
    }

    public int ExpToLevel
    {
        get { return _expToLevel; }
        set { _expToLevel = value; }
    }

    public float LevelModifier
    {
        get { return _levelModifier; }
        set { _levelModifier = value; }
    }
    #endregion

    private int CalculateExpToLevel()
    {
        return _expToLevel * (int)_levelModifier;
    }

    public void LevelUp()
    {
        _expToLevel = CalculateExpToLevel();
        _baseValue++;
    }

    public int AdjustedBaseValue
    {
        get{ return _baseValue + _buffValue; }
    }
}
