using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UserData", menuName = "ScriptableObjects/UserData", order = 1)]
public class UserData : ScriptableObject
{
    private static UserData ins;
    public static UserData Ins
    {
        get
        {
            if (ins == null)
            {
                UserData[] datas = Resources.LoadAll<UserData>("");

                if (datas.Length == 1)
                {
                    ins = datas[0];
                }
                else
                if (datas.Length == 0)
                {
                    Debug.LogError("Can find Scriptableobject UserData");
                }
                else
                {
                    Debug.LogError("have multiple Scriptableobject UserData");
                }
            }

            return ins;
        }
    }

    public const string Key_Coin = "Coin";
    public const string Key_Distance = "Distance";
    //public int coin = 0;

    // Lưu số coin vào PlayerPrefs
    public void SaveInfo(int coins, float distance)
    {
        // coin
        int saveCoin = PlayerPrefs.GetInt(Key_Coin);
        PlayerPrefs.SetInt(Key_Coin, saveCoin + coins);
        

        // distance
        float saveDistance = PlayerPrefs.GetFloat(Key_Distance);
        if(saveDistance < distance)
            PlayerPrefs.SetFloat(Key_Distance, distance);

        PlayerPrefs.Save();
    }

    // Đọc số coin từ PlayerPrefs
    public int LoadCoins() => PlayerPrefs.GetInt(Key_Coin, 0);
    public float LoadHightDistance() => PlayerPrefs.GetFloat(Key_Distance, 0);

    public void SaveCoin(int coins)
    {
        PlayerPrefs.SetInt(Key_Coin, coins);
        PlayerPrefs.Save();
    }
}
