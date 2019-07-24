using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfirmationView : MonoBehaviour {

    private MyCanvas visibleMenuUI;
    private MessageView deliSuccessView;
    private DOAnimation doAnimation;

    public void Deli_Confimation()
    {
        Text[] viewerText = this.GetComponentsInChildren<Text>();
        viewerText[1].text = "納品しますか？";
    }

    public void Save_Confimation()
    {
        Text[] viewerText = this.GetComponentsInChildren<Text>();
        viewerText[1].text = "セーブしますか？";
    }

    public void Onlick_Yes()
    {
        visibleMenuUI = GameObject.Find("FieldUI").GetComponent<MyCanvas>();
        doAnimation = GameObject.Find("DOAnimation").GetComponent<DOAnimation>();
        MyCanvas.SetActive("MenuUI/Confirmation", false);

        if (visibleMenuUI.GetVisibleMenu().Equals("MenuUI/Delivery"))
        {
            DeliConf();
        }
        else if (visibleMenuUI.GetVisibleMenu().Equals("MenuUI/Save"))
        {
            SaveConf();
        }
    }

    private void DeliConf()
    {
        GameObject.Find("Delivery/ConfBtn").GetComponent<DeliNodeBtn>().ItemDelivery();
        MyCanvas.SetActive("MenuUI/DeliSuccess", true);
        GameObject.Find("DeliSuccess").GetComponent<MessageView>().ItemDelivery_Success();
        doAnimation.Open_DOScale(GameObject.Find("DeliSuccess/BG"));
    }

    private void SaveConf()
    {
        //LoadData.DataSave();
        MyCanvas.SetActive("MenuUI/Success", true);
        GameObject.Find("Success").GetComponent<MessageView>().Save_Success();
        doAnimation.Open_DOScale(GameObject.Find("Success/BG"));
    }

    public void Onlick_No()
    {
        visibleMenuUI = GameObject.Find("FieldUI").GetComponent<MyCanvas>();
        doAnimation = GameObject.Find("DOAnimation").GetComponent<DOAnimation>();
        doAnimation.Close_DOScale(GameObject.Find("Confirmation/BG"));

        if (visibleMenuUI.GetVisibleMenu().Equals("MenuUI/Save"))
        {
            visibleMenuUI.SetVisibleMenu("MenuUI/Main");
        }
    }

    // フィールドで使用
    public void GoToTitle_Confimation()
    {
        Text[] viewerText = this.GetComponentsInChildren<Text>();
        viewerText[1].text = "タイトルに戻りますか？";
    }

    public void Onclick_Yes_Field()
    {
        LoadData.DataSave();
        MyCanvas.SetActive("Confirmation_Field", false);
        SceneManager.LoadScene("TitleScene");
    }

    public void Onclick_No_Field()
    {
        doAnimation = GameObject.Find("DOAnimation").GetComponent<DOAnimation>();
        doAnimation.Close_DOScale(GameObject.Find("Confirmation_Field/BG"));
    }
}
