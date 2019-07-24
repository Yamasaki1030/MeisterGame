using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : ScriptableObject
{

    private static GameObject player;
    private static PlayerData playerData;
    private static PlayerItemManager itemManager;
    private static DeliveryItemManager deliItemManager;
    private static Dictionary<Item, int> playerItemsList;
    private static Dictionary<DeliveryItem, int> deliItemsList;

    // ロード機能
    public static void DataLoad()
    {
        player = GameObject.Find("mkmk_Preußen");
        playerData = player.GetComponent<PlayerData>();
        itemManager = GameObject.Find("PlayerItemManager").GetComponent<PlayerItemManager>();
        deliItemManager = GameObject.Find("DeliveryManager").GetComponent<DeliveryItemManager>();

        // 位置情報
        float posX = PlayerPrefs.GetFloat("playerPos.x", 40);
        float posY = PlayerPrefs.GetFloat("playerPos.y", 0);
        float posZ = PlayerPrefs.GetFloat("playerPos.z", 40);
        //playerRotX = PlayerPrefs.GetFloat("playerRot.x");
        Vector3 loadPlayerPos = new Vector3(posX, posY, posZ);
        player.transform.position = loadPlayerPos;

        playerData.SetPlayerMoney(PlayerPrefs.GetInt("Money", 3000));
        playerData.SetDeliPoint(PlayerPrefs.GetInt("deliPoint", 0));

        // 通常アイテム情報(初期化含む)
        playerItemsList = itemManager.GetPlayerItems();
        List<Item> keyList1 = new List<Item>(playerItemsList.Keys);
        foreach (Item pair in keyList1)
        {
            string itemName = itemManager.GetItemNameStr(pair);
            playerItemsList[pair] = PlayerPrefs.GetInt(itemName, 0);
        }
        // 納品アイテム情報(初期化含む)
        deliItemsList = deliItemManager.GetDeliveryItems();
        List<DeliveryItem> keyList2 = new List<DeliveryItem>(deliItemsList.Keys);
        foreach (DeliveryItem pair2 in keyList2)
        {
            string itemName = deliItemManager.GetItemNameStr(pair2);
            deliItemsList[pair2] = PlayerPrefs.GetInt(itemName, 0);
        }

        player.GetComponent<PlayerMoveController>().SetPlayerData(player);
        itemManager.SetLoadItems(playerItemsList);
        deliItemManager.SetLoadDeliItems(deliItemsList);

    }

    // セーブ機能
    public static void DataSave()
    {
        player = GameObject.Find("mkmk_Preußen");
        playerData = player.GetComponent<PlayerData>();
        itemManager = GameObject.Find("PlayerItemManager").GetComponent<PlayerItemManager>();
        deliItemManager = GameObject.Find("DeliveryManager").GetComponent<DeliveryItemManager>();
        playerItemsList = itemManager.GetPlayerItems();
        deliItemsList = deliItemManager.GetDeliveryItems();

        PlayerPrefs.SetFloat("playerPos.x", player.transform.position.x);
        PlayerPrefs.SetFloat("playerPos.y", player.transform.position.y);
        PlayerPrefs.SetFloat("playerPos.z", player.transform.position.z);
        //PlayerPrefs.SetFloat("playerRot.x", playerData.transform.rotation.x);

        PlayerPrefs.SetInt("Money", playerData.GetPlayerMoney());
        PlayerPrefs.SetInt("deliPoint", playerData.GetDeliPoint());

        foreach (KeyValuePair<Item, int> pair in playerItemsList)
        {
            PlayerPrefs.SetInt(pair.Key.ToString(), pair.Value);
        }
        foreach (KeyValuePair<DeliveryItem, int> pair in deliItemsList)
        {
            PlayerPrefs.SetInt(pair.Key.ToString(), pair.Value);
        }
    }

}
