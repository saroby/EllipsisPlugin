using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllipsisSample : MonoBehaviour
{
    private GUIStyle _guiStyle;
    private string _test = null; 

    private void Start()
    {
        _guiStyle = new GUIStyle();
        _guiStyle.fontSize = 100;

        _test = Ellipsis.Instance.GetTest();
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        GUILayout.Label(_test, _guiStyle);
        
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.EndArea();
    }
}
