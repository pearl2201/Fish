﻿using UnityEngine;
using System.Collections;

public class CoinUpgradeManager : MonoBehaviour {

    public tk2dTextMesh coin;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        coin.text = "" + Prefs.Instance().getCoin();
	}

    public void OpenNapThe()
    {
        PopupThieuTien.Instance().Show(PopupThieuTien.SHOW_BUY_GOLD);
        StartScene.Instance().hideAllPopupMain();
    }
}
