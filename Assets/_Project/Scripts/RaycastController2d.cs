using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class RaycastController2d : MonoBehaviour {
    
    public LayerMask collisionMask;

    protected const float SkinWidth = .015f;
    const float dstBetweenRays = .1f;
    [HideInInspector]
    public int horizontalRayCount;
    [HideInInspector]
    public int verticalRayCount;

    [HideInInspector]
    public float horizontalRaySpacing;
    [HideInInspector]
    public float verticalRaySpacing;

    [HideInInspector]
    public BoxCollider2D boxCollider;

    protected RaycastOrigins RayOrigins;

    public virtual void Awake() {
        boxCollider = GetComponent<BoxCollider2D> ();
    }

    public virtual void Start() {
        CalculateRaySpacing ();
    }

    protected void UpdateRaycastOrigins() {
        Bounds bounds = boxCollider.bounds;
        bounds.Expand(SkinWidth * -2);
	
        RayOrigins.BottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        RayOrigins.BottomRight = new Vector2(bounds.max.x, bounds.min.y);
        RayOrigins.TopLeft = new Vector2(bounds.min.x, bounds.max.y);
        RayOrigins.TopRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing() {
        Bounds bounds = boxCollider.bounds;
        bounds.Expand(SkinWidth * -2);

        float boundsWidth = bounds.size.x;
        float boundsHeight = bounds.size.y;
	
        horizontalRayCount = Mathf.RoundToInt(boundsHeight / dstBetweenRays);
        verticalRayCount = Mathf.RoundToInt(boundsWidth / dstBetweenRays);
	
        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    protected struct RaycastOrigins {
        public Vector2 TopLeft, TopRight;
        public Vector2 BottomLeft, BottomRight;
    }
}
