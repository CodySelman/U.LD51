using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodTools.Utilities;

public class PlayerBullet : MonoBehaviour
{
    ObjectPool<PlayerBullet> _myPool;
    float _speed;
    int _damage;
    float _lifetime;
    Vector3 _spread;
    bool _isFlipped;

    void Update() {
        float delta = Time.deltaTime;
        if (_isFlipped) {
            transform.Translate(  (transform.right + _spread) * (_speed * delta));
        }
        else {
            transform.Translate(  (transform.right * -1 + _spread) * (_speed * delta));
        }
        _lifetime -= delta;

        if (_lifetime <= 0) {
            // TODO dotween fade the below as callback
            _myPool.Return(this);
        }
    }

    void OnTriggerEnter(Collider other) {
        // hit stuff
    }

    public void Init(ObjectPool<PlayerBullet> myPool, float speed, float size, int damage, float lifetime, float spread, bool isFlipped) {
        _myPool = myPool;
        _speed = speed;
        _damage = damage;
        _lifetime = lifetime;
        _spread = RandomUtils.GetNoiseAngle2d(-spread, spread);
        _isFlipped = isFlipped;
        
        gameObject.SetActive(true);
        Transform myTrans = transform;
        myTrans.parent = null;
        myTrans.localScale = new Vector3(size, size, size);
    }
}
