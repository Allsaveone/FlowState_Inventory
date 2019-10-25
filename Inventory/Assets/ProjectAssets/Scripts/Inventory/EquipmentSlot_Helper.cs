using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class EquipmentSlot_Helper : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler{

    public GameObject equipSlot, holderParent;
    public CanvasGroup itemCanvasGroup;
    public bool beingDragged = false;

    //Vector2 offset;
    EquipmentSlot eSlot;
    Equipment equipment;

    void Start()
    {
        eSlot = equipSlot.GetComponentInParent<EquipmentSlot>();
        equipment = eSlot.equipment;
    }

    public void OnPointerDown(PointerEventData eventData)//Picks up item to cursor
    {
        if (eSlot.equipedItem != null && eSlot.equipedItem.ID != -1)
        {
            //offset = eventData.position - new Vector2(equipSlot.transform.position.x, equipSlot.transform.position.y);
            equipSlot.transform.SetParent(holderParent.transform);
            equipSlot.transform.position = eventData.position;
            itemCanvasGroup.blocksRaycasts = false;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eSlot.equipedItem != null && eSlot.equipedItem.ID != -1)
        {
            equipment.draggedSlot = this;
            beingDragged = true;

            //offset = eventData.position - new Vector2(equipSlot.transform.position.x, equipSlot.transform.position.y);
            equipSlot.transform.SetParent(holderParent.transform);
            equipSlot.transform.position = eventData.position;
            itemCanvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //if (eSlot.equipedItem != null && eSlot.equipedItem.ID != -1)
        //{
        equipSlot.transform.position = eventData.position;// - offset;
        //}
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        equipment.draggedSlot = null;
        beingDragged = false;
        SetPositionToSlot();
    }

    //ToolTip
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eSlot.equipedItem.ID != -1 && equipment.draggedSlot == null && equipment.inventory.draggedSlot == null)
        {
            equipment.inventory.toolTip.Activate(eSlot.equipedItem);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        equipment.inventory.toolTip.Deactivate();

        if (eSlot.equipedItem.ID != -1)
        {
            if (beingDragged == false)
            {
                SetPositionToSlot();
            }
        }
    }

    void SetPositionToSlot()
    {
        if (equipment != null)
        {
            itemCanvasGroup.blocksRaycasts = true;

            transform.SetParent(eSlot.transform);//slotHolder.transform);
            transform.position = eSlot.transform.position;
        }
    }

    public void DragDropFromEquiped(InventorySlot_Helper helper)
    {
        //Debug.Log(helper.item.Name);
        //

        if (!equipment.inventory.inventoryFull)// && helper.item.ItemType != eSlot.equipedItem.ItemType)
        {
            if (helper.item.ItemType == eSlot.equipedItem.ItemType)
            {
                eSlot.EquipItem(helper.item);
                equipment.inventory.toolTip.Deactivate();
                equipment.inventory.toolTip.Activate(eSlot.equipedItem);

            }
            else
            {
                equipment.inventory.toolTip.Deactivate();
                eSlot.SetEquiped(false);
            }
        }
    }
}
