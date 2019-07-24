using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DetailView : MonoBehaviour {

    private PlayerItemManager itemManager;
    // アイテムデータベース
    [SerializeField]
    private ItemDataBase itemDataBase;
    Text[] detailText;
    private DOAnimation doAnimation;


    // アイテム詳細
    public void ItemDetail(string itemNameData)
    {
        detailText = this.GetComponentsInChildren<Text>();
        itemManager = GameObject.Find("PlayerItemManager").GetComponent<PlayerItemManager>();
        Item itemDetail = itemManager.GetItemsCode(itemNameData);
        detailText[1].text = itemDetail.GetItemName();
        detailText[2].text = itemDetail.GetInformation();
    }

    public void Onclick_Close()
    {
        doAnimation = GameObject.Find("DOAnimation").GetComponent<DOAnimation>();
        doAnimation.Close_DOScale(GameObject.Find("ItemDetail/BG"));
    }
}
