using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLoopExercise_Hezhipeng
{
    public class Display
    {
        private const string PROGRESS_MESSAGE = "游戏进度:{0}/{1}";
        private const string NOT_FIGHTING_INPUT_MESSAGE = "1-前进,2-休息";
        private const string ENCOUNTER_MESSAGE_0 = "被{0}盯上了,进入战斗";
        private const string ENCOUNTER_NUMBER_MESSAEG = "{0}只{1},";
        private const string ENCOUNTER_MESSAGE_1 = "被一只{0}盯上了,进入战斗";
        private const string ENCOUNTER_MESSAGE_2 = "被{0}只{1}盯上了,进入战斗";
        private const string ENCOUNTER_MESSAGE_3 = "被一群{0}盯上了,进入战斗";
        private const string FIGHTING_MESSAGE = "正在战斗\n{0}\n你还有{1}点血,请选择";
        private const string FIGHTING_ENEMY_HP_MESSAGE = "{0}还有{1}点血\n";
        private const string FIGHTING_INPUT_MESSAGE = "1-攻击,2-逃走";
        private const string RUN_AWAY_SUCCESS_MESSAGE = "逃跑成功";
        private const string RUN_AWAY_DEFEATED_MESSAGE = "逃跑失败";
        private const string END_FIGHTING_MESSAGE = "战斗结束";
        private const string GAME_OVER_MESSAGE = "游戏结束";

        #region 打印信息
        public static void ProgressMessage(int progress, int progressAmount)
        {
            Console.WriteLine(PROGRESS_MESSAGE, progress, progressAmount);
        }

        public static void InputMessage(bool isFighting)
        {
            if (!isFighting)
            {
                Console.WriteLine(NOT_FIGHTING_INPUT_MESSAGE);
            }
            else
            {
                Console.WriteLine(FIGHTING_INPUT_MESSAGE);
            }
        }

        public static void EncounterMessage(List<Charactar> enemys)
        {
            Console.WriteLine(ENCOUNTER_MESSAGE_0, GetEnemysMessage(enemys));
        }

        public static void FightingMessage(List<Charactar> enemys, Player player)
        {
            string enemyHpMessage = string.Empty;
            foreach (Charactar enemy in enemys)
            {
                enemyHpMessage += string.Format(FIGHTING_ENEMY_HP_MESSAGE, enemy.name, enemy.GetHP());
            }
            enemyHpMessage = enemyHpMessage.Substring(0, enemyHpMessage.Length - 1);
            Console.WriteLine(FIGHTING_MESSAGE, enemyHpMessage, player.GetHP());
        }

        public static void EndFightingMessage()
        {
            Console.WriteLine(END_FIGHTING_MESSAGE);
        }

        public static void RunAwaySuccessMessage()
        {
            Console.WriteLine(RUN_AWAY_SUCCESS_MESSAGE);
        }

        public static void RunAwayDefeatedMessage()
        {
            Console.WriteLine(RUN_AWAY_DEFEATED_MESSAGE);
        }

        public static void GameOverMessage()
        {
            Console.WriteLine(GAME_OVER_MESSAGE);
        }

        public static void UINotFighting(int prog, int progAmount, Player player)
        {
            Console.WriteLine("------------------------游戏进程{0}/{1}------------------------", prog, progAmount);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                     {0} [HP:{1}/{2}]", player.name, player.GetHP(), player.GetMaxHP());
            Console.WriteLine();
            Console.WriteLine("┌──────────────────────────────┐");
            Console.WriteLine("│                    1-前进 2-休息 0-退出                    │");
            Console.WriteLine("└──────────────────────────────┘");
        }

        public static void UIEncounterEnemy(int prog, int progAmount, Player player, List<Charactar> enemys)
        {
            GetEnemysMessage(enemys);
            Console.WriteLine("------------------------游戏进程{0}/{1}------------------------", prog, progAmount);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                 {0}", string.Format(ENCOUNTER_MESSAGE_0, GetEnemysMessage(enemys)));
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                     {0} [HP:{1}/{2}]", player.name, player.GetHP(), player.GetMaxHP());
            Console.WriteLine();
            Console.WriteLine("┌──────────────────────────────┐");
            Console.WriteLine("│                    1-前进 2-休息 0-退出                    │");
            Console.WriteLine("└──────────────────────────────┘");
        }

        private static string GetEnemysMessage(List<Charactar> enemys)
        {
            List<string> enemyNames = new List<string>();
            Dictionary<string, int> enemyNameNumber = new Dictionary<string, int>();
            foreach (Charactar enemy in enemys)
            {
                bool isExistingName = enemyNames.Contains(enemy.name);
                if (isExistingName)
                {
                    enemyNameNumber[enemy.name]++;
                }
                else
                {
                    enemyNames.Add(enemy.name);
                    enemyNameNumber.Add(enemy.name, 1);
                }
            }

            //
            string enemysMessage = string.Empty;
            foreach (string enemyName in enemyNames)
            {
                enemysMessage += string.Format(ENCOUNTER_NUMBER_MESSAEG, enemyNameNumber[enemyName], enemyName);
            }
            //
            enemysMessage = enemysMessage.Substring(0, enemysMessage.Length - 1);
            return enemysMessage;
        }

        public static void UIInFighting(int prog, int progAmount, Player player, List<Charactar> enemys)
        {
            Console.WriteLine("------------------------游戏进程{0}/{1}------------------------", prog, progAmount);
            Console.WriteLine();
            Console.Write("                     ");
            foreach(Charactar enemy in enemys)
            {
                Console.Write("{0} [HP:{1}/{2}]", enemy.name, enemy.GetHP(), enemy.GetMaxHP());
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("  {0} [HP:{1}/{2}]", player.name, player.GetHP(), player.GetMaxHP());
            Console.WriteLine();
            Console.WriteLine("┌──────────────────────────────┐");
            Console.WriteLine("│                    1-攻击 2-逃跑 0-退出                    │");
            Console.WriteLine("└──────────────────────────────┘");
        }

        public static void UIEndFighting(int prog, int progAmount, Player player)
        {
            Console.WriteLine("------------------------游戏进程{0}/{1}------------------------", prog, progAmount);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                  战斗结束,为你恢复所有血量");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                     {0} [HP:{1}/{2}]", player.name, player.GetHP(), player.GetMaxHP());
            Console.WriteLine();
            Console.WriteLine("┌──────────────────────────────┐");
            Console.WriteLine("│                    1-前进 2-休息 0-退出                    │");
            Console.WriteLine("└──────────────────────────────┘");
        }

        public static void UIRunAway(int prog, int progAmount, Player player, List<Charactar> enemys, bool isSuccess)
        {
            Console.WriteLine("------------------------游戏进程{0}/{1}------------------------", prog, progAmount);
            Console.WriteLine();
            Console.Write("                     ");
            foreach (Charactar enemy in enemys)
            {
                Console.Write("{0} [HP:{1}/{2}]", enemy.name, enemy.GetHP(), enemy.GetMaxHP());
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                            {0}", isSuccess ? "逃跑成功": "逃跑失败");
            Console.WriteLine();
            Console.WriteLine("  {0} [HP:{1}/{2}]", player.name, player.GetHP(), player.GetMaxHP());
            Console.WriteLine();
            Console.WriteLine("┌──────────────────────────────┐");
            Console.WriteLine("│                    1-攻击 2-逃跑 0-退出                    │");
            Console.WriteLine("└──────────────────────────────┘");
        }

        public static void UIGameOver(int prog, int progAmount)
        {
            Console.WriteLine("------------------------游戏进程{0}/{1}------------------------", prog, progAmount);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                           GAME OVER");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("┌──────────────────────────────┐");
            Console.WriteLine("│                           0-退出                           │");
            Console.WriteLine("└──────────────────────────────┘");
        }
        #endregion 打印信息
    }
}
