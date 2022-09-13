public interface IStateable
{
    public void AddState(AgentState targetState);
    public void RemoveState(AgentState targetState);
    public AgentState GetState();
}