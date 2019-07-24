using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DOAnimation : MonoBehaviour {

    [SerializeField]
    RectTransform rectTran;
    [SerializeField]
    CanvasGroup UICanvasGrp;
    // 現在最前列の画面
    string visViewer;

// メッセージ画面等で使用
    public void Open_DOScale(GameObject gameObj)
    {
        rectTran = gameObj.GetComponent<RectTransform>();
        rectTran.localScale = new Vector3(1, 1, 1);
        rectTran.DOScale(new Vector3(0.2f, 0.2f), 0.3f).From().SetUpdate(true).SetEase(Ease.OutBack);
    }
    public void Close_DOScale(GameObject gameObj)
    {
        rectTran = gameObj.GetComponent<RectTransform>();

        if (gameObj.transform.root.name.Equals(gameObj.transform.parent.transform.parent.name))
        {
            visViewer = gameObj.transform.parent.name;
        }
        else
        {
            visViewer = "MenuUI/" + gameObj.transform.parent.name;
        }
        
        rectTran.localScale = new Vector3(1, 1, 1);
        rectTran.DOScale(new Vector3(0.2f, 0.2f), 0.3f).SetUpdate(true).SetEase(Ease.InBack).OnComplete(AA);
    }
    void AA()
    {
        MyCanvas.SetActive(visViewer, false);
    }


    // スクロール画面で使用
    public void Open_DOFade(GameObject gameObj)
    {
        UICanvasGrp = gameObj.GetComponent<CanvasGroup>();
        UICanvasGrp.DOFade(0, 0.5f).From().SetUpdate(true);
    }
    public void Close_DOFade(GameObject gameObj)
    {
        UICanvasGrp = gameObj.GetComponent<CanvasGroup>();
        UICanvasGrp.DOFade(0, 0.5f).SetUpdate(true);
    }

    // フィールド画面で使用
    public void InOut_DOFade(bool isGoToMenu)
    {
        // コルーチンで他の入力を遮断して実行
        Image fadeImage = GameObject.Find("FadeInOut").GetComponent<Image>();
        //bool isFadeOut = false;
        var c = fadeImage.color;
        fadeImage.color = c;

        // フェードアウト
        Sequence tileSeq = DOTween.Sequence();

        tileSeq.Append(DOTween.ToAlpha(() => fadeImage.color, color => fadeImage.color = color, 1f, 1f));
        tileSeq.Append(DOTween.ToAlpha(() => fadeImage.color, color => fadeImage.color = color, 0f, 1f));
        tileSeq.Play();
    }

    private void Gamen_Kirikae()
    {
        MyCanvas.SetActive("Menu/Main", true);
    }
}
