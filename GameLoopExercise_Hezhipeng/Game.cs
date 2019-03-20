using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GameLoopExercise_Hezhipeng
{
    public class Game
    {
        // 定义游戏进程场景输入
        private const ConsoleKey GO_ON_1 = ConsoleKey.D1;
        private const ConsoleKey GO_ON_2 = ConsoleKey.NumPad1;
        private const ConsoleKey STOP_1 = ConsoleKey.D2;
        private const ConsoleKey STOP_2 = ConsoleKey.NumPad2;
        // 定义战斗场景输入
        private const ConsoleKey ATTACT_1 = ConsoleKey.D1;
        private const ConsoleKey ATTACT_2 = ConsoleKey.NumPad1;
        private const ConsoleKey RUN_1 = ConsoleKey.D2;
        private const ConsoleKey RUN_2 = ConsoleKey.NumPad2;
        // 定义退出游戏输入
        private const ConsoleKey EXIT_1 = ConsoleKey.D0;
        private const ConsoleKey EXIT_2 = ConsoleKey.NumPad0;
        // 定义游戏状态标示
        private const int NOT_STATE = -1;
        private const int GAME_OVER = 0;
        private const int NOT_FIGHTING = 1;
        private const int ENCOUNTER_ENEMY = 2;
        private const int FIGHTING = 3;
        private const int END_FIGHTING = 4;
        //// 定义逃跑状态标示
        private const int RUN_AWAY_SUCCESS = 1;
        private const int RUN_AWAY_DEFEATED = 0;

        // 休眠时长(ms)
        private const int SLEEP_TIME = 100;
        // 渲染线程
        private Thread renderThread;
        // 读取输入线程
        private Thread gameplayThread;

        // 游戏状态
        private int gameState;
        // 逃跑状态
        private int runAwayState;

        private ConsoleKeyInfo inputKey;

        // 进程总数
        public int progressAmount = 20;
        // 当前进程
        public int progress = 0;

        public Player player_1;

        public List<Charactar> enemys;

        public void Start()
        {
            player_1 = new Player("Silver");
            gameState = NOT_FIGHTING;
            runAwayState = NOT_STATE;

            renderThread = new Thread(RenderLoop);
            renderThread.Start();

            gameplayThread = new Thread(UpdateGameplayLoop);
            gameplayThread.Start();
        }

        public void Update()
        {
            UpdateInput();
        }

        private void UpdateGameplayLoop()
        {
            while (IsRunning())
            {
                UpdateGameplay();
                Thread.Sleep(SLEEP_TIME);
            }
        }

        private void UpdateInput()
        {
            inputKey = Console.ReadKey(true);
        }

        private void UpdateGameplay()
        {
            Action();
            IsGameOver();
            inputKey = new ConsoleKeyInfo();
        }

        private void RenderLoop()
        {
            while (IsRunning())
            {
                Render();
                Thread.Sleep(SLEEP_TIME);
            }
        }

        private void Render()
        {
            Console.Clear();
            switch (gameState)
            {
                case GAME_OVER:
                    Display.UIGameOver(progress, progressAmount);
                    break;
                case NOT_FIGHTING:
                    Display.UINotFighting(progress, progressAmount, player_1);
                    break;
                case ENCOUNTER_ENEMY:
                    Display.UIEncounterEnemy(progress, progressAmount, player_1, enemys);
                    Thread.Sleep(1000);
                    break;
                case FIGHTING:
                    if (player_1 != null && !player_1.IsDea() && enemys != null && enemys.Count > 0)
                    {
                        if (runAwayState == NOT_STATE)
                        {
                            Display.UIInFighting(progress, progressAmount, player_1, enemys);
                        }
                        else
                        {
                            Display.UIRunAway(progress, progressAmount, player_1, enemys, runAwayState == RUN_AWAY_SUCCESS);
                            Thread.Sleep(1000);
                        }
                    }
                    break;
                case END_FIGHTING:
                    Display.UIEndFighting(progress, progressAmount, player_1);
                    Thread.Sleep(1000);
                    break;
                default:
                    break;
            }
        } 

        public void End()
        {
            gameplayThread.Abort();
            renderThread.Abort();
            Console.WriteLine("游戏结束");
        }

        public bool IsRunning()
        {
            return !EXIT_1.Equals(inputKey.Key) && !EXIT_2.Equals(inputKey.Key);
        }

        private void Action()
        {
            switch (gameState)
            {
                case NOT_FIGHTING:
                    NotFighting();
                    break;
                case ENCOUNTER_ENEMY:
                    gameState = FIGHTING;
                    break;
                case FIGHTING:
                    Fighting();
                    break;
                case END_FIGHTING:
                    player_1.RecoverHP(player_1.GetMaxHP());
                    Thread.Sleep(1000);
                    gameState = NOT_FIGHTING;
                    break;
                default:
                    break;
            }
        }

        private bool IsGoal()
        {
            return progressAmount > 0 && progress >= progressAmount;
        }

        private bool IsGameOver()
        {
            bool isGameOver = IsGoal() || player_1 == null || player_1.IsDea();
            if (isGameOver)
            {
                gameState = GAME_OVER;
            }
            return isGameOver;
        }

        private void NotFighting()
        {
            if (GO_ON_1.Equals(inputKey.Key) || GO_ON_2.Equals(inputKey.Key))
            {
                progress++;
                if (IsEncounterEnemy(true))
                {
                    EncounterEnemy();
                }
            }
            else if (STOP_1.Equals(inputKey.Key) || STOP_2.Equals(inputKey.Key))
            {
                if (IsEncounterEnemy(false))
                {
                    EncounterEnemy();
                }
                else
                {
                    player_1.RecoverHP();
                    player_1.RecoverMP();
                }
            }
        }

        private void Fighting()
        {
            if (enemys != null && enemys.Count > 0 && player_1 != null && !player_1.IsDea())
            {
                if (ATTACT_1.Equals(inputKey.Key) || ATTACT_2.Equals(inputKey.Key))
                {
                    // 玩家攻击
                    Fight(player_1, player_1.skills[0], enemys[0]);

                    // 怪物攻击
                    foreach (Charactar enemy in enemys)
                    {
                        Fight(enemy, enemy.skills[0], player_1);
                    }

                }
                else if (RUN_1.Equals(inputKey.Key) || RUN_2.Equals(inputKey.Key))
                {
                    if (!IsRunAway())
                    {
                        // 怪物攻击
                        foreach (Charactar enemy in enemys)
                        {
                            enemy.Attack(player_1, enemy.skills[0]);
                        }
                    }
                }
            }
        }

        private bool IsEncounterEnemy(bool isProgChange)
        {
            //前进遇敌几率>与休息遇敌几率
            if (isProgChange)
            {
                int encounterOdds = 100;
                return IsRight(encounterOdds);
            }
            else
            {
                int encounterOdds = 30;
                return IsRight(encounterOdds);
            }
        }

        private void EncounterEnemy()
        {
            CreatEnemy();
            gameState = ENCOUNTER_ENEMY;
            Thread.Sleep(1000);
        }

        private void CreatEnemy()
        {
            int enemyCount = 1;
            int randomNum = Tools.GetRandom(1, 100);
            if (randomNum >= 90)
            {
                enemyCount = 3;
            }
            else if (randomNum >= 50)
            {
                enemyCount = 2;
            }
            else
            {
                enemyCount = 1;
            }
            
            enemys = new List<Charactar>();
            for (int i = 0; i < enemyCount; i++)
            {
                enemys.Add(new Goblin());
            }
        }

        private void Fight(Charactar sponsor, Skill userSkill, Charactar target)
        {
            if (sponsor != null && userSkill != null && target != null)
            {
                sponsor.Attack(target, userSkill);
                ClearCharactars();
            }
        }

        private void ClearCharactars()
        {
            // 清除死亡敌人
            for (int i = 0; i < enemys.Count; i++)
            {
                if (enemys[i].IsDea())
                {
                    enemys.Remove(enemys[i]);
                }
            }

            if (enemys == null || enemys.Count <= 0 || player_1 == null || player_1.IsDea())
            {
                gameState = END_FIGHTING;
            }
        }

        private bool IsRunAway()
        {
            float runAwayOdds = 90f / (float)enemys.Count;
            bool isRunAway = IsRight(runAwayOdds);

            if (isRunAway)
            {
                runAwayState = RUN_AWAY_SUCCESS;
                Thread.Sleep(1000);
                gameState = NOT_FIGHTING;
            }
            else
            {
                runAwayState = RUN_AWAY_DEFEATED;
                Thread.Sleep(1000);
                gameState = FIGHTING;
            }
            runAwayState = NOT_STATE;
            return isRunAway;
        }

        private bool IsRight(int odds)
        {
            bool isEncounter_1 = Tools.GetRandom(1, 100) <= odds;
            bool isEncounter_2 = Tools.GetRandom(1, 100) <= odds;
            if (isEncounter_1 && isEncounter_2)
            {
                return true;
            }
            else
            {
                return Tools.GetRandom(1, 100) <= odds;
            }
        }

        private bool IsRight(float odds)
        {
            bool isEncounter_1 = Tools.GetRandom(1f, 100f) <= odds;
            bool isEncounter_2 = Tools.GetRandom(1f, 100f) <= odds;
            if (isEncounter_1 && isEncounter_2)
            {
                return true;
            }
            else
            {
                return Tools.GetRandom(1f, 100f) <= odds;
            }
        }
    }
}
