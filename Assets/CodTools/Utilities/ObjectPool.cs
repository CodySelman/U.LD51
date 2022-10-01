using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    T _poolObject;
    int _initialPoolSize = 5;
    Transform _parent;

    readonly List<T> _pool = new();

    public ObjectPool(T poolObject, int initialPoolSize, Transform myParent) {
        _poolObject = poolObject;
        _initialPoolSize = initialPoolSize;
        _parent = myParent;
        Init();
    }

    void Init() {
        for (int i = 0; i < _initialPoolSize; i++)
        {
            SpawnPoolObject();
        }
    }

    T SpawnPoolObject() {
        T o = GameObject.Instantiate(_poolObject, _parent.transform);
        o.gameObject.SetActive(false);
        _pool.Add(o);
        return o;
    }
    
    public T Get() {
        T o = _pool.Count == 0 ? SpawnPoolObject() : _pool[^1];
        _pool.Remove(o);
        return o;
    }
 
    public void Return(T o) {
        Transform t = o.transform;
        t.gameObject.SetActive(false);
        _pool.Add(o);
        t.SetParent(_parent.transform);
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
    }
}