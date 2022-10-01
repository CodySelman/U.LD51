using UnityEngine;

namespace JRPGAutobattler.Rendering
{
    public class Billboard : MonoBehaviour
    {
        Camera _cameraMain;
        void LateUpdate() {
            BillboardSprites();
        }

        [ContextMenu("Billboard")]  
        public void BillboardSprites() {
            _cameraMain ??= Camera.main;
            if (_cameraMain is not null) {
                transform.forward = _cameraMain.transform.forward;
            }
        }
    }
}
