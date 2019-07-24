using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageView : MonoBehaviour {

    Text[] sucViewText;
    private MyCanvas visibleMenuUI;
    private PlayerData playerData;
    private DOAnimation doAnimation;

    public void ItemProd_Success()
    {
        sucViewText = this.GetComponentsInChildren<Text>();
        sucViewText[1].text = "アイテムを作成しました。";

    }

    public void ItemDerivery_Warning()
    {
        sucViewText = this.GetComponentsInChildren<Text>();
        sucViewText[1].text = "アイテムの個数を入力してください。";
    }

    public void ItemDelivery_Success()
    {
        sucViewText = this.GetComponentsInChildren<Text>();
        playerData = GameObject.Find("mkmk_Preußen").GetComponent<PlayerData>();
        sucViewText[1].text = "アイテムを納品しました。";
        sucViewText[2].text = playerData.GetPlayerMoney().ToString() + " マルク";
        sucViewText[3].text = playerData.GetDeliPoint().ToString() + " pt";

    }

    public void Save_Success()
    {
        sucViewText = this.GetComponentsInChildren<Text>();
        sucViewText[1].text = "セーブしました。";

    }

    public void Onclick_SuccessOK()
    {
        visibleMenuUI = GameObject.Find("FieldUI").GetComponent<MyCanvas>();
        doAnimation = GameObject.Find("DOAnimation").GetComponent<DOAnimation>();
        

        if (visibleMenuUI.GetVisibleMenu().Equals("MenuUI/ItemsProd"))
        {
            doAnimation.Close_DOScale(GameObject.Find("Success/BG"));
            ProdView prodView = GameObject.Find("ItemsProd").GetComponent<ProdView>();
            Text[] detailText = GameObject.Find("ItemsProd").GetComponentsInChildren<Text>();
            prodView.RecipeDetail(detailText[2].text);
        }
        else if (visibleMenuUI.GetVisibleMenu().Equals("MenuUI/Delivery"))
        {
            doAnimation.Close_DOScale(GameObject.Find("DeliSuccess/BG"));
            GameObject.Find("Content").GetComponent<ScrollControll>().DeliveryScroll();
        }
        else if (visibleMenuUI.GetVisibleMenu().Equals("MenuUI/Save"))
        {
            doAnimation.Close_DOScale(GameObject.Find("Success/BG"));
            visibleMenuUI.SetVisibleMenu("MenuUI/Main");
        }

            
    }

    public void Onclick_Deli_WarningOK()
    {
        doAnimation = GameObject.Find("DOAnimation").GetComponent<DOAnimation>();
        doAnimation.Close_DOScale(GameObject.Find("Warning/BG"));
    }

    // フィールド画面で使用
    public void Result_Mining(string miningItemName, int miningItemQ)
    {
        sucViewText = this.GetComponentsInChildren<Text>();
        sucViewText[1].text = miningItemName + "を" + miningItemQ.ToString() + "個採取しました。";
    }
    public void Onclick_OK_Field()
    {
        doAnimation = GameObject.Find("DOAnimation").GetComponent<DOAnimation>();
        doAnimation.Close_DOScale(GameObject.Find("Success_Field/BG"));
    }

}
