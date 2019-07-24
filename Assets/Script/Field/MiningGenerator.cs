using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiningGenerator : MonoBehaviour {

    [SerializeField] private RectTransform btnMining;

    // 採掘可能であるか
    private bool isMining = true;

    // アイテムのデータを保持する
    Dictionary<int, string> itemInfo;
    // 採取できるアイテムの辞書
    Dictionary<int, float> itemCollectDict;


    private PlayerMoveController player;
    // プレイヤーのバッグ
    private PlayerItemManager playerBag;

    // Use this for initialization
    void Start () {
        // 採集ボタンを非表示にする
        MyCanvas.SetActive("MiningBtn", false);
        // 採集アイテムを持ち物に追加する
        playerBag = GameObject.Find("PlayerItemManager").GetComponent<PlayerItemManager>();
        // 
        player = GameObject.Find("mkmk_Preußen").GetComponent<PlayerMoveController>();

        // 各辞書の初期化
        InitializeDicts();
    }

    private void OnTriggerEnter(Collider other)
    {
        MyCanvas.SetActive("MiningBtn", true);
        player.activeMiningpoint = this.gameObject;
    }
    private void OnTriggerExit(Collider other)
    {
        MyCanvas.SetActive("MiningBtn", false);
        player.activeMiningpoint = null;
    }


    // 採集ボタンを押す
    public void OnClick()
    {
        if (isMining == true)
        {
            // アイテム抽選
            int itemId = Choose();

            string itemName = itemInfo[itemId];
            int itemQuantity = Random.Range(1, 3);
            MyCanvas.SetActive("Success_Field", true);
            GameObject.Find("DOAnimation").GetComponent<DOAnimation>().Open_DOScale(GameObject.Find("Success_Field"));
            GameObject.Find("Success_Field").GetComponent<MessageView>().Result_Mining(itemName, itemQuantity);

            playerBag.UpdateItems(itemName, itemQuantity, true);
            //isMining = false;

        }
    }

    void InitializeDicts()
    {
        // アイテムデータベースと採取率ファイルの読み込み
        // 採掘入手アイテム一覧
        itemInfo = new Dictionary<int, string>();
        itemInfo.Add(0, "石ころ");
        itemInfo.Add(1, "銅鉱石");
        itemInfo.Add(2, "銀鉱石");
        itemInfo.Add(3, "上質な銅鉱石");
        itemInfo.Add(4, "金鉱石");
        itemInfo.Add(5, "上質な銀鉱石");
        itemInfo.Add(6, "上質な金鉱石");
        itemInfo.Add(7, "パラジウム");

        // このダンジョンで入手できるアイテム
        itemCollectDict = new Dictionary<int, float>();
        itemCollectDict.Add(0, 30.0f);
        itemCollectDict.Add(1, 25.0f);
        itemCollectDict.Add(2, 15.0f);
        itemCollectDict.Add(3, 10.0f);
        itemCollectDict.Add(4, 5.0f);
        itemCollectDict.Add(5, 5.0f);
    }

    int Choose()
    {
        // 確率の合計値
        float total = 0;

        // ドロップ率の合計
        foreach (KeyValuePair<int, float> elem in itemCollectDict)
        {
            total += elem.Value;
        }

        float randomPoint = Random.value * total;

        foreach (KeyValuePair<int, float> elem in itemCollectDict)
        {
            if (randomPoint < elem.Value)
            {
                return elem.Key;
            }
            else
            {
                randomPoint -= elem.Value;
            }
        }

        return 0;
    }
}
