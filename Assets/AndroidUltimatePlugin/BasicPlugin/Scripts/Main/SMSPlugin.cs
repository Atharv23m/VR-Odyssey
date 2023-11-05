using UnityEngine;
using System.Collections;
using System;

public class SMSPlugin : MonoBehaviour
{

    private static SMSPlugin instance;
    private static GameObject container;
    private const string TAG = "[SMSPlugin]: ";
    private static AUPHolder aupHolder;

#if UNITY_ANDROID
    private static AndroidJavaObject jo;
#endif

    public bool isDebug = true;
    private bool isInit = false;

    private Action<string,string,string> ReceivedSMS;

    public event Action<string,string,string> OnReceivedSMS
    {
        add { ReceivedSMS += value; }
        remove { ReceivedSMS += value; }
    }

    private Action SMSComplete;
    public event Action OnSMSComplete
    {
        add { SMSComplete += value; }
        remove { SMSComplete += value; }
    }

    private Action SMSFail;
    public event Action OnSMSFail
    {
        add { SMSFail += value; }
        remove { SMSFail += value; }
    }

    public static SMSPlugin GetInstance()
    {
        if (instance == null)
        {
            container = new GameObject();
            container.name = "SMSPlugin";
            instance = container.AddComponent(typeof(SMSPlugin)) as SMSPlugin;
            DontDestroyOnLoad(instance.gameObject);
            aupHolder = AUPHolder.GetInstance();
            instance.gameObject.transform.SetParent(aupHolder.gameObject.transform);
        }

        return instance;
    }

    private void Awake()
    {
#if UNITY_ANDROID
        if (Application.platform == RuntimePlatform.Android)
        {
            jo = new AndroidJavaObject("com.gigadrillgames.androidplugin.sms.SMSPlugin");
        }
#endif
    }

    /// <summary>
    /// Sets the debug.
    /// 0 - false, 1 - true
    /// </summary>
    /// <param name="debug">Debug.</param>
    public void SetDebug(int debug)
    {
#if UNITY_ANDROID
        if (Application.platform == RuntimePlatform.Android)
        {
            jo.CallStatic("SetDebug", debug);
            AUP.Utils.Message(TAG, "SetDebug");
        }
        else
        {
            AUP.Utils.Message(TAG, "warning: must run in actual android device");
        }
#endif
    }

    public void Init()
    {
        if (isInit)
        {
            return;
        }

#if UNITY_ANDROID
        if (Application.platform == RuntimePlatform.Android)
        {
            jo.CallStatic("init");
            isInit = true;
            SetCallbackListener(onSMSComplete, onSMSFail, onReceivedSMS);
        }
        else
        {
            AUP.Utils.Message(TAG, "warning: must run in actual android device");
        }
#endif
    }

    private void SetCallbackListener(Action onSMSComplete, Action onSMSFail, Action<string,string,string> onReceivedSMS)
    {
#if UNITY_ANDROID
        if (Application.platform == RuntimePlatform.Android)
        {
            SMSCallback callback = new SMSCallback();
            callback.onSMSComplete = onSMSComplete;
            callback.onSMSFail = onSMSFail;
            callback.onReceivedSMS = onReceivedSMS;

            jo.CallStatic("setCallbackListener", callback);
            AUP.Utils.Message(TAG, "setCallbackListener");
        }
        else
        {
            AUP.Utils.Message(TAG, "warning: must run in actual android device");
        }
#endif
    }

	public void SendSMS(string mobileNumber,string message)
    {
#if UNITY_ANDROID
        if (Application.platform == RuntimePlatform.Android)
        {
            jo.CallStatic("sendSMS", mobileNumber,message);
            AUP.Utils.Message(TAG, "sendSMS");
        }
        else
        {
            AUP.Utils.Message(TAG, "warning: must run in actual android device");
        }
#endif
    }

    private void onSMSComplete()
    {
        if (null != SMSComplete)
        {
            SMSComplete();
        }
    }

    private void onSMSFail()
    {
        if (null != SMSFail)
        {
            SMSFail();
        }
    }

    private void onReceivedSMS(string sender,string message,string all)
    {
        if (null != ReceivedSMS)
        {
            ReceivedSMS(sender,message,all);
        }
    }
}