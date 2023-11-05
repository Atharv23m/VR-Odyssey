using System;
using System.Collections;
using System.Collections.Generic;
using AUP;
using UnityEngine;

public class ImagePickerDemo3 : MonoBehaviour {

	private const string TAG = "[ImagePickerDemo3]: ";
	private ImagePickerPlugin imagePickerPlugin;
	private UtilsPlugin utilsPlugin;
	private Dispatcher dispatcher;

	private void Awake () {
		dispatcher = Dispatcher.GetInstance ();

		utilsPlugin = UtilsPlugin.GetInstance ();
		utilsPlugin.SetDebug (0);

		imagePickerPlugin = ImagePickerPlugin.GetInstance ();
		imagePickerPlugin.SetDebug (0);
		// init image picker instance
		imagePickerPlugin.Init ();
	}

	// Use this for initialization
	void Start () {

	}

	public void AddEventListener () {
		imagePickerPlugin.OnGetImageCropComplete += onGetImageCropComplete;
		imagePickerPlugin.OnGetImageCropFail += onGetImageCropFail;
		imagePickerPlugin.OnGetImageFail += onGetImageCropFail;
		imagePickerPlugin.OnGetImageCancel += onGetImageCropFail;
	}

	public void RemoveEventListener () {
		imagePickerPlugin.OnGetImageCropComplete -= onGetImageCropComplete;
		imagePickerPlugin.OnGetImageCropFail -= onGetImageCropFail;
		imagePickerPlugin.OnGetImageFail -= onGetImageCropFail;
		imagePickerPlugin.OnGetImageCancel -= onGetImageCropFail;
	}

	public void GetImage () {
		string folderPath = utilsPlugin.CreateRootFolder ("AUPCropImage");
		string imageID = TimeHelper.GetMilliSecondSinceEpoch ().ToString ();
		if (!folderPath.Equals ("", StringComparison.Ordinal)) {
			imagePickerPlugin.GetImageCrop (folderPath + "/" + imageID + ".jpg", 180, 180);
		}

	}

	private void onGetImageCropComplete (string imagePath) {
		dispatcher.InvokeAction (
			() => {
				string base64 = utilsPlugin.GetImageBase64 (imagePath);
				Texture2D tex = new Texture2D (180, 180, TextureFormat.ARGB32, false);
				tex.LoadImage (Convert.FromBase64String (base64));
			}
		);
	}

	private void onGetImageCropFail () {
		dispatcher.InvokeAction (
			() => {
				Debug.Log (TAG + "onGetImageCropFail failed ");
			}
		);
	}
}