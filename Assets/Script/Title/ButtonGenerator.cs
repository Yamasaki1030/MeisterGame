using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonGenerator : MonoBehaviour {

    [SerializeField] private Button gamePlayButton;

    // Use this for initialization
    void Start()
    {
        gamePlayButton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickSub()
    {
        if (gamePlayButton.tag == "GameStart")
        {
            SceneManager.LoadScene("FieldScene");
        } 
        else
        {
            // セーブデータ消去
        }
    }


}