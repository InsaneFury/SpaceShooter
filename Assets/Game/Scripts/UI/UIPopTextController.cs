using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopTextController : MonoBehaviour
{
    private static UIPopText popText;
    private static GameObject canvas;

    public static void Initialize()
    {
        canvas = GameObject.Find("Canvas");
        if(!popText)
        popText = Resources.Load<UIPopText>("Prefabs/PopContainer");
    }

    public static void CreatePopText(string text, Transform t)
    {
        UIPopText instance = Instantiate(popText);
        Vector2 screenPos = Camera.main.WorldToScreenPoint(new Vector2(t.position.x + Random.Range(-.5f, .5f),t.position.y + Random.Range(-.5f, .5f)));

        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPos;
        instance.SetText(text);
    }
}
