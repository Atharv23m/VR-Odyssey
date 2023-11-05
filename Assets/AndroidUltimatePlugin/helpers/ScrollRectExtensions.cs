using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ScrollRectExtensions
{

    public static void ScrollToTop(this ScrollRect scrollRect)
    {
        //scrollRect.normalizedPosition = new Vector2(0, 1);
        scrollRect.verticalNormalizedPosition = 1f;
    }

    public static void ScrollToBottom(this ScrollRect scrollRect)
    {
        //scrollRect.normalizedPosition = new Vector2(0, 0);
        scrollRect.verticalNormalizedPosition = 0f;
    }
}
