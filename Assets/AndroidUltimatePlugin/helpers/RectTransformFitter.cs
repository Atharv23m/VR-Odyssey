using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RectTransformFitter : MonoBehaviour
{
    public VerticalLayoutGroup layout;
    private RectTransform content;
    public float currentHeightDisplay = 0f;

    // Use this for initialization
    void Start()
    {
        content = (RectTransform)transform;
    }

    public void CheckFit()
    {
        float currentHeight = 0f;

        foreach (RectTransform child in transform)
        {
            currentHeight += child.rect.height + layout.spacing;
            if (currentHeight >= content.rect.height)
            {                
                Vector2 tempPos = content.position;
                content.sizeDelta = new Vector2(content.sizeDelta.x, currentHeight);
                tempPos.y = currentHeight;
                content.position = tempPos;
            }
        }

        currentHeightDisplay = currentHeight;
    }
}
