using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeGenerator : MonoBehaviour {

    public float fadeSpeed = 0.05f;
    float alfa;
    float red, green, blue;

	// Use this for initialization
	void Start () {
        // Panelの色を取得
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
    }

    // フェードアウト処理
    public void FadeOutInstruction()
    {
        
        StartCoroutine("FadeOut");
    }

    public IEnumerator FadeOut()
    {
        while(alfa <= 1)
        {
            GetComponent<Image>().color = new Color(red, green, blue, alfa);
            alfa += fadeSpeed;
            // 秒待つ
            yield return new WaitForSeconds(fadeSpeed * 0.4f);
        }
        
    }

    // フェードイン処理
    public void FadeInInstruction()
    {
        StartCoroutine("FadeIn");
    }

    public IEnumerator FadeIn()
    {
        while (alfa >= 0){
            GetComponent<Image>().color = new Color(red, green, blue, alfa);
            alfa -= fadeSpeed;
            yield return new WaitForSeconds(fadeSpeed * 0.4f);
        }
        
    }
}
