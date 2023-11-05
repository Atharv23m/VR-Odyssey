using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextSpeech;
using UnityEngine.UI;
using UnityEngine.Android;

public class Voice_Controller : MonoBehaviour
{
    const string LANG_CODE = "en-US";
    public Text res;

    // Start is called before the first frame update
    void Start()
    {
        SpeechToText.Instance.Setting(LANG_CODE);
        SpeechToText.Instance.onResultCallback = OnFinalSpeechResult;
        SpeechToText.Instance.onPartialResultsCallback = OnPartialSpeechResult;
       
        if(!Permission.HasUserAuthorizedPermission(Permission.Microphone))
            Permission.RequestUserPermission(Permission.Microphone);
    }

    public void Start_Listening()
    {
        SpeechToText.Instance.StartRecording();
        Debug.Log("Listening");
    }

    public void Stop_Listening()
    {
        SpeechToText.Instance.StopRecording();
        Debug.Log("Stopped");
    }

    void OnFinalSpeechResult(string result)
    {
        res.text = result;
    }

    void OnPartialSpeechResult(string result)
    {
        res.text = result;
    }
}
