namespace _45_task
{
    class BattleArena
    {
    }

    abstract class BaseFighter : IAttacker, IDamageable, IHealable, IClone
    {
        public abstract void Attack(IDamageable target);
        public abstract void TryHealing(int health);
        public abstract void TryTakeDamage(int damage);
        public abstract BaseFighter Clone();
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
        public override BaseFighter Clone()
        {
            return new Fighter1();
        }
    }

    class Mage : BaseFighter, IHealer
    {
        public override void Attack(IDamageable target)
        {
        }

        public void Heal(IHealable target)
        {
        }

        public override void TryHealing(int health)
        {
        }

        public override void TryTakeDamage(int damage)
        {
        }
        public override BaseFighter Clone()
        {
            return new Mage();
        }
    }

    class Warrior : BaseFighter
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
        public override BaseFighter Clone()
        {
            return new Warrior();
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

    interface IClone
    {
        BaseFighter Clone();
    }
}
