using UnityEngine;
using System.Collections;

public class PopupThieuTien : MonoBehaviour
{

    private static PopupThieuTien _instance;
    public tk2dTextMesh textbuyGold;
    public tk2dTextMesh textbuyHeart;
    public JavaMessage javaMessage;

    public static int SHOW_BUY_HEART = 1;
    public static int SHOW_BUY_GOLD = 2;
    public int option;
    public int nhaMang;
    public static PopupThieuTien Instance()
    {
        return _instance;
    }

    void Awake()
    {
        _instance = this;
    }

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
       
        if (StartScene.Instance()!=null)
        {
            StartScene.Instance().showUpgradePopupOnly();
        }
        else
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    public void Show(int option)
    {
        gameObject.SetActive(true);
        this.option = option;
        if(option == SHOW_BUY_GOLD)
        {
            textbuyGold.gameObject.SetActive(true);
            textbuyHeart.gameObject.SetActive(false);
        }
        else if(option == SHOW_BUY_HEART)
        {
            textbuyGold.gameObject.SetActive(false);
            textbuyHeart.gameObject.SetActive(true);
        }
    }

    public void Show(int option, float duration)
    {
        Show(option);

        StartCoroutine(closeAfterTime(duration));
    }

    IEnumerator closeAfterTime(float duration)
    {
        float t = 0;
        while(t <= duration)
        {
            t += Time.deltaTime;
            yield return null;
        }
        Close();
    }

    public void clickBuy()
    {
        PopupText.Instance().Show("Đang xử lý. Vui lòng đợi.");
        if(option == SHOW_BUY_GOLD)
        {
            javaMessage.buyGold(nhaMang);
        }
        else
        {
            javaMessage.buyLive(nhaMang);
        }
       // gameObject.SetActive(false);
       
    }

    public void clickViettelItem()
    {
        nhaMang = JavaMessage.SEND_TO_VIETTEL;
        clickBuy();
    }

    public void clickMobiVinaItem()
    {
        nhaMang = JavaMessage.SEND_TO_VINA_MOBI;
        clickBuy();
    }
}
