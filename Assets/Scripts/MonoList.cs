using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

class MonoList<T> : List<T> where T : MonoBehaviour
{
    public void RemoveAndDestroy(T item)
    {
        Object.Destroy(item);
        Remove(item);
    }

    public void RemoveAndDestroy(IEnumerable<T> items)
    {
        foreach (T t in items)
        {
            RemoveAndDestroy(t);
        }
    }

    public void ClearAndDestroy()
    {
        foreach (T t in this)
        {
            Object.Destroy(t);
        }

        Clear();
    }
}