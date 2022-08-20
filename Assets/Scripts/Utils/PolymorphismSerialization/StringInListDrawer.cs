﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class StringInListAttribute : PropertyAttribute {
    public delegate string[] GetStringList();

    protected StringInListAttribute()
    {
    }

    public StringInListAttribute(Type _type) {
        list = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(_asssembly => _asssembly.GetTypes())
            .Where(type => type.IsSubclassOf(_type) && !type.IsGenericType)
            .Select(type =>type.ToString())
            .ToList();
    }

    public StringInListAttribute(Type _type, string _methodName) {
        var method = _type.GetMethod (_methodName);
        if (method != null) {
            list = method.Invoke (null, null) as List<string>;
        } else {
            Debug.LogError ("NO SUCH METHOD " + _methodName + " FOR " + _type);
        }
    }
   

    public List<string> list {
        get;
        protected set;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(StringInListAttribute))]
public class tringInListDrawer : PropertyDrawer {
    // Draw the property inside the given rect
    public override void OnGUI (Rect _position, SerializedProperty _property, GUIContent _label) {
        var stringInList = attribute as StringInListAttribute;
        var list = stringInList.list;
        
        if (_property.propertyType == SerializedPropertyType.String) {
            int index = Mathf.Max (0, list.IndexOf (_property.stringValue));
            index = EditorGUI.Popup (_position, _property.displayName, index, list.ToArray());

            _property.stringValue = list [index];
        } else if (_property.propertyType == SerializedPropertyType.Integer) {
            _property.intValue = EditorGUI.Popup (_position, _property.displayName, _property.intValue, list.ToArray());
        } else {
            base.OnGUI (_position, _property, _label);
        }
    }
}
#endif