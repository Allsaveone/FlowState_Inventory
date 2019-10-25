using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;

public class ToolTip : MonoBehaviour {

    public Item item;
    public GameObject toolTipObj;

    public Color colour;
    private string itemDataString;
    public Text itemText;

    void Start()
    {
        toolTipObj.SetActive(false);
    }

    void Update()
    {
        if(toolTipObj.activeSelf)
        {
            toolTipObj.transform.position = Input.mousePosition;
        }
    }

    public void Activate(Item item)
    {
        Deactivate();
        this.item = item;
        CreateItemData();
        toolTipObj.SetActive(true);
    }

    public void CreateItemData()
    {
        string colourHex = ToRGBHex(colour);
        //string colourHexString = "<color=" + colourHex + ">";

        itemDataString = colourHex+"<b>" + item.Name + "</b></color>\n" +
            "<i>" + item.ItemType+ "</i>" + "\n"
            + item.Description + "\n" 
            + "Value" + +item.Value;

        itemText.text = itemDataString;
    }

    public static string ToRGBHex(Color c)
    {
        string rgbHex = string.Format("#{0:X2}{1:X2}{2:X2}", ToByte(c.r), ToByte(c.g), ToByte(c.b));
        return ToHTMLHexString(rgbHex);
    }

    private static byte ToByte(float f)
    {
        f = Mathf.Clamp01(f);
        return (byte)(f * 255);
    }

    public static string ToHTMLHexString(string colorHex)//set rgbHex into propper format for use
    {
        return string.Format("<color=" + colorHex + ">");
    }

    public void Deactivate()
    {
        item = null;
        toolTipObj.SetActive(false);
    }

}
