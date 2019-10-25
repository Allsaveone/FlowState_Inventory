using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterCreator_VitalHelper : MonoBehaviour {

    public int vitalIndex;
    public Text titleText;
    public Text valueText;
    public Image vitalIcon;

    //private Vital vital;
    private CharacterCreator charCreator;

    public void SetUp (CharacterCreator creator,int index, Vital vit)
    {
        vitalIndex = index;
        charCreator = creator;
        titleText.text = ((VitalName)index).ToString();
        valueText.text = vit.AdjustedBaseValue.ToString();
    }

    public void UpdateStatUi()
    {
        valueText.text = charCreator.playerCharacter.GetVital(vitalIndex).AdjustedBaseValue.ToString();
    }
}
