using System;
using System.Collections;
using UnityEngine;

public class ImagePickerCallback : AndroidJavaProxy {

    public Action<string> onGetImageComplete;
    public Action<string> onGetImagesComplete;
    public Action onGetImageCancel;
    public Action onGetImageFail;

    public Action<string> onGetImageCropComplete;

    public Action onGetImageCropFail;

    public ImagePickerCallback () : base ("com.gigadrillgames.androidplugin.image.IImageCallback") { }

    void GetImageComplete (String imagePath) {
        onGetImageComplete (imagePath);
    }

    void GetImagesComplete (String imagePath) {
        onGetImagesComplete (imagePath);
    }

    void GetImageCancel () {
        onGetImageCancel ();
    }

    void GetImageFail () {
        onGetImageFail ();
    }

    void GetImageCropComplete(String imagePath){
        onGetImageCropComplete(imagePath);
    }
	void GetImageCropFail(){
        onGetImageCropFail();
    }
}