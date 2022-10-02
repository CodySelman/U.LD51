using System.Collections;
using System.Collections.Generic;
using CodTools.Utilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum ActorAnimationState
{
    None = 0,
    Idle = 1,
    Walking = 2,
    Death = 3,
    Attacking = 4,
    Spawn = 5,
}

[RequireComponent((typeof(Rigidbody2D)))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(TopdownController2d))]
    public class Player : MonoBehaviour
    {
        // components
        [SerializeField] Rigidbody2D rb;
        [SerializeField] BoxCollider2D collider;
        [SerializeField] SpriteRenderer sr;
        [SerializeField] Animator animator;
        [SerializeField] TopdownController2d topdownController;
        [SerializeField] PlayerGun gun;
        [SerializeField] Transform bulletSpawnTransform;
        [SerializeField] SpriteRenderer gunSr;
        [SerializeField] DeadPlayer deadPlayerPrefab;

        // variables
        [SerializeField] int healthMax = 3;
        [SerializeField] int health = 3;
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float respawnAnimLength = 0.4f;
        [SerializeField] float invincibilityTime = 0.5f;

        public bool isAlive = true;
        bool _isRespawning;
        float _respawnTimeRemaining = 0f;
        float _batteryTimeMax = 10f;
        float _batteryTime = 10f;
        int _batterySecondsLeft = 10;
        bool _isInvincible = false;
        float _invincibilityTimer = 0f;

        Vector2 _inputVec = Vector2.zero;
        ActorAnimationState _animState = ActorAnimationState.Idle;

        void Start() {
            ResetStats();
            StartSpawn();
        }

        void Update() {
            if (isAlive && !_isRespawning) {
                // movement
                _inputVec = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
                if (_inputVec.magnitude > 0.1f) {
                    if (_animState != ActorAnimationState.Walking) {
                        ChangeAnimationState(ActorAnimationState.Walking);
                    }
                    Vector2 moveVec = _inputVec * (moveSpeed * Time.deltaTime);
                    topdownController.Move(moveVec, _inputVec);
                } else if (_animState != ActorAnimationState.Idle) {
                    ChangeAnimationState(ActorAnimationState.Idle);
                }

                // gun & sprite renderer
                Vector3 pos = transform.position;
                Vector3 gunPos = gun.transform.position;
                Vector3 reticlePos = Reticle.Instance.transform.position;
                float gunAngle = Mathf.Atan2(gunPos.y - reticlePos.y, gunPos.x - reticlePos.x) * Mathf.Rad2Deg;
                
                if (reticlePos.x >= pos.x) {
                    sr.flipX = true;
                    gunSr.flipX = true;
                    gunSr.transform.rotation = Quaternion.Euler(new Vector3(0, 0, gunAngle + 180));
                }
                else {
                    sr.flipX = false;
                    gunSr.flipX = false;
                    gunSr.transform.rotation = Quaternion.Euler(new Vector3(0, 0, gunAngle));
                }

                if (_isInvincible) {
                    _invincibilityTimer -= Time.deltaTime;
                    if (_invincibilityTimer <= 0f) {
                        _isInvincible = false;
                    }
                }

                if (Input.GetMouseButtonDown(0)) {
                    gun.Shoot();
                }
                else if (Input.GetKeyDown(KeyCode.R)) {
                    gun.Reload();
                }
            }
            
            _batteryTime -= Time.deltaTime;
            int tempBatterySeconds = Mathf.CeilToInt(_batteryTime);
            if (tempBatterySeconds != _batterySecondsLeft) {
                SetBatterySecondsLeft(tempBatterySeconds);
            }
            if (_batteryTime <= 0 && !_isRespawning) {
                if (isAlive) {
                    Die();
                }
                StartSpawn();
            }

            if (_isRespawning) {
                _respawnTimeRemaining -= Time.deltaTime;
                if (_respawnTimeRemaining <= 0f) {
                    FinishSpawn();
                }
            }
        }

        public void GetHit() {
            if (_isInvincible || !isAlive || _isRespawning) return;

            SetHealth(health - 1);

            if (health <= 0) {
                Die();
            }
            else {
                _isInvincible = true;
                _invincibilityTimer = invincibilityTime;
            }
        }

        void ResetStats() {
            SetHealth(healthMax);
            _batteryTime = _batteryTimeMax;
            SetBatterySecondsLeft(Mathf.CeilToInt(_batteryTime));
        }

        void SetHealth(int newHealth) {
            health = newHealth;
            EvPlayerHealthChanged e = new (health, healthMax);
            EventManager.instance.Raise(e);
        }

        void StartSpawn() {
            _isRespawning = true;
            if (_animState == ActorAnimationState.Death) {
                Transform t = transform;
                DeadPlayer d = Instantiate(deadPlayerPrefab, t.position, deadPlayerPrefab.transform.rotation);
                d.Init(sr.flipX);
            }
            _respawnTimeRemaining = respawnAnimLength;
            transform.position = Ship.Instance.playerSpawnPoint.position;
            ChangeAnimationState(ActorAnimationState.Spawn);
        }

        void FinishSpawn() {
            ResetStats();
            _isRespawning = false;
            isAlive = true;
        }

        void SetBatterySecondsLeft(int seconds) {
            _batterySecondsLeft = seconds;
            EvBatteryChanged e = new(_batterySecondsLeft);
            EventManager.instance.Raise(e);
        }

        void ChangeAnimationState(ActorAnimationState newState) {
            if (newState == _animState) {
                return;
            }

            _animState = newState;
            PlayAnimation();
        }

        void PlayAnimation() {
            switch (_animState) {
                case ActorAnimationState.Idle:
                    animator.Play("Idle");
                    break;
                case ActorAnimationState.Walking:
                    animator.Play("Walk");
                    break;
                case ActorAnimationState.Death:
                    animator.Play("Death");
                    break;
                case ActorAnimationState.Spawn:
                    animator.Play("Spawn");
                    break;
                default:
                    animator.Play("Idle");
                    break;
            }
        }

        void Die() {
            isAlive = false;
            ChangeAnimationState(ActorAnimationState.Death);
        }

    }
