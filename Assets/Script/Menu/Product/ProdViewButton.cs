using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ProdViewButton : MonoBehaviour {

    public ProdView prodView;
    public MessageView successView;
    private DOAnimation doAnimation;

    public void Onclick_QuantityChangePlus()
    {
        prodView = GameObject.Find("ItemsProd").GetComponent<ProdView>();
        prodView.QuantityChangePlus();
    }

    public void Onclick_QuantityChangeMinus()
    {
        prodView = GameObject.Find("ItemsProd").GetComponent<ProdView>();
        prodView.QuantityChangeMinus();
    }

    public void OnClick_ItemProd()
    {
        prodView = GameObject.Find("ItemsProd").GetComponent<ProdView>();
        prodView.ItemProd();

        MyCanvas.SetActive("MenuUI/Success", true);
        successView = GameObject.Find("Success").GetComponent<MessageView>();
        doAnimation = GameObject.Find("DOAnimation").GetComponent<DOAnimation>();
        successView.ItemProd_Success();

        doAnimation.Open_DOScale(GameObject.Find("Success/BG"));
    }

    public void OnClick_Close()
    {
        doAnimation = GameObject.Find("DOAnimation").GetComponent<DOAnimation>();
        doAnimation.Close_DOScale(GameObject.Find("ItemsProd/BG"));
        prodView = GameObject.Find("ItemsProd").GetComponent<ProdView>();
        prodView.ViewerClose();

        MyCanvas.SetActive("MenuUI/ItemsProd", false);
    }
}
