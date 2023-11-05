using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AUP;
using UnityEngine.UI;

public class SMSDemo : MonoBehaviour {
	private const string TAG = "[SMSDemo]: ";
	private SMSPlugin smsPlugin;
	private Dispatcher dispatcher;
	public InputField mobileNumberInput;
	public InputField messageInput;
    public Text messageReceivedText;

	private string mobileNumber;
	private string message;

	// Use this for initialization
	void Start () {
		dispatcher = Dispatcher.GetInstance();
		smsPlugin = SMSPlugin.GetInstance();
		smsPlugin.SetDebug(0);
		smsPlugin.Init();
		AddEventListener();
	}

	private void OnDestroy()
    {
        RemoveEventListener();
    }
	
	private void AddEventListener()
    {
		smsPlugin.OnReceivedSMS+=OnReceivedSMS;
		smsPlugin.OnSMSComplete+=OnSMSComplete;
		smsPlugin.OnSMSFail+=OnSMSFail;
	}

	private void RemoveEventListener()
    {
		smsPlugin.OnReceivedSMS-=OnReceivedSMS;
		smsPlugin.OnSMSComplete-=OnSMSComplete;
		smsPlugin.OnSMSFail-=OnSMSFail;
	}

	public void SendSMS(){
		if (mobileNumberInput != null)
        {
           mobileNumber = mobileNumberInput.text;
		}else{
			Debug.Log( TAG + " mobileNumberInput is null ");
		}

		if (messageInput != null)
        {
           message = messageInput.text;
		}else{
			Debug.Log( TAG + " messageInput is null ");
		}

		if(!string.IsNullOrEmpty(mobileNumber) && !string.IsNullOrEmpty(message) ){
			smsPlugin.SendSMS(mobileNumber,message);
		}else{
			Debug.Log( TAG + " mobileNumber is null or empty or message is null or empty ");
		}
	}

	private void UpdateMessageReceived(string val){
		if(messageReceivedText!=null){
			messageReceivedText.text = val;
		}else{
			Debug.Log( TAG + " messageReceivedText is null ");
		}
	}

	private void OnReceivedSMS(string sender, string message,string all){
		 dispatcher.InvokeAction(
            () =>
            {
				UpdateMessageReceived("From: " + sender + " message: " + message );
                Debug.Log( TAG + " OnReceivedSMS sender: " + sender + " message " + message + " all: " + all );
            }
        );
	}

	private void OnSMSComplete(){
		dispatcher.InvokeAction(
            () =>
            {
                Debug.Log( TAG + " OnSMSComplete" );
            }
        );
	}

	private void OnSMSFail(){
		dispatcher.InvokeAction(
            () =>
            {
                Debug.Log( TAG + " OnSMSFail" );
            }
        );
	}
}