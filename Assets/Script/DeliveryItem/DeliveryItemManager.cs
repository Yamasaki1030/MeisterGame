using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryItemManager : MonoBehaviour {

    // 納品アイテムデータベース
    [SerializeField]
    private DeliItemDataBase deliItemDataBase;
    // 納品アイテム数管理
    private Dictionary<DeliveryItem, int> numOfDeliItem = new Dictionary<DeliveryItem, int>();

    private DeliNodeItemQ deliItemQ;
    // ステータス（仮）
    private PlayerData playerData;

    void Start()
    {
        playerData = GameObject.Find("mkmk_Preußen").GetComponent<PlayerData>();
        // アイテム個数の初期化
        for (int i = 0; i < deliItemDataBase.GetDeliItemLists().Count; i++)
        {
            numOfDeliItem.Add(deliItemDataBase.GetDeliItemLists()[i], 10);
        }
    }
    // 所持アイテム情報を渡す
    public Dictionary<DeliveryItem, int> GetDeliveryItems()
    {
        return numOfDeliItem;
    }
    // 所持アイテム情報の読み込み
    public void SetLoadDeliItems(Dictionary<DeliveryItem, int> loadDeliItems)
    {
        numOfDeliItem = loadDeliItems;
    }

    // 対象アイテムの所持数を渡す
    public int GetStock(string searchItemName)
    {
        return numOfDeliItem[GetItemsCode(searchItemName)];
    }

    // アイテム納品
    public void DeliExecution(string deliItemName, int deliItemQuantity)
    {

        UpdateDeliItems(deliItemName, deliItemQuantity, false);
        // 所持金・ポイントの更新
        DeliveryItem deliItem = GetItemsCode(deliItemName);
        int money = deliItem.GetPrice() * deliItemQuantity;
        int deliPoint = deliItem.GetDeliPoint() * deliItemQuantity;
        // 所持金とポイントの更新
        playerData.UpdPlayerMoney(money);
        playerData.UpdPlayerPoint(deliPoint);
    }

    // アイテムの追加・消費
    public void UpdateDeliItems(string updItemName, int updItemQuantity, bool isAdd)
    {
        int positionQuantity = 0;
        if (isAdd)
        {
            // 追加
            positionQuantity = numOfDeliItem[GetItemsCode(updItemName)] + updItemQuantity;
        }
        else
        {
            // 消費
            positionQuantity = numOfDeliItem[GetItemsCode(updItemName)] - updItemQuantity;
        }
        numOfDeliItem[GetItemsCode(updItemName)] = positionQuantity;
    }

    // アイテム本体を探す
    public DeliveryItem GetItemsCode(string searchItemName)
    {
        return deliItemDataBase.GetDeliItemLists().Find(itemName => itemName.GetItemName() == searchItemName);
    }
    // アイテム名を探す
    public string GetItemNameStr(DeliveryItem searchItemCode)
    {
        int result = deliItemDataBase.GetDeliItemLists().IndexOf(searchItemCode);
        return deliItemDataBase.GetDeliItemLists()[result].GetItemName();
    }
}
