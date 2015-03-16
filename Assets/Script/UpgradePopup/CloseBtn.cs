using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class CloseBtn : MonoBehaviour
{

    public GameObject close;

    public void ClosePopup()
    {
        close.SendMessage("CloseUpgradePopup");
    }
}

