namespace _45_task
{
    class BattleArena
    {
    }

    abstract class BaseFighter : IAttacker, IDamageable, IHealable
    {
        public abstract void Attack(IDamageable target);
        public abstract void TryHealing(int health);
        public abstract void TryTakeDamage(int damage);
    }

    class Fighter1 : BaseFighter
    {
        public override void Attack(IDamageable target)
        {
        }

        public override void TryHealing(int health)
        {
        }

        public override void TryTakeDamage(int damage)
        {
        }
    }

    interface IDamageable
    {
        void TryTakeDamage(int damage);
    }

    interface IHealable
    {
        void TryHealing(int health);
    }

    interface IAttacker
    {
        void Attack(IDamageable target);
    }

    interface IHealer
    {
        void Heal(IHealable target);
    }
}
