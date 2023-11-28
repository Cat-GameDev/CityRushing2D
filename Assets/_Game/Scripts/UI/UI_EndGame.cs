using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UI_EndGame : UICanvas
{
    [SerializeField] TextMeshProUGUI distanceText;
    [SerializeField] TextMeshProUGUI coinsText;

    public void MainMenuButton()
    {
        LevelManager.Instance.MainMenu();
        Close(0f);
    }

    public void DisplayInfo(float distantce, int coin)
    {
        distanceText.SetText("Distance: " + distantce.ToString("F2") + " m");
        coinsText.SetText("Coin: " + coin.ToString("#,#"));
    }


}
