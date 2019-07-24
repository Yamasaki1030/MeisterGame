using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuButtonGenerator : MonoBehaviour {

    [SerializeField] private Button menuButton;
    private Text returnEndBtn;
    private MyCanvas visibleMenuUI;
    private DOAnimation doAnimation;
    private FadeGenerator FadeInOut;

    [SerializeField]
    CanvasGroup UIImage;

    // Use this for initialization
    void Start()
    {
        menuButton = GetComponent<Button>();
        visibleMenuUI = GameObject.Find("FieldUI").GetComponent<MyCanvas>();
        doAnimation = GameObject.Find("DOAnimation").GetComponent<DOAnimation>();
        FadeInOut = GameObject.Find("FadeInOut").GetComponent<FadeGenerator>();
    }

    public void Onclick_GoToTitle()
    {
        MyCanvas.SetActive("Confirmation_Field", true);
        GameObject.Find("Confirmation_Field").GetComponent<ConfirmationView>().GoToTitle_Confimation();
    }

    public void OnClick_MenuMain()
    {
        StartCoroutine("GoMenuFade");
        visibleMenuUI.SetVisibleMenu("MenuUI/Main");
    }

    public void OnClick_Bag()
    {
        MyCanvas.SetActive("MenuUI/Main", false);
        MyCanvas.SetActive("MenuUI/Items", true);
        returnEndBtn = GameObject.Find("ReturnEndBtn").GetComponentInChildren<Text>();
        returnEndBtn.text = "戻る";
        visibleMenuUI.SetVisibleMenu("MenuUI/Items");
        GameObject.Find("Content").GetComponent<ScrollControll>().ItemScroll();

        doAnimation.Open_DOFade(GameObject.Find("Items/BG"));
    }

    public void OnClick_Workshop()
    {
        MyCanvas.SetActive("MenuUI/Main", false);
        MyCanvas.SetActive("MenuUI/Workshop", true);
        returnEndBtn = GameObject.Find("ReturnEndBtn").GetComponentInChildren<Text>();
        returnEndBtn.text = "戻る";
        visibleMenuUI.SetVisibleMenu("MenuUI/Workshop");
        GameObject.Find("Content").GetComponent<ScrollControll>().WorkshopScroll();

        doAnimation.Open_DOFade(GameObject.Find("Workshop/BG"));
    }

    public void OnClick_Delivery()
    {
        MyCanvas.SetActive("MenuUI/Main", false);
        MyCanvas.SetActive("MenuUI/Delivery", true);
        returnEndBtn = GameObject.Find("ReturnEndBtn").GetComponentInChildren<Text>();
        returnEndBtn.text = "戻る";
        visibleMenuUI.SetVisibleMenu("MenuUI/Delivery");
        GameObject.Find("Content").GetComponent<ScrollControll>().DeliveryScroll();

        doAnimation.Open_DOFade(GameObject.Find("Delivery/BG"));
    }

    public void OnClick_Save()
    {
        MyCanvas.SetActive("MenuUI/Confirmation", true);
        visibleMenuUI.SetVisibleMenu("MenuUI/Save");
        GameObject.Find("Confirmation").GetComponent<ConfirmationView>().Save_Confimation();
        doAnimation.Open_DOScale(GameObject.Find("Confirmation/BG"));
        //LoadData.DataSave();
    }

    public void OnClick_ReturnEnd()
    {
        
        if (!visibleMenuUI.GetVisibleMenu().Equals("MenuUI/Main"))
        {
            // 前の画面へ戻る
            returnEndBtn = GameObject.Find("ReturnEndBtn").GetComponentInChildren<Text>();
            returnEndBtn.text = "終了";

            MyCanvas.SetActive(visibleMenuUI.GetVisibleMenu(), false);
            MyCanvas.SetActive("MenuUI/Main", true);
            visibleMenuUI.SetVisibleMenu("MenuUI/Main");
        }
        else
        {
            
            // メニュー画面を閉じる
            StartCoroutine("GoFieldFade");
        }
    }

    IEnumerator GoMenuFade()
    {
        FadeInOut.fadeSpeed = 0.05f;
        FadeInOut.FadeOutInstruction();
        yield return new WaitForSeconds(0.8f);
        MyCanvas.SetActive("MenuUI", true);
        FadeInOut.FadeInInstruction();
        yield return new WaitForSeconds(1.0f);

        //returnEndBtn = GameObject.Find("ReturnEndBtn").GetComponentInChildren<Text>();
        Time.timeScale = 0f;
    }
    IEnumerator GoFieldFade()
    {
        Debug.Log(visibleMenuUI.GetVisibleMenu());
        FadeInOut.fadeSpeed = 0.05f;
        FadeInOut.FadeOutInstruction();
        yield return new WaitForSeconds(0.9f);
        MyCanvas.SetActive("MenuUI", false);
        FadeInOut.FadeInInstruction();
        yield return new WaitForSeconds(0.9f);

        //returnEndBtn = GameObject.Find("ReturnEndBtn").GetComponentInChildren<Text>();
        Time.timeScale = 1f;
    }
}
