using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public static class WindowLockShortcut
{
    [MenuItem("Tools/Window Locker/Toggle Window Lock", false, 950)]
    [Shortcut("Window Locker/Toggle Window Lock", KeyCode.L, ShortcutModifiers.Action)]
    public static void ToggleLock()
    {
        EditorWindow focusedWindow = EditorWindow.focusedWindow;

        if (focusedWindow == null) return;

        Type windowType = focusedWindow.GetType();

        PropertyInfo propertyInfo = windowType.GetProperty("isLocked", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        if (propertyInfo != null)
        {
            bool currentState = (bool)propertyInfo.GetValue(focusedWindow, null);
            propertyInfo.SetValue(focusedWindow, !currentState, null);

            focusedWindow.Repaint();

            Debug.Log($"{windowType.Name} をロックしました。");
        }
        else
        {
            Debug.LogWarning($"{windowType.Name} はロック機能をサポートしていないか、プロパティ名が異なります。");
        }
    }
}