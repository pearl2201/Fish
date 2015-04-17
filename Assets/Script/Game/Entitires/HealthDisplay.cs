using UnityEngine;
using System.Collections;

public class HealthDisplay : MonoBehaviour {

    public tk2dTextMesh health;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update ()
    {

        
    }

    public void updateHealth()
    {
        health.text = Prefs.Instance().getLive() + "";
    }
}
