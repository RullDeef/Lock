using UnityEngine;
using UnityEditor;
using Core;

[CustomEditor(typeof(LockConstructor))]
public class LockConstructorEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Create"))
        {
            Lock targetLock = (target as LockConstructor).targetLock;
            Transform holder = GameObject.Find("LockHolder").transform;

            (target as LockConstructor).ConstructLock(targetLock, holder);

            Debug.Log("created");
        }
    }
}
