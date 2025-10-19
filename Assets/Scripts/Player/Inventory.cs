using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private ItemData itemData;
    private List<ItemData> Items = new List<ItemData>();
    private void Start()
    {
        Debug.Log(itemData.Icon);
        Debug.Log(itemData.ItemName);
    }
}
