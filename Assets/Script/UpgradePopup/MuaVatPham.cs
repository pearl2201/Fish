using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MuaVatPham : MonoBehaviour
{

    public ItemVatPham[] items;
    public int indexId;
    public tk2dSprite icon; public tk2dTextMesh costT; public tk2dTextMesh avaiableCountT;

    void Start()
    {
        indexId = 0;
        setActiveItem(indexId);
    }
    public void setActiveItem(int id)
    {
        for (int i = 0; i < items.GetLength(0); i++)
        {
            if (id == i)
            {
                items[i].setup(icon, costT, avaiableCountT);
            }
            else
            {
                items[i].unChoose();
            }
        }
        indexId = id;
    }

    public void Click()
    {
        items[indexId].buy();
        items[indexId].setup(icon, costT, avaiableCountT);
    }
}

