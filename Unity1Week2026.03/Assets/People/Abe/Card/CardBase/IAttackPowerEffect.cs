public interface IAttackPowerEffect
{
    int AttackPower { get; }
    void AddAttackPower(int delta);
}
