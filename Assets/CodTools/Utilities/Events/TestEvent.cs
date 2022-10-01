namespace CodTools.Utilities
{
    public class TestEvent : IGameEvent
    {
        public int TestInt { get; }

        public string TestString { get; }

        public TestEvent(int testInt, string testString) {
            TestInt = testInt;
            TestString = testString;
        }
    }
}
