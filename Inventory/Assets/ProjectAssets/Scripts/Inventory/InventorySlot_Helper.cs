using UnityEngine;
using UnityEngine.UI;
//using System.Collections;
using UnityEngine.EventSystems;
using System;
//using System;

public class InventorySlot_Helper : MonoBehaviour , IBeginDragHandler ,IDragHandler ,IEndDragHandler,IPointerDownHandler,IPointerUpHandler,IPointerEnterHandler,IPointerExitHandler{

    public Item item;
    public int amount;
    public int slotId;

    public GameObject itemSlot,slotHolder,holderParent;
    public Image slotSpriteImage;
    public Image itemSpriteImage;
    public Text itemAmmountText;
    public CanvasGroup itemCanvasGroup;
    public Inventory inventory;

    //Vector2 offset;
    public bool beingDragged = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (item != null && item.ID != -1)
        {
            //offset = eventData.position - new Vector2(itemSlot.transform.position.x, itemSlot.transform.position.y);
            itemSlot.transform.SetParent(holderParent.transform);
            itemSlot.transform.position = eventData.position;
            itemCanvasGroup.blocksRaycasts = false;
        }      
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null && item.ID != -1)
        {
            inventory.draggedSlot = this; 
            beingDragged = true;
            //offset = eventData.position - new Vector2(itemSlot.transform.position.x, itemSlot.transform.position.y);
            itemSlot.transform.SetParent(holderParent.transform);
            itemSlot.transform.position = eventData.position;
            itemCanvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //if (item != null && item.ID != -1)
        //{
        itemSlot.transform.position = eventData.position;// - offset;
        //}
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!beingDragged)
        {
            Debug.Log("Wuuuut!");
        }

        inventory.draggedSlot = null;
        beingDragged = false;
        SetPositionToSlot();
    }

    //ToolTip
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(item.ID != -1 && inventory.draggedSlot == null)
        {
            inventory.toolTip.Activate(item);
        }       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventory.toolTip.Deactivate();

        if (item.ID != -1)
        {
            if (beingDragged == false)
            {
                SetPositionToSlot();
            }          
        }
    }

    public void SetPositionToSlot()
    {
        if (inventory != null)
        {
            itemCanvasGroup.blocksRaycasts = true;

            itemSlot.transform.SetParent(inventory.inventorySlots[slotId].transform);//slotHolder.transform);
            itemSlot.transform.position = inventory.inventorySlots[slotId].transform.position;
        }     
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (item.ItemType == ItemType.Consumable && beingDragged == false)
        {
            Invoke("Consume", 0.1f);
           
        }

    }

    void Consume()
    {
        Consumable_Item consumeItem = (Consumable_Item)item;
        consumeItem.Consume();

        inventory.RemoveItem(inventory.items, item);
        Debug.Log("Consumed " + item.Name);
    }
}
