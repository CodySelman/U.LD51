using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodTools.Utilities;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] PlayerBullet bulletPrefab;
    [SerializeField] Animator reloadBarAnim;
    [SerializeField] Transform bulletSpawnTransform;
    [SerializeField] Transform bulletSpawnFlipTransform;
    
    [SerializeField] float speedBase = 5f;
    [SerializeField] float speedMod = 1f;
    [SerializeField] float speed = 5f;
    
    [SerializeField] float sizeBase = 1f;
    [SerializeField] float sizeMod = 1f;
    [SerializeField] float size = 1f;
    
    [SerializeField] int damageBase = 1;
    [SerializeField] float damageMod = 1f;
    [SerializeField] int damage = 1;

    [SerializeField] int clipSizeBase = 6;
    [SerializeField] float clipSizeMod = 1f;
    [SerializeField] int clipSize = 6;
    
    [SerializeField] float fireRateBase = 3f;
    [SerializeField] float fireRateMod = 1f;
    [SerializeField] float fireRate = 3f;

    [SerializeField] float reloadTimeBase = 1.5f;
    [SerializeField] float reloadTimeMod = 1f;
    [SerializeField] float reloadTime = 1.5f;

    [SerializeField] float spreadBase = 1f;
    [SerializeField] float spreadMod = 1f;
    [SerializeField] float spread = 1f;
    
    [SerializeField] float lifetimeBase = 1f;
    [SerializeField] float lifetimeMod = 1f;
    [SerializeField] float lifetime = 1f;
    
    ObjectPool<PlayerBullet> _pool;
    int _currentAmmo;
    float _shotTimer = 0f;
    bool _isShotCooldown = false;
    bool _isReloading = false;
    float _reloadTimeRemaining = 0f;
    AnimationClip _reloadClip;
    float _reloadClipLength;

    void Start() {
        _pool = new ObjectPool<PlayerBullet>(bulletPrefab, 10, transform);
        
        SetSpeed();
        SetSize();    
        SetDamage();    
        SetClipSize();
        SetFireRate();    
        SetReloadTime();
        SetSpread();
        SetLifetime();    
        
        _currentAmmo = clipSize;
        // update UI
        EvAmmoChanged e = new (_currentAmmo, clipSize);
        EventManager.instance.Raise(e);
        
        _reloadClip = reloadBarAnim.GetCurrentAnimatorClipInfo(0)[0].clip;
        _reloadClipLength = _reloadClip.length;
        reloadBarAnim.gameObject.SetActive(false);
    }

    void Update() {
        if (_isShotCooldown) {
            _shotTimer -= Time.deltaTime;
            
            if (_shotTimer <= 0) {
                _isShotCooldown = false;
            }
        }
        
        if (_isReloading) {
            _reloadTimeRemaining -= Time.deltaTime;
            if (_reloadTimeRemaining <= 0) {
                Reload();
            }
        }
    }

    public void Shoot() {
        if (!_isReloading && !_isShotCooldown) {
            PlayerBullet bullet = _pool.Get();
            Transform t = transform;
            Vector3 reticlePos = Reticle.Instance.transform.position;
            Vector3 bulletSpawnPos = sr.flipX ? bulletSpawnFlipTransform.position : bulletSpawnTransform.position;
            Vector3 dir =  (reticlePos - bulletSpawnPos).normalized;
            bullet.transform.position = bulletSpawnPos;
            bullet.Init(_pool, speed, size, damage, lifetime, spread, dir);
            _currentAmmo -= 1;
            EvAmmoChanged e = new (_currentAmmo, clipSize);
            EventManager.instance.Raise(e);
            if (_currentAmmo <= 0) {
                StartReload();
            }
            else {
                _shotTimer = 1 / fireRate;
                _isShotCooldown = true;
            }
        }
    }

    public void StartReload() {
        if (_currentAmmo < clipSize) {
            _isReloading = true;
            _reloadTimeRemaining = reloadTime;
            reloadBarAnim.gameObject.SetActive(true);
            reloadBarAnim.speed =  _reloadClipLength / reloadTime;
            reloadBarAnim.Play("Reload");
        }
    }

    public void Reload() {
        _isReloading = false;
        reloadBarAnim.gameObject.SetActive(false);
        _currentAmmo = clipSize;
        EvAmmoChanged e = new (_currentAmmo, clipSize);
        EventManager.instance.Raise(e);
    }

    public void ChangeSpeedBase(float changeAmount) {
        speedBase = Mathf.Max(1, speedBase + changeAmount);
        SetSpeed();
    }
    public void ChangeSpeedMod(float changeAmount) {
        speedMod = Mathf.Max(0.1f, speedMod + changeAmount);
        SetSpeed();
    }
    void SetSpeed() {
        speed = speedBase * speedMod;
    }

    public void ChangeSizeBase(float changeAmount) {
        sizeBase = Mathf.Max(1, sizeBase + changeAmount);
        SetSize();
    }
    public void ChangeSizeMod(float changeAmount) {
        sizeMod = Mathf.Max(0.1f, sizeMod + changeAmount);
        SetSize();
    }
    void SetSize() {
        size = sizeBase * sizeMod;
    }

    public void ChangeDamageBase(float changeAmount) {
        damageBase = Mathf.FloorToInt(Mathf.Max(1, damageBase + changeAmount));
        SetDamage();
    }
    public void ChangeDamageMod(float changeAmount) {
        damageMod = Mathf.Max(0.1f, damageMod + changeAmount);
        SetDamage();
    }
    void SetDamage() {
        damage = Mathf.CeilToInt(damageBase * damageMod);
    }

    public void ChangeClipSizeBase(float changeAmount) {
        clipSizeBase = Mathf.FloorToInt(Mathf.Max(1, clipSizeBase + changeAmount));
        SetClipSize();
    }
    public void ChangeClipSizeMod(float changeAmount) {
        clipSizeMod = Mathf.Max(0.1f, clipSizeMod + changeAmount);
        SetClipSize();
    }
    void SetClipSize() {
        clipSize = Mathf.CeilToInt(clipSizeBase * clipSizeMod);
        EvAmmoChanged e = new (_currentAmmo, clipSize);
        EventManager.instance.Raise(e);
    }

    public void ChangeReloadTimeBase(float changeAmount) {
        reloadTimeBase = Mathf.Max(1, reloadTimeBase + changeAmount);
        SetReloadTime();
    }
    public void ChangeReloadTimeMod(float changeAmount) {
        reloadTimeMod = Mathf.Max(0.1f, reloadTimeMod + changeAmount);
        SetReloadTime();
    }
    void SetReloadTime() {
        reloadTime = reloadTimeBase * reloadTimeMod;
    }
    
    public void ChangeSpreadBase(float changeAmount) {
        spreadBase = Mathf.Max(1, spreadBase + changeAmount);
        SetSpread();
    }
    public void ChangeSpreadMod(float changeAmount) {
        spreadMod = Mathf.Max(0.1f, spreadMod + changeAmount);
        SetSpread();
    }
    void SetSpread() {
        spread = spreadBase * spreadMod;
    }
    
    public void ChangeLifetimeBase(float changeAmount) {
        lifetimeBase = Mathf.Max(1, lifetimeBase + changeAmount);
        SetLifetime();
    }
    public void ChangeLifetimeMod(float changeAmount) {
        lifetimeMod = Mathf.Max(0.1f, lifetimeMod + changeAmount);
        SetLifetime();
    }
    void SetLifetime() {
        lifetime = lifetimeBase * sizeMod;
    }
    
    public void ChangeFireRateBase(float changeAmount) {
        fireRateBase = Mathf.Max(1, fireRateBase + changeAmount);
        SetFireRate();
    }
    public void ChangeFireRateMod(float changeAmount) {
        fireRateMod = Mathf.Max(0.1f, fireRateMod + changeAmount);
        SetFireRate();
    }
    void SetFireRate() {
        fireRate = fireRateBase * fireRateMod;
    }
}
