using System;
using System.Collections;
using AUP;
using UnityEngine;
using UnityEngine.UI;

public class ImagePickerDemo : MonoBehaviour {
    private const string TAG = "[ImagePickerDemo]: ";

    private SharePlugin sharePlugin;
    private ImagePickerPlugin imagePickerPlugin;
    private UtilsPlugin utilsPlugin;

    public Text statusText;
    private string imagePath = "";

    public RawImage rawImage;
    public Button shareButton;

    private Dispatcher dispatcher;

    // Use this for initialization
    void Start () {
        dispatcher = Dispatcher.GetInstance ();

        utilsPlugin = UtilsPlugin.GetInstance ();
        utilsPlugin.SetDebug (0);

        sharePlugin = SharePlugin.GetInstance ();
        sharePlugin.SetDebug (0);

        imagePickerPlugin = ImagePickerPlugin.GetInstance ();
        imagePickerPlugin.SetDebug (0);
        // init image picker instance
        imagePickerPlugin.Init ();
        // add eventlisteners
        AddEventListener ();

        EnableDisableShareButton (false);
    }

    private void AddEventListener () {
        imagePickerPlugin.OnGetImageComplete += onGetImageComplete;
        imagePickerPlugin.OnGetImageCancel += onGetImageCancel;
        imagePickerPlugin.OnGetImageFail += onGetImageFail;

        imagePickerPlugin.OnGetImageCropComplete += onGetImageCropComplete;
        imagePickerPlugin.OnGetImageCropFail += onGetImageCropFail;
    }

    // remove event listeners
    private void RemoveEventListener () {
        imagePickerPlugin.OnGetImageComplete -= onGetImageComplete;
        imagePickerPlugin.OnGetImageCancel -= onGetImageCancel;
        imagePickerPlugin.OnGetImageFail -= onGetImageFail;

        imagePickerPlugin.OnGetImageCropComplete -= onGetImageCropComplete;
        imagePickerPlugin.OnGetImageCropFail -= onGetImageCropFail;
    }

    private void OnDestroy () {
        RemoveEventListener ();
    }

    public void GetImage () {
        imagePickerPlugin.GetImage ();
        EnableDisableShareButton (false);
    }

    public void GetImageCrop () {
        string folderPath = utilsPlugin.CreateRootFolder (".SecretContainerImage");
        if (!folderPath.Equals ("", StringComparison.Ordinal)) {
            imagePickerPlugin.GetImageCrop (folderPath + "/croppyImage.jpg", 180, 180);
            EnableDisableShareButton (false);
        }
    }

    public void ShareImage () {
        if (!imagePath.Equals ("", StringComparison.Ordinal)) {
            sharePlugin.ShareImage ("MyPictureSubject", "MyPictureSubjectContent", imagePath);
            UpdateStatus ("Sharing Picture");
        } else {
            Debug.Log ("[CameraDemo] imagepath is empty");
            UpdateStatus ("can't image path is empty");
        }
    }

    private void UpdateStatus (string status) {
        if (statusText != null) {
            statusText.text = String.Format ("Status: {0}", status);
        }
    }

    private void DelayLoadImage () {
        //loads texture
        rawImage.texture = AUP.Utils.LoadTexture (imagePath);

        UpdateStatus ("load image complete");
        EnableDisableShareButton (true);
    }

    private void EnableDisableShareButton (bool val) {
        shareButton.interactable = val;
    }

    private void LoadImageMessage () {
        UpdateStatus ("Loading Image...");
    }

    private void onGetImageComplete (string imagePath) {
        dispatcher.InvokeAction (
            () => {
                this.imagePath = imagePath;

                UpdateStatus ("Get Image Complete");

                Invoke ("LoadImageMessage", 0.3f);
                Invoke ("DelayLoadImage", 0.5f);

                Debug.Log (TAG + "onGetImageComplete imagePath " + imagePath);
            }
        );
    }

    private void onGetImageCancel () {
        dispatcher.InvokeAction (
            () => {
                UpdateStatus ("onGetImageCancel");
            }
        );
    }

    private void onGetImageFail () {
        dispatcher.InvokeAction (
            () => {
                UpdateStatus ("onGetImageFail");
            }
        );
    }

    private void onGetImageCropComplete (string imagePath) {
        dispatcher.InvokeAction (
            () => {
                this.imagePath = imagePath;

                UpdateStatus ("Get Image Crop Complete");

                Invoke ("LoadImageMessage", 0.3f);
                Invoke ("DelayLoadImage", 0.5f);

                string base64 = utilsPlugin.GetImageBase64 (imagePath);
                string folderPath = utilsPlugin.CreateRootFolder ("AUPCropImageFolder");

                // generate timestamp based on current time on the device
                string imageID = TimeHelper.GetMilliSecondSinceEpoch ().ToString ();

                if (!folderPath.Equals ("", StringComparison.Ordinal)) {
                    utilsPlugin.SaveImageBase64 (base64, folderPath + "/" + imageID + ".jpg");
                }
                Debug.Log (TAG + "onGetImageCropComplete imagePath " + imagePath);

                string testPath = utilsPlugin.getExternalStorageDirectory () + "/" + "AUPCropImageFolder" + "/" + imageID + ".jpg";
                base64 = utilsPlugin.GetImageBase64 (testPath);

                // bonus you can also create hidden folder and save image inside it
                string newfolderPath = utilsPlugin.CreateRootFolder (".SecretContainerImage");
                if (!newfolderPath.Equals ("", StringComparison.Ordinal)) {
                    utilsPlugin.SaveImageBase64 (base64, newfolderPath + "/testImage22.jpg");
                }
            }
        );
    }

    private void onGetImageCropFail () {
        dispatcher.InvokeAction (
            () => {
                UpdateStatus ("onGetImageCropFail");
                Debug.Log (TAG + "onGetImageCropFail failed ");
            }
        );
    }
}