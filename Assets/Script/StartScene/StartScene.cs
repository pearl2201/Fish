using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class StartScene : MonoBehaviour,CloseUpgradePopupInterface 
{

    public GameObject uiStartScene;
    public GameObject upgradePopup;
    public void StartCLick()
    {
        Application.LoadLevel("GameScene");
    }

    public void ExitClick()
    {
        Application.Quit();
    }

    public void UpgradeClick()
    {
        uiStartScene.SetActive(false);
        if (upgradePopup!=null)
        {
            upgradePopup.SetActive(true);
        }
    }

    public void HideUpgradePopup()
    {

        uiStartScene.SetActive(true);
        if(upgradePopup != null)
        {
            upgradePopup.SetActive(false);
        }
    }

    public void CloseUpgradePopup()
    {
        upgradePopup.gameObject.SetActive(false);
        uiStartScene.gameObject.SetActive(true);
    }

    
}

