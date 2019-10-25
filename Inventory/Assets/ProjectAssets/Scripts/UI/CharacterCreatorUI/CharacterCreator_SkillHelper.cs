using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterCreator_SkillHelper : MonoBehaviour {

    public int skillIndex;
    public Text titleText;
    public Text valueText;
    public Image skillIcon;

    //private Skill skill;
    private CharacterCreator charCreator;

    public void SetUp(CharacterCreator creator,int index, Skill skil)
    {
        skillIndex = index;
        charCreator = creator;
        titleText.text = ((SkillName)index).ToString();
        valueText.text = skil.AdjustedBaseValue.ToString();

    }

    public void UpdateStatUi()
    {
        valueText.text = charCreator.playerCharacter.GetSkill(skillIndex).AdjustedBaseValue.ToString();
    }
}
