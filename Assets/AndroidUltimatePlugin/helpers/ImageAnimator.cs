using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAnimator : MonoBehaviour
{

	public RawImage targetImage;
    public Texture2D[] frames;
    public int framesPerSecond = 10;

    private void Update()
    {
        int index = (int)(Time.time * framesPerSecond) % frames.Length;
		targetImage.texture = frames[index];
        //renderer.material.mainTexture = frames[index];
    }

}
