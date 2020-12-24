using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public class PrefabSearchTool : EditorWindow
{
    string cpName;
    StringBuilder result = new StringBuilder();
    public List<string> searchPaths = new List<string>();

    [MenuItem("Custom/查找使用component的prefab")]
    public static void Search()
    {
        EditorWindow.GetWindow(typeof(PrefabSearchTool));
    }

    void OnGUI()
    {
        result.Clear();
        GUILayout.BeginHorizontal("box");
        if (GUILayout.Button("添加搜索路径"))
        {
            string[] guids = Selection.assetGUIDs;
            for (int i = 0; i < guids.Length; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                if (!searchPaths.Contains(assetPath)) searchPaths.Add(assetPath);
            }
        }

        if (GUILayout.Button("清除搜索路径"))
        {
            searchPaths.Clear();
        }
        GUILayout.EndHorizontal();

        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty stringsProperty = so.FindProperty("searchPaths");
        EditorGUILayout.PropertyField(stringsProperty, true);
        so.ApplyModifiedProperties();

        GUILayout.Space(40);
        cpName = EditorGUILayout.TextField(label: "输入组件名称：", text: cpName);
        GUILayout.Space(40);


        if (GUILayout.Button("确定"))
        {
            for (int i = 0; i < searchPaths.Count; i++)
            {
                string path = searchPaths[i];
                bool isFolder = AssetDatabase.IsValidFolder(path);
                if (isFolder)
                {
                    FindInDirectory(path);
                }
            }
            Debug.Log("结果：" + result.ToString());
        }
    }

    void FindInDirectory(string path)
    {
        string[] files = Directory.GetFiles(path);
        foreach (string file in files)
        {
            string xfile = file.Replace(@"\", "/");
            if (Path.GetExtension(xfile) == ".prefab")
            {
                GameObject prefab = AssetDatabase.LoadAssetAtPath(xfile, typeof(System.Object)) as GameObject;
                if (!prefab) continue;
                foreach (var cp in prefab.GetComponents<Component>())
                {
                    if (cp.GetType().Name == cpName) result.Append(xfile + ";");
                }
            }
        }

        string[] dirs = Directory.GetDirectories(path);
        foreach (string dir in dirs)
        {
            string xdir = dir.Replace(@"\", "/");
            FindInDirectory(xdir);
        }
    }
}
