namespace CodTools.Utilities
{
    public abstract class State
    {
        protected StateMachine sm;

        protected State(StateMachine sm)
        {
            this.sm = sm;
        }

        public virtual void Enter(){}
        public virtual void HandleInput(){}
        public virtual void LogicUpdate(){}
        public virtual void PhysicsUpdate(){}
        public virtual void LateUpdate(){}
        public virtual void Exit(){}
    }
}
