using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

    int money = 3000;
    int deliPoint = 0;

	public void UpdPlayerMoney(int updMoney)
    {
        money += updMoney;
    }
    public void UpdPlayerPoint(int updDeliPoint)
    {
        deliPoint += updDeliPoint;
    }

    public int GetPlayerMoney()
    {
        return money;
    }
    public int GetDeliPoint()
    {
        return deliPoint;
    }

    public void SetPlayerMoney(int loadMoney)
    {
        money = loadMoney;
    }
    public void SetDeliPoint(int loadDeliPoint)
    {
        deliPoint = loadDeliPoint;
    }
}
