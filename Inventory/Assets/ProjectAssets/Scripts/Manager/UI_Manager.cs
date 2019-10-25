using UnityEngine;
using System.Collections;

public class UI_Manager : MonoBehaviour {

    public KeyCode inventoryKey = KeyCode.B;
    public KeyCode equipmentKey = KeyCode.C;
    public KeyCode lootKey = KeyCode.E;

    public GameObject equipment;
    public GameObject inventory;
    public GameObject loot;

    void Update ()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            Toggle(inventory);
        }

        if (Input.GetKeyDown(equipmentKey))
        {
            Toggle(equipment);
        }

        if (Input.GetKeyDown(lootKey))
        {
            Toggle(loot);
        }
    }

    void Toggle(GameObject toToggle)
    {
        bool status = toToggle.activeInHierarchy;
        status = !status;
        toToggle.SetActive(status);
    }
}
