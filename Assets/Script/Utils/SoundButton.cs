using UnityEngine;
using System.Collections;

public class SoundButton : MonoBehaviour {

    public tk2dSprite display;
	// Use this for initialization
	void Start () {
        checkDisplaySprite();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void toggle()
    {
        SoundManager.Instance().toggle();
        checkDisplaySprite();
    }

    public void checkDisplaySprite()
    {
        if(SoundManager.Instance().isSoundActive())
        {
            display.SetSprite("btnSound_1");
        }
        else
        {
            display.SetSprite("btnSound_2");
        }
    }
}
