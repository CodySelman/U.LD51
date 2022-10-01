using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CollisionInfo {
	public bool _above, _below, _left, _right;
	
	public Vector2 MoveAmountOld;
	public int FaceDir;

	public void Reset() {
		_above = _below = false;
		_left = _right = false;
	}
}

public class TopdownController2d : RaycastController2d
{
    CollisionInfo _collisions;
    Vector2 _playerInput;
    
    public override void Start() {
	    base.Start ();
    }
    
    public void Move(Vector2 moveAmount, Vector2 input) {
	    UpdateRaycastOrigins ();
    
	    _collisions.Reset ();
	    _collisions.MoveAmountOld = moveAmount;
	    _playerInput = input;

        if (moveAmount.x != 0) {
	    	_collisions.FaceDir = (int)Mathf.Sign(moveAmount.x);
	    }
    
	    HorizontalCollisions(ref moveAmount);
        VerticalCollisions (ref moveAmount);
    
	    transform.Translate (moveAmount);
    }
    
    void HorizontalCollisions(ref Vector2 moveAmount) {
	    int directionX = _collisions.FaceDir;
	    float rayLength = Mathf.Abs(moveAmount.x) + SkinWidth;
    
	    if (Mathf.Abs(moveAmount.x) < SkinWidth) {
	    	rayLength = 2 * SkinWidth;
	    }
    
	    for (int i = 0; i < horizontalRayCount; i ++) {
	    	Vector2 rayOrigin = (directionX == -1) ? RayOrigins.BottomLeft : RayOrigins.BottomRight;
	    	rayOrigin += Vector2.up * (horizontalRaySpacing * i);
	    	RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
    
	    	Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);
            
            if (!hit) continue;
            if (hit.distance == 0) {
	            continue; 
            }
            moveAmount.x = (hit.distance - SkinWidth) * directionX;
            rayLength = hit.distance;

            _collisions._left = directionX == -1;
            _collisions._right = directionX == 1;
        }
    }
    
    void VerticalCollisions(ref Vector2 moveAmount) {
		int directionY = (int)Mathf.Sign(moveAmount.y);
		float rayLength = Mathf.Abs(moveAmount.y) + SkinWidth;

		for (int i = 0; i < verticalRayCount; i ++) {
			Vector2 rayOrigin = (directionY == -1) ? RayOrigins.BottomLeft : RayOrigins.TopLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i + moveAmount.x);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

			Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);

			if (!hit) continue;
			if (hit.distance == 0) {
				continue; 
			}
			
			moveAmount.y = (hit.distance - SkinWidth) * directionY;
			rayLength = hit.distance;

			_collisions._left = directionY == -1;
			_collisions._right = directionY == 1;
		}
    }
}
