using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterCreator_AttributeHelper : MonoBehaviour {

    public int attributeIndex;
    public Text titleText;
    public Text valueText;
    public Image attributeIcon;
    public Button increaseButton;
    public Button decreaseButton;

    //private Attribute attribute;
    private CharacterCreator charCreator;

    public void SetUp(CharacterCreator creator, int index, Attribute att)
    {
        attributeIndex = index;
        //attribute = att;
        charCreator = creator;
        titleText.text = ((AttributeName)index).ToString();
        valueText.text = att.AdjustedBaseValue.ToString();

        increaseButton.onClick.AddListener(() => creator.IncreaseAttritute_Button(index));
        decreaseButton.onClick.AddListener(() => creator.DeacreaseAttritute_Button(index));
    }

    public void UpdateStatUi()
    {
        valueText.text = charCreator.playerCharacter.GetAttribute(attributeIndex).AdjustedBaseValue.ToString();
    }

    void OnDestroy()
    {
        increaseButton.onClick.RemoveAllListeners();
        decreaseButton.onClick.RemoveAllListeners();
    }
}
