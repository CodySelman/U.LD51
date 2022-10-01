using UnityEngine;
using TMPro;

namespace CodTools.Utilities
{
    public class TestListener : MonoBehaviour
    {
        public TMP_Text text;
        private int num = 0;
        private string str = "";

        void OnEnable() {
            EventManager.instance.AddListener<TestEvent>(Callback);
        }

        void OnDisable() {
            EventManager.instance.RemoveListener<TestEvent>(Callback);
        }

        void Callback(TestEvent testEvent) {
            num += testEvent.TestInt;
            str += testEvent.TestString;

            text.text = num.ToString() + " " + str;
        }
    }
}
