using UnityEngine;
using System.Collections;

public class UpgradePopup : MonoBehaviour  
{

    public int currentIdCannon;
    public ItemPopup[] itemPopup;
    // Use this for initialization
    void Start()
    {
        currentIdCannon = 1;
        itemPopup[currentIdCannon - 1].loadItem();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void click(int idCannon)
    {
        if(idCannon == currentIdCannon)
        {

        }
        else
        {

            itemPopup[currentIdCannon - 1].unLoadItem();
            itemPopup[idCannon - 1].loadItem();
            currentIdCannon = idCannon;
        }
        SoundManager.Instance().playBtnClick();
    }

    public void upgradeSpeedOnDown()
    {
        SoundManager.Instance().playBtnClick();
        itemPopup[currentIdCannon - 1].upgradeSpeedOnDown();

    }

    public void upgradeSpeedOnUp()
    {


        itemPopup[currentIdCannon - 1].upgradeSpeedOnUp();
    }

    public void upgradeDamageOnDown()
    {
        SoundManager.Instance().playBtnClick();
        itemPopup[currentIdCannon - 1].upgradeDamageOnDown();
    }

    public void upgradeDamageOnUp()
    {
        itemPopup[currentIdCannon - 1].upgradeDamageOnUp();

    }

    public void Close(GameObject go)
    {
        
    }

    public void Open(GameObject open)
    {

    }

}
