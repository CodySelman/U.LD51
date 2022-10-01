using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodTools.Utilities
{
    public class Pool<T> : MonoBehaviour where T : MonoBehaviour
    {
        readonly List<T> _pool = new ();
        readonly List<T> _inUsePool = new ();

        public T prefab;
        [SerializeField]
        int initialSize = 0;
        [SerializeField]
        int maxSize = 100;
        void Awake() {
            for (int i = 0; i < initialSize; i++) {
                SpawnPoolItem();
            }
        }

        T SpawnPoolItem() {
            T o = Instantiate(prefab, transform);
            o.gameObject.SetActive(false);
            _pool.Add(o);
            return o;
        }

        public T Get() {
            // if unused pool has obj, return it
            if (_pool.Count > 0) {
                T o = _pool[^1];
                _pool.Remove(o);
                _inUsePool.Add(o);
                return o;
            }
            else if (_pool.Count + _inUsePool.Count > maxSize) {
                T o = SpawnPoolItem();
                return o;
            }
            else {
                return null;
            }
        }

        public void ReturnToPool(T obj) {
            _inUsePool.Remove(obj);
            _pool.Add(obj);
        }
    }
}
