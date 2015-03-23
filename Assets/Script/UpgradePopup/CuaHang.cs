using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CuaHang : MonoBehaviour
{
    public GameObject upgrade;
    public GameObject muaVatPham;

    public void Close()
    {

    }

    public void enableUpgrade()
    {
        upgrade.gameObject.SetActive(true);
        muaVatPham.gameObject.SetActive(false);
        SoundManager.Instance().playBtnClick();
    }


    public void enableMuaVatPham()
    {
        upgrade.gameObject.SetActive(false);
        muaVatPham.gameObject.SetActive(true);
        SoundManager.Instance().playBtnClick();
    }

}

