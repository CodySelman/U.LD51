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
    Vector3 _direction;
    int _pierce;

    void Update() {
        if (GameManager.Instance.isGameOver || UpgradeManager.Instance.isUpgrading) return;
        
        float delta = Time.deltaTime;
        transform.Translate(  (_direction + _spread) * (_speed * delta));
        _lifetime -= delta;

        if (_lifetime <= 0) {
            // TODO dotween fade the below as callback
            _myPool.Return(this);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (GameManager.Instance.isGameOver) return;
        
        if (other.CompareTag("Enemy")) {
            other.GetComponent<Enemy>().GetHit(_damage);
            if (_pierce > 0) {
                _pierce -= 1;
            }
            else {
                _myPool.Return(this);
            }
        }
    }

    public void Init(ObjectPool<PlayerBullet> myPool, float speed, float size, int damage, float lifetime, float spread, Vector3 direction, int pierce) {
        _myPool = myPool;
        _speed = speed;
        _damage = damage;
        _lifetime = lifetime;
        _spread = RandomUtils.GetNoiseAngle2d(-spread, spread);
        _direction = direction;
        _pierce = pierce;

        gameObject.SetActive(true);
        Transform myTrans = transform;
        myTrans.rotation = Quaternion.identity;
        myTrans.parent = null;
        myTrans.localScale = new Vector3(size, size, size);
    }
}
