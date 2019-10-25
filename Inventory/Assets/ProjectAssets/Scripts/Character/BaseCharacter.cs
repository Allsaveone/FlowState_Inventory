using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BaseCharacter  {

    private string _name;
    private int _level;
    private uint _freeExp;

    private Attribute[] _attributes;
    private Vital[] _vitals;
    private Skill[] _skills;

	public void Awake ()
    {
        _name = string.Empty;
        _level = 0;
        _freeExp = 0;

        _attributes = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];
        _vitals = new Vital[Enum.GetValues(typeof(VitalName)).Length];
        _skills = new Skill[Enum.GetValues(typeof(SkillName)).Length];

        SetUpAttributes();
        SetUpVitals();
        SetUpSkills();
    }

    #region Get Basics
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int Level
    {
        get { return _level; }
        set { _level = value; }
    }

    public uint FreeEXP
    {
        get { return _freeExp; }
        set { _freeExp = value; }
    }

    public Attribute[] Attributes
    {
        get { return _attributes; }
    }
    #endregion

    #region Exp
    public void AddExp(uint exp)
    {
        _freeExp += exp;

        CalculateLevel();
    }

    public void CalculateLevel()
    {
        //Check for level up or a Value that represents the characters Worth in stat points.
    }
    #endregion

    #region SetUp
    private void SetUpAttributes ()
    {
        for (int i = 0; i < _attributes.Length; i++)
        {
            _attributes[i] = new Attribute();
        }
    }

    private void SetUpVitals()
    {
        for (int i = 0; i < _vitals.Length; i++)
        {
            _vitals[i] = new Vital();
        }
        SetUpVitalModifiers();

    }

    private void SetUpSkills()
    {
        for (int i = 0; i < _skills.Length; i++)
        {
            _skills[i] = new Skill();
        }
        SetUpSkillModifiers();
    }
    #endregion

    #region Get Stat
    public Attribute GetAttribute(int index)
    {
        return _attributes[index];
    }

    public Vital GetVital(int index)
    {
        return _vitals[index];
    }

    public Skill GetSkill(int index)
    {
        return _skills[index];
    }
    #endregion

    private void SetUpVitalModifiers()//Pass in class modifiers?
    {
        //Health
        /*
        ModifingAttribute health = new ModifingAttribute();
        health.attribute = GetAttribute((int)AttributeName.Constitution);
        health.ratio = 0.5f; //percentage of stat that will be added to health
        GetVital((int)VitalName.Health).AddModifier(health);
        */
        GetVital((int)VitalName.Health).AddModifier(new ModifingAttribute {attribute = GetAttribute((int)AttributeName.Endurance),ratio = 1f });
        //Stamina
        GetVital((int)VitalName.Armour).AddModifier(new ModifingAttribute { attribute = GetAttribute((int)AttributeName.Endurance), ratio = 0.1f });
        //Mana
        //GetVital((int)VitalName.Mana).AddModifier(new ModifingAttribute { attribute = GetAttribute((int)AttributeName.Willpower), ratio = 1f });
        //Damage?
        GetVital((int)VitalName.Damage).AddModifier(new ModifingAttribute { attribute = GetAttribute((int)AttributeName.Strength), ratio = 0.5f });
        //Toughness?
    }

    private void SetUpSkillModifiers()
    {
        //Melee Attack
        GetSkill((int)SkillName.M_Att).AddModifier(new ModifingAttribute(GetAttribute((int)AttributeName.Strength), 0.25f));
        GetSkill((int)SkillName.M_Att).AddModifier(new ModifingAttribute(GetAttribute((int)AttributeName.Agility), 0.25f));
        //Mellee Defense
        GetSkill((int)SkillName.M_Def).AddModifier(new ModifingAttribute(GetAttribute((int)AttributeName.Speed), 0.25f));
        GetSkill((int)SkillName.M_Def).AddModifier(new ModifingAttribute(GetAttribute((int)AttributeName.Endurance), 0.25f));

        //Range Attack
        GetSkill((int)SkillName.R_Att).AddModifier(new ModifingAttribute(GetAttribute((int)AttributeName.Focus), 0.25f));
        GetSkill((int)SkillName.R_Att).AddModifier(new ModifingAttribute(GetAttribute((int)AttributeName.Speed), 0.25f));
        //Range Defense
        GetSkill((int)SkillName.R_Def).AddModifier(new ModifingAttribute(GetAttribute((int)AttributeName.Speed), 0.25f));
        GetSkill((int)SkillName.R_Def).AddModifier(new ModifingAttribute(GetAttribute((int)AttributeName.Agility), 0.25f));

        //Magic Attack
        GetSkill((int)SkillName.P_Att).AddModifier(new ModifingAttribute(GetAttribute((int)AttributeName.Focus), 0.25f));
        GetSkill((int)SkillName.P_Att).AddModifier(new ModifingAttribute(GetAttribute((int)AttributeName.Will), 0.25f));
        //Magic Defense
        GetSkill((int)SkillName.P_Def).AddModifier(new ModifingAttribute(GetAttribute((int)AttributeName.Focus), 0.25f));
        GetSkill((int)SkillName.P_Def).AddModifier(new ModifingAttribute(GetAttribute((int)AttributeName.Will), 0.25f));    
    }

    public void UpdateCharacterStats()
    {
        for (int i = 0; i < _vitals.Length; i++)
        {
            _vitals[i].UpdateModifer();
            
        }

        for (int j = 0; j < _skills.Length; j++)
        {
            _skills[j].UpdateModifer();
        }
    }
}
