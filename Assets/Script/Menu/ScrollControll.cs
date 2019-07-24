using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollControll : MonoBehaviour {

    [SerializeField] RectTransform Prefab = null;
    private PlayerItemManager itemManager;
    private DeliveryItemManager deliItemManager;
    private Dictionary<Item, int> scrollItems;
    private Dictionary<DeliveryItem, int> scrollDeli;
    [SerializeField] FileReader fireReader;
    private List<string> recipeNameList;


    // スクロールバーの初期化
    private void Scroll_Initialize()
    {
        GameObject content = GameObject.Find("Content");

        for (int i = 0; i < content.transform.childCount; i++)
        {
            // メモ：transformは削除できない
            Destroy(content.transform.GetChild(i).gameObject);
        }
    }

    public void ItemScroll()
    {
        itemManager = GameObject.Find("PlayerItemManager").GetComponent<PlayerItemManager>();
        scrollItems = itemManager.GetPlayerItems();

        Scroll_Initialize();
        foreach (KeyValuePair<Item, int> pair in scrollItems)
        {
            RectTransform item = GameObject.Instantiate(Prefab) as RectTransform;
            item.SetParent(transform, false);
            string itemName = itemManager.GetItemNameStr(pair.Key);

            Text[] nodePrefab = item.GetComponentsInChildren<Text>();
            nodePrefab[0].text = itemName;
        }
    }

    public void WorkshopScroll()
    {
        fireReader = GameObject.Find("FileReader").GetComponent<FileReader>();
        recipeNameList = fireReader.GetResipeDatasIndex();

        Scroll_Initialize();
        for (int i = 0; i < recipeNameList.Count; i++)
        {
            RectTransform node = GameObject.Instantiate(Prefab) as RectTransform;
            node.SetParent(transform, false);

            Text[] nodePrefab = node.GetComponentsInChildren<Text>();
            nodePrefab[0].text = recipeNameList[i];
        }
    }

    public void DeliveryScroll()
    {
        deliItemManager = GameObject.Find("DeliveryManager").GetComponent<DeliveryItemManager>();
        scrollDeli = deliItemManager.GetDeliveryItems();

        Scroll_Initialize();
        foreach (KeyValuePair<DeliveryItem, int> pair in scrollDeli)
        {
            RectTransform item = GameObject.Instantiate(Prefab) as RectTransform;
            item.SetParent(transform, false);
            string itemName = deliItemManager.GetItemNameStr(pair.Key);

            Text[] nodePrefab = item.GetComponentsInChildren<Text>();
            nodePrefab[0].text = itemName;
        }
    }
}
