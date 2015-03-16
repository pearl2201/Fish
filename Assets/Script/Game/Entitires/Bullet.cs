using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    public Vector3 speed;
    public int indexCannon;
    public tk2dSprite bulletImage;
    public Vector3 pos;

    // Use this for initialization
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        pos += speed;
        transform.position = pos;
        if(outOfScreen())
        {
            MainGameScript.lockBullet = false;
            Destroy(this.gameObject);
        }
    }
    public float cos;
    public float sin;
    public void setup(Transform cannon, float z, float angle, int indexCannon)
    {
        this.pos = cannon.transform.position;
        this.transform.localRotation = cannon.transform.localRotation;
        pos.z = z;
        transform.position = pos;
         cos = Mathf.Cos(angle * Mathf.Deg2Rad);
         sin = Mathf.Sin(angle * Mathf.Deg2Rad);
        if(cos < 0)
            cos *= (-1);
        if(angle > 0 && angle <= 90 && sin > 0)
        {
            sin *= (-1);
        }
        else if(angle >= -90 && angle < 0 && sin < 0)
        {
            sin *= (-1);
        }
        speed = new Vector3(Constants.SPEED_BULLET_MODEL * (1 + ((float) Prefs.Instance().getSpeed(indexCannon))/10) * sin, Constants.SPEED_BULLET_MODEL * (1 +((float) Prefs.Instance().getSpeed(indexCannon))/10) * cos, 0);
        this.indexCannon = indexCannon;
        bulletImage.SetSprite("dan" + indexCannon);
        
    }

    public bool outOfScreen()
    {
        pos = transform.position;
        if(Mathf.Abs(pos.x) >= Constants.HALF_SCREEN_UNIT_WIDTH + 0.05f || Mathf.Abs(pos.y) >= Constants.HALF_SCREEN_UNIT_HEIGHT + 0.05f)
        {
            return true;
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Coll");
        Fish fishScript = other.gameObject.GetComponentInParent<Fish>();
        Debug.Log(other.gameObject.name);
        if(fishScript != null)
        {
            Debug.Log("Get fiish script");
            
            LuoiManager.Instance().setupLuoi(this);
        }

    }
}
