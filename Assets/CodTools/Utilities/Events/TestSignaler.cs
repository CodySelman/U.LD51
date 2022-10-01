using UnityEngine;

namespace CodTools.Utilities
{
    public class TestSignaler : MonoBehaviour
    {
        public TestEvent testEvent = new TestEvent(1, "a");

        void Update() {
            if (Input.GetKeyDown(KeyCode.T)) {
                EventManager.instance.Raise(testEvent);
            }
        }
    }
}
