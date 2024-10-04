namespace _18_Task
{
    public class Program
    {
        private static void Main()
        {
            const string CommandBaseAttack = "1";
            const string CommandFireball = "2";
            const string CommandExplosion = "3";
            const string CommandPlayerHealing = "4";
            const string ExitBattleCommand = "exit";

            Console.Title = "ДЗ: Бой с боссом";
            Console.WindowHeight = 50;
            Console.WindowWidth = 100;
            Random random = new Random();

            string name = "Валанар";
            int playerHealth = 500;
            int playerMana = 500;
            int damageBaseAttack = 100;
            int damageFireball = 150;
            int damageExplosion = 250;
            int restorationHealth = 200;
            int restorationMana = 200;
            int restorationManaByStep = 50;
            int restorationCount = 2;
            int fireballManaCost = 250;
            bool isUsedFireball = false;

            string enemyName = "Бальтазар";
            int healthEnemy = 1000;
            int damageEnemy = 100;

            bool isPlayerDoneStep = false;
            bool isEnemyDoneStep = false;
            bool isRunCombat = true;
            string userInput;

            string skillBaseAttackInfo = $"Обычная атака клинком с нанесением [{damageBaseAttack}] урона";
            string skillFireballInfo = $"Бросок огненного шара с нанесением [{damageFireball}] урона";
            string skillExplosionInfo = $"Произвести взрыв горящего врага с нанесением [{damageExplosion}] урона";
            string skillRestorationHealthInfo = $"Выпить зелье восстановления [{restorationHealth}] здоровья и [{restorationMana}] маны";

            string battleTextSkillBaseAttack = $"Игрок [{name}] делает обычную атаку врагу [{enemyName}]";
            string battleTextSkillFireball = $"Игрок [{name}] бросает огненный шар в врага [{enemyName}]";
            string battleTextSkillExplosion = $"Игрок [{name}] взрывает горящего врага [{enemyName}]";
            string battleTextSkillRestorationHealth = $"Игрок [{name}] пьёт зелье восстановления";

            string skillMenu = $"Ваши навыки:" +
                               $"\n {CommandBaseAttack} - {skillBaseAttackInfo}" +
                               $"\n {CommandFireball} - {skillFireballInfo}" +
                               $"\n {CommandExplosion} - {skillExplosionInfo}" +
                               $"\n {CommandPlayerHealing} - {skillRestorationHealthInfo}" +
                               $"\n {ExitBattleCommand} - побег с поля боя...";

            string continueMessage = "\nНажмите любую клавишу чтобы продолжить...\n";
            string startBattleMessage = "\nДа начнётся битва героя с боссом!!!";
            string requestCommandMessage = "\nВведите команду: ";
            string playerStepBreakMessage = "Такого умения нет, вы пропускаете свой ход.";
            string playerStepMessage = ">------ Ход игрока ------<";
            string enemyStepMessage = ">------ Ход босса ------<";
            string playerVictoryMessage = $"Битва окончена! Игрок {name} победил своего врага {enemyName}! Да здравствует победа!";
            string enemyVictoryMessage = $"Битва окончена! Игрок {name} пал смертью храбрых. {enemyName} - победил в этой битве!";
            string drowInBattleMessage = $"Битва окончена ничьей! Игрок {name} и {enemyName} не добились успеха в битве!";
            string loseEscapeFromBattleMessage = "Вы сбежали с битвы, позор вам!";

            Console.WriteLine(startBattleMessage);

            while (isRunCombat == true)
            {
                Console.Clear();
                Console.WriteLine($"Здоровье игрока: [{playerHealth}] единиц." +
                                   $"\nМана игрока: [{playerMana}] единиц." +
                                   $"\nЗелий восстановления: [{restorationCount}] хода." +
                                   $"\n------------------------------------------------" +
                                   $"\nЗдоровье босса: [{healthEnemy}] единиц.\n");

                while (isPlayerDoneStep == false)
                {
                    Console.WriteLine(skillMenu);
                    Console.Write(requestCommandMessage);
                    userInput = Console.ReadLine();
                    Console.WriteLine(playerStepMessage);

                    switch (userInput)
                    {
                        case ExitBattleCommand:
                            Console.WriteLine(loseEscapeFromBattleMessage);
                            return;

                        case CommandBaseAttack:
                            healthEnemy -= damageBaseAttack;
                            Console.WriteLine(battleTextSkillBaseAttack);
                            isPlayerDoneStep = true;
                            break;

                        case CommandFireball:
                            if (playerMana >= fireballManaCost)
                            {
                                healthEnemy -= damageFireball;
                                playerMana -= fireballManaCost;
                                Console.WriteLine(battleTextSkillFireball);
                                isUsedFireball = true;
                                isPlayerDoneStep = true;
                            }
                            else
                            {
                                Console.WriteLine($"Сейчас вы не можете использовать:" +
                                    $"\n[{skillFireballInfo}]." +
                                    $"\n Вам не хватает маны.");
                            }

                            break;

                        case CommandExplosion:
                            if (isUsedFireball == true)
                            {
                                healthEnemy -= damageExplosion;
                                Console.WriteLine(battleTextSkillExplosion);
                                isUsedFireball = false;
                                isPlayerDoneStep = true;
                            }
                            else
                            {
                                Console.WriteLine($"Сейчас вы не можете использовать" +
                                    $"\n[{skillExplosionInfo}]." +
                                    $"\nПримените сначала:" +
                                    $"\n[{skillFireballInfo}].");
                            }

                            break;

                        case CommandPlayerHealing:
                            if (restorationCount > 0)
                            {
                                playerHealth += restorationHealth;
                                playerMana += restorationMana;
                                Console.WriteLine(battleTextSkillRestorationHealth);
                                restorationCount--;
                                isPlayerDoneStep = true;
                            }

                            break;

                        default:
                            Console.WriteLine(playerStepBreakMessage);
                            break;
                    }

                    Console.WriteLine(continueMessage);
                    Console.ReadKey();
                }

                while (isEnemyDoneStep == false)
                {
                    Console.WriteLine(enemyStepMessage);
                    playerHealth -= damageEnemy;
                    Console.WriteLine($"{enemyName} нанёс {damageEnemy} единиц урона игроку [{name}].\n");
                    isEnemyDoneStep = true;
                }

                isPlayerDoneStep = false;
                isEnemyDoneStep = false;

                if (playerHealth <= 0 || healthEnemy <= 0)
                {
                    isRunCombat = false;
                }

                playerMana += restorationManaByStep;

                Console.WriteLine(continueMessage);
                Console.ReadKey();
            }

            if (playerHealth <= 0 && healthEnemy <= 0)
            {
                Console.WriteLine(drowInBattleMessage);
            }
            else if (playerHealth <= 0)
            {
                Console.WriteLine(enemyVictoryMessage);
            }
            else if (healthEnemy <= 0)
            {
                Console.WriteLine(playerVictoryMessage);
            }

            Console.WriteLine(continueMessage);
            Console.ReadKey();
        }
    }
}