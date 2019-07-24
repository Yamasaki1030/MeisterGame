using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ItemChoose : MonoBehaviour, IPointerClickHandler{

    MyCanvas visibleMenuUI;
    DetailView detailView;
    ProdView prodView;
    Text nodeText;
    DOAnimation doAnimation;

    public void OnPointerClick(PointerEventData eventData)
    {
        visibleMenuUI = GameObject.Find("FieldUI").GetComponent<MyCanvas>();
        if (visibleMenuUI.GetVisibleMenu().Equals("MenuUI/Items"))
        {
            VisDetailView();
        }
        else if (visibleMenuUI.GetVisibleMenu().Equals("MenuUI/Workshop"))
        {
            VisProdlView();
        }
    }

    private void VisDetailView()
    {
        nodeText = this.GetComponentInChildren<Text>();
        doAnimation = GameObject.Find("DOAnimation").GetComponent<DOAnimation>();
        MyCanvas.SetActive("MenuUI/ItemDetail", true);
        detailView = GameObject.Find("ItemDetail").GetComponent<DetailView>();
        detailView.ItemDetail(nodeText.text);

        doAnimation.Open_DOScale(GameObject.Find("ItemDetail/BG"));
    }

    private void VisProdlView()
    {
        nodeText = this.GetComponentInChildren<Text>();
        doAnimation = GameObject.Find("DOAnimation").GetComponent<DOAnimation>();
        MyCanvas.SetActive("MenuUI/ItemsProd", true);
        prodView = GameObject.Find("ItemsProd").GetComponent<ProdView>();
        prodView.RecipeDetail(nodeText.text);

        doAnimation.Open_DOScale(GameObject.Find("ItemsProd/BG"));
    }
}
