using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


[System.Serializable]
public struct ColorToSell
{
    public Color color;
    public int price;
}


public enum ColorType
{
    playerColor,
    platformColor
}


public class UI_Shop : UICanvas
{
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI notifyText;

    [Header("Platform colors")]
    [SerializeField] private GameObject platformColorButton;
    [SerializeField] private Transform platformColorParent;
    [SerializeField] private Image platformDisplay;
    [SerializeField] private ColorToSell[] platformColors;

    
    [Header("Player colors")]
    [SerializeField] private GameObject playerColorButton;
    [SerializeField] private Transform playerColorParent;
    [SerializeField] private Image playerDisplay;
    [SerializeField] private ColorToSell[] playerColors;

    void Start()
    {
        coinsText.text = UserData.Ins.LoadCoins().ToString("#,#");

        for (int i = 0; i < platformColors.Length; i++)
        {
            Color color = platformColors[i].color;
            int price = platformColors[i].price;

            GameObject newButton = Instantiate(platformColorButton, platformColorParent);

            newButton.transform.GetChild(0).GetComponent<Image>().color = color;
            newButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = price.ToString("#,#");

            newButton.GetComponent<Button>().onClick.AddListener(() => PurchaseColor(color, price, ColorType.platformColor));
        }

        for (int i = 0; i < playerColors.Length; i++)
        {
            Color color = playerColors[i].color;
            int price = playerColors[i].price;

            GameObject newButton = Instantiate(playerColorButton, playerColorParent);

            newButton.transform.GetChild(0).GetComponent<Image>().color = color;
            newButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = price.ToString("#,#");    // price.ToString("#,#");;

            newButton.GetComponent<Button>().onClick.AddListener(() => PurchaseColor(color, price, ColorType.playerColor));
        }
    }

    public void PurchaseColor(Color color, int price, ColorType colorType)
    {
        //AudioManager.instance.PlaySFX(4);

        if (EnoughMoney(price))
        {
            if (colorType == ColorType.platformColor)
            {
                LevelManager.Instance.plaformColor = color;
                platformDisplay.color = color;
            }
            else if (colorType == ColorType.playerColor)
            {
                LevelManager.Instance.GetPlayer().sr.color = color;
                playerDisplay.color = color;
            }

            StartCoroutine(Notify("Purchased successful", 1));
        }
        else
            StartCoroutine(Notify("Not enough money!", 1));

    }

    private bool EnoughMoney(int price)
    {
        int myCoins = UserData.Ins.LoadCoins();

        if (myCoins > price)
        {
            int newAmountOfCoins = myCoins - price;
            UserData.Ins.SaveCoin(newAmountOfCoins);
            coinsText.text = UserData.Ins.LoadCoins().ToString("#,#");
            return true;
        }
        return false;

    }

    IEnumerator Notify(string text, float seconds)
    {
        notifyText.text = text;

        yield return new WaitForSeconds(seconds);

        notifyText.text = "Click to buy";
    }

    public void ExitButton()
    {
        GameManager.Instance.ChangeState(GameState.MainMenu);
        UIManager.Instance.OpenUI<UI_MainMenu>();
        Close(0f);
    }
}
