using UnityEngine;
using System.Collections;
using System;

public class ManagerAddLive : MonoBehaviour
{
    public DateTime test;
    public HealthDisplay healthDisplay;
    // Use this for initialization
    void Start()
    {
        DateTime test = new DateTime();
        test.AddMinutes(10);
    }

    // Update is called once per frame
    void Update()
    {

        int deltaTime = DateTime.Now.Subtract(Prefs.Instance().GetLastTimeAddLive()).Minutes;

        if(deltaTime >= 10)
        {

            Prefs.Instance().SetLastTimeAddLive(DateTime.Now);
            Prefs.Instance().setLive(Prefs.Instance().getLive() + 1);
            if(healthDisplay!=null)
            {
                healthDisplay.updateHealth();
            }
        }
    }


}
