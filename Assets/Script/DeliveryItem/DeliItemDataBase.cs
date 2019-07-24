using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeliItemDataBase", menuName = "CreateDeliItemDataBase")]
public class DeliItemDataBase : ScriptableObject {

    [SerializeField]
    private List<DeliveryItem> deliItemLists = new List<DeliveryItem>();

    // アイテムリストを返す
    public List<DeliveryItem> GetDeliItemLists()
    {
        return deliItemLists;
    }
}
