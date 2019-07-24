using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliNodeItemQ : MonoBehaviour {

    private DeliveryItemManager deliItemManager;

    Text[] detailText;

    // アイテム作成数関連
    private int createQuantity;
    private int maxCreateQ;

    private void Start()
    {
        deliItemManager = GameObject.Find("DeliveryManager").GetComponent<DeliveryItemManager>();
        detailText = this.gameObject.GetComponentsInChildren<Text>();
        detailText[1].text = "1";

    }



    public void QuantityChangePlus()
    {
        //detailText = this.GetComponentsInChildren<Text>();
        maxCreateQ = deliItemManager.GetStock(detailText[0].text);

        if ((createQuantity == maxCreateQ) || (createQuantity == 99))
        {

        }
        else
        {
            // 作製数に+1
            createQuantity++;
        }
        detailText[1].text = createQuantity.ToString();
    }

    public void QuantityChangeMinus()
    {
        //detailText = this.GetComponentsInChildren<Text>();
        if (createQuantity > 1)
        {
            // 作製数に-1
            createQuantity--;
        }
        detailText[1].text = createQuantity.ToString();
    }
}
