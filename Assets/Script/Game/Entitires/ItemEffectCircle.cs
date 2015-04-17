using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ItemEffectCircle : MonoBehaviour
{

    public tk2dSprite image;
    public tk2dSprite bg;
    public float bankinh;

    void Start()
    {
        bankinh = bg.GetBounds().size.x / 2;
    }
    public void setup(MainGameScript.GameState gameState)
    {
        gameObject.SetActive(true);
        if(gameState == MainGameScript.GameState.BANHMY)
        {
            image.SetSprite("banhmy");
        }
        else if(gameState == MainGameScript.GameState.BOM)
        {
            image.SetSprite("bomb");
        }
        else if(gameState == MainGameScript.GameState.SET)
        {
            image.SetSprite("set");
        }
        if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            point.z = gameObject.transform.position.z;
            transform.position = point;
        }
        else
        {
            if(Input.touchCount > 0)
            {
                Vector3 point = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                point.z = gameObject.transform.position.z;
                transform.position = point;

            }
        }
    }
    void Update()
    {
        if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            point.z = gameObject.transform.position.z;
            transform.position = point;
        }
        else
        {
            if(Input.touchCount > 0)
            {
                Vector3 point = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                point.z = gameObject.transform.position.z;
                transform.position = point;

            }
        }

    }

}

