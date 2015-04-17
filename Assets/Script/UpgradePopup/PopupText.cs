﻿using UnityEngine;
using System.Collections;

public class PopupText : MonoBehaviour
{
    private static PopupText _instance;
    public static PopupText Instance()
    {
        return _instance;
    }

    void Awake()
    {
        _instance = this;
    }
    public tk2dTextMesh message;
    // Use this for initialization
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Close()
    {
        SoundManager.Instance().playBtnClick();
        gameObject.SetActive(false);
    }

    public void Show(string mess)
    {
        gameObject.SetActive(true);
        Debug.Log("PopupText: " + mess);
        message.text = mess;
    }

    public void Show(string mess, float duration)
    {
        gameObject.SetActive(true);
       message.text = mess;
        StartCoroutine(closeAfterTime(duration));
    }

    IEnumerator closeAfterTime(float duration)
    {
        float t = 0;
        while (t<= duration)
        {
            t += Time.deltaTime;
            yield return null;
        }
        Close();
    }
}
