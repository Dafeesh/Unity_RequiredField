using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
#endif

[
#if UNITY_EDITOR
    InitializeOnLoad,
#endif
     AttributeUsage(AttributeTargets.Field)
]
public class RequiredField : Attribute
{
#if UNITY_EDITOR
    static RequiredField()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    static void OnPlayModeStateChanged(PlayModeStateChange change)
    {
        if (change == PlayModeStateChange.EnteredPlayMode)
        {
            CheckRequiredFields();
        }
    }

    //[DidReloadScripts] //Optional if you also want to check whenever scripts compile
    static void CheckRequiredFields()
    {
        if (!EditorApplication.isPlaying)
            return;

        MonoBehaviour firstMissingReference = null;
        MonoBehaviour[] allComponents = GameObject.FindObjectsOfType<MonoBehaviour>();
        foreach (var obj in allComponents)
        {
            IEnumerable<FieldInfo> fieldInfos = obj.GetType()
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where((f) => f.GetCustomAttributes(typeof(RequiredField), inherit: true)
                    .Any((o) => o is RequiredField));

            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                object value = fieldInfo.GetValue(obj);
                if (value == null || value.Equals(null))
                {
                    firstMissingReference = obj;
                    Debug.LogError(
                        "Missing required field value! " +
                        "'" + obj.name + "' (" + obj.GetType().ToString() + ")", obj);
                }
            }
        }

        if (firstMissingReference != null)
            EditorApplication.isPlaying = false;
    }

    /* Can also...
     * 
    
        if (firstMissingReference != null)
            firstMissingReference.StartCoroutine(StopEditorAtEndOfFrame());
    }

    static IEnumerator StopEditorAtEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        EditorApplication.isPlaying = false;
    }

    */
#endif
}
