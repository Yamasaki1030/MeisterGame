using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyCanvas : MonoBehaviour {

    public static Canvas _canvas;
    public string visibleUI;

    // Use this for initialization
    void Awake () {
        _canvas = GetComponent<Canvas>();
	}
	
	public static void SetActive(string name, bool b)
    {
        Transform menuUIVis =  _canvas.transform.Find(name);
        menuUIVis.gameObject.SetActive(b);
        return;
    
    }

    // 現在表示しているメニュー項目
    public void SetVisibleMenu(string visMenuName)
    {
        visibleUI = visMenuName;
    }
    public string GetVisibleMenu()
    {
        return visibleUI;
    }
}
