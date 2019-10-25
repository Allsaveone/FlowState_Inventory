using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CharacterCreator : MonoBehaviour {

    [SerializeField]
    public PlayerCharacter playerCharacter;
    private const int _STARTING_CHARACTER_POINTS = 85;
    private const int _STARTING_ATTRIBUTE_VALUE = 10;
    private const int _MINIMUM_ATTRIBUTE_VALUE = 2;

    public int characterPoints;

    public GameObject attribute_Layout;
    public GameObject attributeUI_Element;
    public List<CharacterCreator_AttributeHelper> attribute_UI;

    public GameObject vital_Layout;
    public GameObject vitalUI_Element;
    public List<CharacterCreator_VitalHelper> vital_UI;

    public GameObject skill_Layout;
    public GameObject skillUI_Element;
    public List<CharacterCreator_SkillHelper> skill_UI;

    void Start ()
    {
        characterPoints = _STARTING_CHARACTER_POINTS;
        playerCharacter = new PlayerCharacter();
        playerCharacter.Awake();
        Assign_StartingAttributeValues();// Starting values as race/class profiles?
        playerCharacter.UpdateCharacterStats();
        Setup_UI();
    }

    void Assign_StartingAttributeValues()
    {
        for (int i = 0; i < playerCharacter.Attributes.Length; i++)
        {
            playerCharacter.GetAttribute(i).BaseValue = _STARTING_ATTRIBUTE_VALUE;
            characterPoints -= _STARTING_ATTRIBUTE_VALUE;
        }
    }
	
	void Setup_UI ()
    {
        //Attribute UI
        for (int i = 0; i < Enum.GetValues(typeof(AttributeName)).Length; i++)
        {
            GameObject go = Instantiate(attributeUI_Element, attribute_Layout.transform.position, Quaternion.identity) as GameObject;
            CharacterCreator_AttributeHelper helper = go.GetComponent<CharacterCreator_AttributeHelper>();
            helper.SetUp(this, i, playerCharacter.GetAttribute(i));
            attribute_UI.Add(helper);
            go.transform.SetParent(attribute_Layout.transform, true);
        }

        //Vital UI
        for (int i = 0; i < Enum.GetValues(typeof(VitalName)).Length; i++)
        {
            GameObject go = Instantiate(vitalUI_Element, vital_Layout.transform.position, Quaternion.identity) as GameObject;
            CharacterCreator_VitalHelper helper = go.GetComponent<CharacterCreator_VitalHelper>();
            helper.SetUp(this, i, playerCharacter.GetVital(i));
            vital_UI.Add(helper);
            go.transform.SetParent(vital_Layout.transform, true);
        }

        //Skills
        for (int i = 0; i < Enum.GetValues(typeof(SkillName)).Length; i++)
        {
            GameObject go = Instantiate(skillUI_Element, skill_Layout.transform.position, Quaternion.identity) as GameObject;
            CharacterCreator_SkillHelper helper = go.GetComponent<CharacterCreator_SkillHelper>();
            helper.SetUp(this,i, playerCharacter.GetSkill(i));
            skill_UI.Add(helper);
            go.transform.SetParent(skill_Layout.transform, true);
        }    
    }

    public void IncreaseAttritute_Button(int attribute)
    {
        if(characterPoints > 0)
        {
            //Debug.Log(((AttributeName)attribute).ToString()+ " Increased");
            characterPoints--;
            playerCharacter.GetAttribute(attribute).BaseValue++;
            playerCharacter.UpdateCharacterStats();

            UpdateStatUI();
        }        
    }

    public void DeacreaseAttritute_Button(int attribute)
    {
        if(playerCharacter.GetAttribute(attribute).BaseValue == _MINIMUM_ATTRIBUTE_VALUE)
        {
            return;
        }
        //Debug.Log(((AttributeName)attribute).ToString()+ " Decreased");
        characterPoints++;
        playerCharacter.GetAttribute(attribute).BaseValue--;
        playerCharacter.UpdateCharacterStats();

        UpdateStatUI();
    }

    void UpdateStatUI()
    {
        for (int i = 0; i < attribute_UI.Count; i++)
        {
            attribute_UI[i].UpdateStatUi();
        }

        for (int i = 0; i < vital_UI.Count; i++)
        {
            vital_UI[i].UpdateStatUi();
        }

        for (int i = 0; i < skill_UI.Count; i++)
        {
            skill_UI[i].UpdateStatUi();
        }
    }
}
