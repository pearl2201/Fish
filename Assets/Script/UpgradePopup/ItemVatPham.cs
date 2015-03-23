using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ItemVatPham : MonoBehaviour
{
    public int id;
    public int cost;
    public int avaiableCount;

    public tk2dSprite defineIcon;
    public tk2dSprite bgIcon;

    public MuaVatPham muaVatPham;
    public enum TYPEITEMVATPHAM
    {
        SET, HOURGLASS, BOMB
    };

    public TYPEITEMVATPHAM type;
    public void setup(tk2dSprite icon,  tk2dTextMesh costT, tk2dTextMesh avaiableCountT)
    {
        if (type == TYPEITEMVATPHAM.BOMB)
        {
            avaiableCount = Prefs.Instance().getNumbBomb();
        }
        else if (type == TYPEITEMVATPHAM.HOURGLASS)
        {
            avaiableCount = Prefs.Instance().getNumbGlass();
        }
        else if (type == TYPEITEMVATPHAM.SET)
        {
            avaiableCount = Prefs.Instance().getNumbSet();
        }
        cost = 300;
        bgIcon.SetSprite("nc1");
        costT.text = "Giá: " + cost;
        avaiableCountT.text = "Hiện có: " + avaiableCount;
        icon.SetSprite(defineIcon.spriteId);
    }

    public void unChoose()
    {
        bgIcon.SetSprite("nc-2");
    }

    public void Choose()
    {

    }

    public void Click()
    {
        SoundManager.Instance().playBtnClick();
        muaVatPham.setActiveItem(id);
    }
    public void buy()
    {
        if (Prefs.Instance().getCoin() >= cost)
        {
            if (type == TYPEITEMVATPHAM.BOMB)
            {
                Prefs.Instance().setNumbBomb(Prefs.Instance().getNumbBomb() + 1);
            }
            else if (type == TYPEITEMVATPHAM.HOURGLASS)
            {
                Prefs.Instance().setNumbHourGlass(Prefs.Instance().getNumbGlass() + 1);
            }
            else if (type == TYPEITEMVATPHAM.SET)
            {
                Prefs.Instance().setNumbSet(Prefs.Instance().getNumbSet() + 1);
            }
            PopupText.Instance().Show("Bạn đã mua thành công.");
        }
        else
        {
            PopupText.Instance().Show("Không đủ vàng.");
        }
    }

}

