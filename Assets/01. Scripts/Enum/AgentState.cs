
[System.Flags]
public enum AgentState
{
    Idle = 0,
    Chase = 1,
    Attack = 1 << 1,
    Stun = 1 << 2,
    Die = 1 << 3,
}