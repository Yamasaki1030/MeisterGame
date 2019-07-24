using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "DeliItem", menuName = "CreateDeliItem")]
public class DeliveryItem : ScriptableObject{

    // アイテムの分類・アイコン・名前・説明
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private string itemName;
    [SerializeField]
    private string information;
    [SerializeField]
    private int price;
    [SerializeField]
    private int deliPoint;


    public Sprite GetIcon()
    {
        return icon;
    }

    public string GetItemName()
    {
        return itemName;
    }

    public string GetInformation()
    {
        return information;
    }

    public int GetPrice()
    {
        return price;
    }

    public int GetDeliPoint()
    {
        return deliPoint;
    }
}
