using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Pause : UICanvas
{
    public override void Open()
    {
        Time.timeScale = 0;
        base.Open();
    }

    public override void Close(float delayTime)
    {
        Time.timeScale = 1;
        base.Close(0f);
    }

    public void MainMenuButton()
    {
        LevelManager.Instance.MainMenu();
        Close(0f);
    }

    public void ExitButton()
    {
        Close(0f);
    }
}
