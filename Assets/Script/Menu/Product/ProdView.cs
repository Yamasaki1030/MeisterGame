using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProdView : MonoBehaviour {

    private PlayerItemManager itemManager;
    // アイテムデータベース
    [SerializeField]
    private ItemDataBase itemDataBase;
    // レシピデータ
    [SerializeField] FileReader fireReader;
    private List<string[]> recipeList;

    private Text[] detailText;

    // アイテム作成数関連
    private int createQuantity;
    private int maxCreateQ;


    // 作成用画面の表示
    public void RecipeDetail(string searchRecipeName)
    {
        itemManager = GameObject.Find("PlayerItemManager").GetComponent<PlayerItemManager>();
        fireReader = GameObject.Find("FileReader").GetComponent<FileReader>();
        detailText = this.GetComponentsInChildren<Text>();
        Button prodBtn = GameObject.Find("ItemsProd/BG/ProdBtn").GetComponent<Button>();



        recipeList = fireReader.GetResipeDatas();
        int result = fireReader.GetResipeDatasIndex().IndexOf(searchRecipeName);
        ItemProd_Initialize();

        detailText[2].text = recipeList[result][0];
        detailText[10].text = createQuantity.ToString();
        detailText[11].text = "アイテムを作りますか？";
        prodBtn.interactable = true;


        for (int recipeColumn = 2; recipeColumn < recipeList[result].Length; recipeColumn = recipeColumn + 2)
        {
            if (recipeList[result][recipeColumn] != "")
            {
                string itemName = recipeList[result][recipeColumn];
                int itemQuantity = int.Parse(recipeList[result][recipeColumn + 1]);
                SetMaxQuantity(itemName, itemQuantity);

                // 在庫確認
                if (itemManager.GetStock(itemName) < itemQuantity)
                {
                    detailText[10].text = "0";
                    detailText[11].text = "アイテムが足りません";
                    // 該当アイテムの文字色変更
                    // detailText[recipeColumn + 2].text
                    // detailText[recipeColumn + 2].text
                    // 「作製」ボタン、個数増減ボタン無効化
                    prodBtn.interactable = false;
                }

                detailText[recipeColumn + 2].text = recipeList[result][recipeColumn];       // 必要アイテム名称
                detailText[recipeColumn + 3].text = "× " + recipeList[result][recipeColumn + 1];   // 必要アイテムの個数
            }
            else
            {
                detailText[recipeColumn + 2].text = "";
                detailText[recipeColumn + 3].text = "";
            }
        }

    }

    // 作成アイテム数の増減と表示
    private void SetMaxQuantity(string itemName, int itemQuantity)
    {
        int num = itemManager.GetStock(itemName) / itemQuantity;
        if (num < maxCreateQ)
        {
            maxCreateQ = num;
        }
    }

    public void QuantityChangePlus()
    {
        detailText = this.GetComponentsInChildren<Text>();

        if ((createQuantity == maxCreateQ) || (createQuantity == 99))
        {

        }
        else
        {
            // 作製数に+1
            createQuantity++;
        }
        detailText[10].text = createQuantity.ToString();
    }

    public void QuantityChangeMinus()
    {
        detailText = this.GetComponentsInChildren<Text>();
        if (createQuantity > 1)
        {
            // 作製数に-1
            createQuantity--;
        }
        detailText[10].text = createQuantity.ToString();
    }

    // アイテム作成
    public void ItemProd()
    {
        itemManager = GameObject.Find("PlayerItemManager").GetComponent<PlayerItemManager>();
        detailText = this.GetComponentsInChildren<Text>();

        itemManager.ItemProd(detailText[2].text, int.Parse(detailText[10].text)); // detailText[2].text
        ItemProd_Initialize();
    }

    public void ViewerClose()
    {
        ItemProd_Initialize();
        MyCanvas.SetActive("MenuUI/ItemsProd", false);
    }

    private void ItemProd_Initialize()
    {
        createQuantity = 1;
        maxCreateQ = 99;
    }

}