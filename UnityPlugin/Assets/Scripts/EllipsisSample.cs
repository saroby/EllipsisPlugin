using Ellipsis;
using Ellipsis.Permission;
using UnityEngine;
using System.Collections.Generic;

public class EllipsisSample : MonoBehaviour
{
    private GUIStyle _guiStyle;
    private string _test = null;

    private void Start()
    {
        _guiStyle = new GUIStyle();
        _guiStyle.fontSize = 50;

        _test = EllipsisManager.Instance.GetTest();
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

        if (GUILayout.Button("check permissions", _guiStyle))
        {
            Debug.Log("check permissions");

            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    {
                        var result = Permission.CheckState(new[] { AndroidPermiossionType.READ_CONTACTS });
                        _test = DictionaryToString(result);
                        break;
                    }
                case RuntimePlatform.IPhonePlayer:
                    {
                        var result = Permission.CheckState(new[] { IOSPermissionType.NSPhotoLibraryAddUsageDescription, IOSPermissionType.NSCameraUsageDescription });
                        _test = DictionaryToString(result);
                        break;
                    }
            }
        }

        GUILayout.FlexibleSpace();

        if (GUILayout.Button("request permissions", _guiStyle))
        {
            Debug.Log("request permissions");

            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    {
                        Permission.RequestAsync(new[] { AndroidPermiossionType.READ_CONTACTS, AndroidPermiossionType.WRITE_EXTERNAL_STORAGE, AndroidPermiossionType.SEND_SMS }, (result) =>
                        {
                            Debug.Log(DictionaryToString(result));                            
                        });
                        break;
                    }
                case RuntimePlatform.IPhonePlayer:
                    {
                        Permission.Request(new[] { IOSPermissionType.NSPhotoLibraryAddUsageDescription, IOSPermissionType.NSCameraUsageDescription });
                        break;
                    }
            }
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndArea();
    }

    public string DictionaryToString<T1, T2>(Dictionary<T1, T2> dictionary)
    {
        string dictionaryString = "{";
        foreach (KeyValuePair<T1, T2> keyValues in dictionary)
        {
            dictionaryString += keyValues.Key + " : " + keyValues.Value + ", ";
        }
        return dictionaryString.TrimEnd(',', ' ') + "}";
    }
}