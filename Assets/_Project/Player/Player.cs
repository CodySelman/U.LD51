using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum ActorAnimationState
{
    None = 0,
    Idle = 1,
    Walking = 2,
    Death = 3,
    Attacking = 4,
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
        [SerializeField] SpriteRenderer gunSr;

        // variables
        [SerializeField] int healthMax = 3;
        [SerializeField] int health = 3;
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] bool isAlive = true;

        Vector2 _inputVec = Vector2.zero;
        ActorAnimationState _animState = ActorAnimationState.Idle;

        void Start() {
            health = healthMax;
        }

        void Update() {
            if (isAlive) {
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

                if (Input.GetMouseButtonDown(0)) {
                    gun.Shoot();
                }
                else if (Input.GetKeyDown(KeyCode.R)) {
                    gun.Reload();
                }   
            }
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
                default:
                    animator.Play("Idle");
                    break;
            }
        }

    }
