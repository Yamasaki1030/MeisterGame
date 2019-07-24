using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemManager : MonoBehaviour {

    // アイテムデータベース
    [SerializeField]
    private ItemDataBase itemDataBase;
    // アイテム数管理
    private Dictionary<Item, int> numOfItem = new Dictionary<Item, int>();

    //private List<String> itemName = new List<String>();
    //private List<String> itemQuantity = new List<String>();

    // レシピデータ
    [SerializeField] FileReader fireReader;
    private List<string[]> recipeList;

    // 納品アイテム関連
    DeliveryItemManager deliItemManager;


    // Use this for initialization
    void Start()
    {
        // アイテム個数の初期化
        newItems();
    }

    // 対象アイテムの所持数を渡す
    public int GetStock(string searchItemName)
    {
        return numOfItem[GetItemsCode(searchItemName)];
    }

    // アイテムの追加・消費
    public void UpdateItems(string updItemName, int updItemQuantity, bool isAdd)
    {
        int positionQuantity = 0;
        Debug.Log(updItemName + "/" + updItemQuantity);
        if(isAdd)
        {
            // 追加
            positionQuantity = numOfItem[GetItemsCode(updItemName)] + updItemQuantity;
        } else
        {
            // 消費
            positionQuantity = numOfItem[GetItemsCode(updItemName)] - updItemQuantity;
        }
        numOfItem[GetItemsCode(updItemName)] = positionQuantity;
    }

    // アイテム加工
    public void ItemProd(string prodItemName, int prodItemQuantity)
    {
        fireReader = GameObject.Find("FileReader").GetComponent<FileReader>();
        recipeList = fireReader.GetResipeDatas();
        int result = fireReader.GetResipeDatasIndex().IndexOf(prodItemName);
        deliItemManager = GameObject.Find("DeliveryManager").GetComponent<DeliveryItemManager>();

        for (int recipeColumn = 2; recipeColumn < recipeList[result].Length; recipeColumn = recipeColumn + 2)
        {
            // 1種類ずつ消費
            if (recipeList[result][recipeColumn] != "")
            {
                
                string itemName = recipeList[result][recipeColumn];
                int itemQuantity = int.Parse(recipeList[result][recipeColumn + 1]);
                UpdateItems(itemName, itemQuantity * prodItemQuantity, false);
            }
        }
        // 納品アイテムへの追加
        if(prodItemName.Equals("ピンクゴールド ×10") || prodItemName.Equals("ホワイトゴールド ×10"))
        {
            prodItemName = prodItemName.Substring(0, (prodItemName.Length - 4));
            prodItemQuantity = prodItemQuantity * 10;
            Debug.Log(prodItemName);
            UpdateItems(prodItemName, prodItemQuantity, true);
        }
        else
        {
            deliItemManager.UpdateDeliItems(prodItemName, prodItemQuantity, true);
        }
        
    }

    // 所持アイテム情報を渡す
    public Dictionary<Item, int> GetPlayerItems()
    {
        return numOfItem;
    }
    // 所持アイテム情報の読み込み
    public void SetLoadItems(Dictionary<Item, int> loadItems)
    {
        numOfItem = loadItems;
    }
    // 所持アイテム情報の初期化(new game)
    public void newItems()
    {
        for (int i = 0; i < itemDataBase.GetItemLists().Count; i++)
        {
            numOfItem.Add(itemDataBase.GetItemLists()[i], 0);
        }
    }

    // アイテム本体を探す
    public Item GetItemsCode(string searchItemName)
    {
        return itemDataBase.GetItemLists().Find(itemName => itemName.GetItemName() == searchItemName);
    }
    // アイテム名を探す
    public string GetItemNameStr(Item searchItemCode)
    {
        int result = itemDataBase.GetItemLists().IndexOf(searchItemCode);
        return itemDataBase.GetItemLists()[result].GetItemName();
    }
}
