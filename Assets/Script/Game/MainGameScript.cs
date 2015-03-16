using UnityEngine;
using System.Collections;

public class MainGameScript : MonoBehaviour
{

    public Cannon cannon;
    public tk2dCamera mCamera;
    public GameObject bulletContainer;
    public GameObject bulletModel;
    public GameObject fishContainer;
    public static bool lockBullet;

    private static MainGameScript _instance;

    public static MainGameScript Instance()
    {
        if(_instance == null)
        {
            Debug.Log("loi roi em oi");
        }
        return _instance;
    }
    void Awake()
    {
        _instance = this;
    }
    // Use this for initialization
    void Start()
    {
        lockBullet = false;
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void fireBullet()
    {
        if(!lockBullet)
        {
            // get position of mouse;
            Vector3 aPoint = cannon.transform.position;
            Vector3 bPoint = new Vector3();
            if(Application.platform == RuntimePlatform.WindowsEditor)
            {
                bPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                if(Input.touchCount > 0)
                    bPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            }
            bPoint.z = aPoint.z;
            Vector3 targetDir = bPoint - aPoint;
            Vector3 forward = new Vector3(0, 1, 0);

            float goc = Vector3.Angle(targetDir, forward);
            if(targetDir.x > 0)
            {
                goc *= (-1);
            }
            Debug.Log("goc: " + goc + "targetDir: " + targetDir.ToString() + " forward: " + forward.ToString());
            cannon.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, goc));

            // khoi tao bullet
            GameObject go = Instantiate(bulletModel) as GameObject;
            go.transform.parent = bulletContainer.transform;
            Bullet bulletScript = go.GetComponent<Bullet>();
            bulletScript.setup(cannon.transform, bulletContainer.transform.position.z, goc, cannon.indexCannon);
            lockBullet = true;
            cannon.fire();
            CoinManager.Instance().subGold(cannon.indexCannon);

        }
    }

    public void ReturnMenuScreen()
    {
        Application.LoadLevel("StartScene");
    }
}
