using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppVersionController : MonoBehaviour {
    public Text versionText;
    private void OnEnable(){
        UpdateVersionText();
    }
    private void UpdateVersionText(){
        if(versionText!= null){
            versionText.text = "version: " + Application.version;
        }
    }
}