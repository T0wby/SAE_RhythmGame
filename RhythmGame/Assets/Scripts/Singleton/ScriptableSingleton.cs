using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[Serializable]
public class ScriptableSingleton<T> : ScriptableObject where T : ScriptableObject
{
	private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance is null) Initialize();
            else
            {
                var name = typeof(T).Name;
                Debug.LogWarning($"Another instance of {name} is already running. Instance is {_instance.name}.");
            }
            return _instance;
        }
    }

    private static void Initialize()
    {
        _instance = Resources.Load<T>($"Assets/Resources/Singleton/{typeof(T)}.asset");
        if (_instance is null)
        {
            var data = CreateInstance<T>();
            var path = string.Empty;

            try
            {
#if UNITY_EDITOR
                //Create Folder
                path = "Assets/Resources";
                if (!AssetDatabase.IsValidFolder(path)) AssetDatabase.CreateFolder("Assets", "Resources");
                path = string.Concat(path, "/Singleton");
                if (!AssetDatabase.IsValidFolder(path)) AssetDatabase.CreateFolder("Assets/Resources", "Singleton");
                path = string.Concat(path, $"/{typeof(T)}.asset");

                //Create Asset
                AssetDatabase.CreateAsset(data, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = data;
#endif
                _instance = data;
            }
            catch (IOException ex)
            {
                Debug.LogException(ex);
            }

        }
    }

    private void RemoveDuplicates()
    {
        T[] objArr = FindObjectsOfType<T>();
#if UNITY_EDITOR

        if (objArr.Length > 1)
        {
            var path = string.Empty;
            for (int i = 1; i < objArr.Length; i++)
            {
                path = AssetDatabase.GetAssetPath(objArr[i]);
                AssetDatabase.DeleteAsset(path);
            }
        }
#endif
    }

    protected virtual void Awake() => RemoveDuplicates();
}
