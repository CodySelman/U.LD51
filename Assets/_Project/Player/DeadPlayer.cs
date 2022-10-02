using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlayer : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;

    public void Init(bool isFlipped) {
        if (isFlipped) {
            sr.flipX = true;
        }
    }
}
