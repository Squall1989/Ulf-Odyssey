public interface IAttackable 
{
    AttackableType AttackableType { get; }
    bool IsAlive { get; }
    void Attack(float amount);
}

public enum AttackableType
{
    npc,
    player,
}