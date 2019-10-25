using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System;

public class LootSlot_Helper : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler{

    public ItemDropBox box;
    public Item item = new Item();
    public int amount = 1;
    public Image icon;
    public Text itemName;
    public Text itemAmount;

	public void SetUp (Item newItem,ItemDropBox newBox)
    {
        box = newBox;
        item = newItem;
        icon.sprite = newItem.Icon;
        itemName.text = newItem.Name;

        this.gameObject.name = newItem.Name;
	}

    public void SetAmmountText (int newAmount)
    {
        if (!itemAmount.gameObject.activeInHierarchy && amount >1)
        {
            itemAmount.gameObject.SetActive(true);
        }
        amount = newAmount;
        itemAmount.text = newAmount.ToString();
	}

    //ToolTip
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.LogError("OnPointerEnter");

        if (item.ID != -1)
        {
            box.inventory.toolTip.Activate(item);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.LogError("OnPointerClick");

        if (!box.inventory.inventoryFull)
        {
            if (item.Stackable && amount > 1)
            {
                for (int i = 0; i < amount; i++)
                {
                    if (box.inventory.inventoryFull)
                    {
                        Debug.LogWarning("Inventory Full");
                        return;
                    }

                    box.inventory.Add_Item(item);
                    box.items.Remove(item);
                    box.ItemRemoved();
                }
                //amount--;
                //SetAmmountText(amount);
                //box.inventory.Add_Item(item);
                Destroy(this.gameObject);
                return;
            }

            box.items.Remove(item);

            box.inventory.Add_Item(item);
            box.ItemRemoved();
            Destroy(this.gameObject);
            box.inventory.toolTip.Deactivate();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        box.inventory.toolTip.Deactivate();
    }
}
