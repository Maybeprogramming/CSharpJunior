//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace _45_Task
//{
//    public class BattleArena
//    {
//    }

//    public abstract class BaseFighter : IAttacker, IDamageable, IHealable
//    {
//        public abstract void Attack(IDamageable target);
//        public abstract void TryHealing(int health);
//        public abstract void TryTakeDamage(int damage);
//    }

//    public class Fighter1 : BaseFighter
//    {
//        public override void Attack(IDamageable target)
//        {
//            throw new NotImplementedException();
//        }

//        public override void TryHealing(int health)
//        {
//            throw new NotImplementedException();
//        }

//        public override void TryTakeDamage(int damage)
//        {
//            throw new NotImplementedException();
//        }
//    }

//    interface IDamageable
//    {
//        void TryTakeDamage(int damage);
//    }

//    interface IHealable
//    {
//        void TryHealing(int health);
//    }

//    interface IAttacker
//    {
//        void Attack(IDamageable target);
//    }

//    interface IHealer
//    {
//        void Heal(IHealable target);
//    }
//}
