using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SimpleSceneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void LoadNextScene(){
		SceneManager.LoadScene("SecondScene");
	}

	public void LoadPrevScene(){
		SceneManager.LoadScene("FirstScene");
	}
	
	
}
