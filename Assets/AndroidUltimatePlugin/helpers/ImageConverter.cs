using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageConverter
{

    public static string Texture2DToBase64(Texture2D texture2d)
    {
        string base64 = Convert.ToBase64String(texture2d.EncodeToJPG());
        return base64;
    }

    public static byte[] Texture2DToByteArray(Texture2D texture2d)
    {
        return texture2d.EncodeToJPG();
    }

    public static string ByteArrayToBase64(byte[] byteArray)
    {
        return Convert.ToBase64String(byteArray);
    }

    public static byte[] Base64ToByteArray(string base64)
    {
        return Convert.FromBase64String(base64);
    }

    public static Texture2D Base64ToTexture2D(string base64, int width, int height, TextureFormat format, bool mipmap)
    {
        byte[] byteArray = Convert.FromBase64String(base64);
        Texture2D tex = new Texture2D(width, height, format, mipmap);
        tex.LoadImage(byteArray);
        return (Texture2D)tex;
    }

	/// <summary>
	/// not yet tested
	/// </summary>
	/// <param name="base64"></param>
	/// <returns></returns>
    public static Texture2D Base64ToTexture2D(string base64)
    {
        byte[] imageData = Convert.FromBase64String(base64);

        int width, height;
        GetImageSize(imageData, out width, out height);

        Texture2D texture = new Texture2D(width, height, TextureFormat.ARGB32, false, true);
        texture.hideFlags = HideFlags.HideAndDontSave;
        texture.filterMode = FilterMode.Point;
        texture.LoadImage(imageData);

        return texture;
    }

    private static void GetImageSize(byte[] imageData, out int width, out int height)
    {
        width = ReadInt(imageData, 3 + 15);
        height = ReadInt(imageData, 3 + 15 + 2 + 2);
    }

    private static int ReadInt(byte[] imageData, int offset)
    {
        return (imageData[offset] << 8) | imageData[offset + 1];
    }
}
