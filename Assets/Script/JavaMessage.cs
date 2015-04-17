using UnityEngine;
using System.Collections;

public class JavaMessage : MonoBehaviour
{

#if UNITY_ANDROID && !UNITY_EDITOR
    private static AndroidJavaClass jc ;
    private static AndroidJavaObject jo ;
#endif

    public bool isBuyGold;
    public bool isBuyLive;
    public static int SHOW_BUY_HEART = 1;
    public static int SHOW_BUY_GOLD = 2;

    public static int SEND_TO_VIETTEL = 1;
    public static int SEND_TO_VINA_MOBI = 2;

    public int nhaMang = 1;

    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Debug.Log("JavaMessage: begin init java object");
    jc = new AndroidJavaClass("vn.tofu.fish.MainActivity");
    jo = jc.GetStatic<AndroidJavaObject>("_context");
        Debug.Log("JavaMessage: end init java object");
#endif
    }
    public void getMessageFailFromAndroid(string message)
    {
        PopupText.Instance().Show(message);
    }

    public void getMessageSuccessFromAndroid(string mess)
    {
        PopupText.Instance().Show("Giao dịch thành công.");
        Debug.Log("buy gold success");
        if(isBuyGold)
        {
            Prefs.Instance().setCoin(Prefs.Instance().getCoin() + 10000);
            PopupText.Instance().Show("Giao dịch thành công.\nBạn có thể đóng Popup lại.");
            isBuyGold = false;
            Debug.Log("buy gold success");
        }
        else
        {
            Prefs.Instance().setLive(Prefs.Instance().getLive() + 75);
            PopupText.Instance().Show("Giao dịch thành công.\nBạn có thể đóng Popup lại.");
            isBuyLive = false;

        }
    }

    public void buyGold(int nhaMang)
    {
        if(!isBuyGold && !isBuyLive)
        {
            Debug.Log("buy gold");
            isBuyGold = true;
            this.nhaMang = nhaMang;
            sendSMSToAndroid(nhaMang);
            if(Application.platform == RuntimePlatform.WindowsEditor)
                getMessageSuccessFromAndroid("success");

        }
    }

    public void buyLive(int nhaMang)
    {
        if(!isBuyGold && !isBuyLive)
        {
            isBuyLive = true;
            this.nhaMang = nhaMang;
            sendSMSToAndroid(nhaMang);
            if(Application.platform == RuntimePlatform.WindowsEditor)
                getMessageSuccessFromAndroid("success");

        }
    }

    public static void sendSMSToAndroid(int nhaMang)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        jo.Call("sendMessage", nhaMang);
#endif

    }
}
