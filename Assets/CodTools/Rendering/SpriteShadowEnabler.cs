using UnityEngine;
using UnityEngine.Rendering;

namespace CodTools.Rendering
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteShadowEnabler : MonoBehaviour
    {
        [SerializeField] bool shouldCastShadows = true;
        [SerializeField] bool shouldReceiveShadows = true;
        void Start() {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.shadowCastingMode = shouldCastShadows ? ShadowCastingMode.On : ShadowCastingMode.Off;
            sr.receiveShadows = shouldReceiveShadows;
        }
    }
}
