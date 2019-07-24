using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliNodeBtn : MonoBehaviour{

    Text[] deliNodeText;
    private DeliNodeItemQ nodeItemQ;
    private DeliveryItemManager deliItemManager;
    private MyCanvas visibleMenuUI;
    private DOAnimation doAnimation;

    private void Start()
    {
        nodeItemQ = this.transform.parent.GetComponent<DeliNodeItemQ>();
        visibleMenuUI = GameObject.Find("FieldUI").GetComponent<MyCanvas>();
    }

    public void Onclick_DeliItemPlus()
    {
        nodeItemQ.QuantityChangePlus();
    }

    public void Onclick_DeliItemMinus()
    {
        nodeItemQ.QuantityChangeMinus();
    }

    public void Onclick_DeliItemImage()
    {

    }

    public void Onclick_DeliQCheck()
    {
        doAnimation = GameObject.Find("DOAnimation").GetComponent<DOAnimation>();
        string visScroll = visibleMenuUI.GetVisibleMenu() + "/BG/Scroll View/Viewport/Content";
        GameObject content = GameObject.Find(visScroll);
        int cnt = 0;

        // 入力個数の確認
        for (int i = 0; i < content.transform.childCount; i++)
        {
            deliNodeText = content.transform.GetChild(i).GetComponentsInChildren<Text>();
            cnt += int.Parse(deliNodeText[1].text);
        }

        if(cnt == 0)
        {
            MyCanvas.SetActive("MenuUI/Warning", true);
            GameObject.Find("Warning").GetComponent<MessageView>().ItemDerivery_Warning();
            doAnimation.Open_DOScale(GameObject.Find("Warning/BG"));

        }
        else
        {
            MyCanvas.SetActive("MenuUI/Confirmation", true);
            GameObject.Find("Confirmation").GetComponent<ConfirmationView>().Deli_Confimation();
            doAnimation.Open_DOScale(GameObject.Find("Confirmation/BG"));
        }
    }

    // 納品確認
    public void ItemDelivery()
    {
        deliItemManager = GameObject.Find("DeliveryManager").GetComponent<DeliveryItemManager>();
        string visScroll = visibleMenuUI.GetVisibleMenu() + "/BG/Scroll View/Viewport/Content";
        GameObject content = GameObject.Find(visScroll);

        for (int i = 0; i < content.transform.childCount; i++)
        {
            deliNodeText = content.transform.GetChild(i).GetComponentsInChildren<Text>();
            deliItemManager.DeliExecution(deliNodeText[0].text, int.Parse(deliNodeText[1].text));
        }
    }

}
