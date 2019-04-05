using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class ExtensionMethods
{
    /// <summary>
    /// Récupéré ici https://answers.unity.com/questions/530178/how-to-get-a-component-from-an-object-and-add-it-t.html
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="comp"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static T GetCopyOf<T>(this MonoBehaviour Monobehaviour, T Source) where T : MonoBehaviour
    {
        //Type check
        Type type = Monobehaviour.GetType();
        if (type != Source.GetType()) return null;

        //Declare Binding Flags
        BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;

        //Iterate through all types until monobehaviour is reached
        while (type != typeof(MonoBehaviour))
        {
            //Apply Fields
            FieldInfo[] fields = type.GetFields(flags);
            foreach (FieldInfo field in fields)
            {
                field.SetValue(Monobehaviour, field.GetValue(Source));
            }

            //Move to base class
            type = type.BaseType;
        }
        return Monobehaviour as T;
    }
}
