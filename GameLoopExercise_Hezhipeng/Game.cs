using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLoopExercise_Hezhipeng
{
    public class Game
    {
        // 定义游戏进程场景输入
        //private const string GO_ON = "1";
        //private const string STOP = "2";
        private const ConsoleKey GO_ON_1 = ConsoleKey.D1;
        private const ConsoleKey GO_ON_2 = ConsoleKey.NumPad1;
        private const ConsoleKey STOP_1 = ConsoleKey.D2;
        private const ConsoleKey STOP_2 = ConsoleKey.NumPad2;
        // 定义战斗场景输入
        //private const string ATTACT = "1";
        //private const string RUN = "2";
        private const ConsoleKey ATTACT_1 = ConsoleKey.D1;
        private const ConsoleKey ATTACT_2 = ConsoleKey.NumPad1;
        private const ConsoleKey RUN_1 = ConsoleKey.D2;
        private const ConsoleKey RUN_2 = ConsoleKey.NumPad2;
        // 定义退出游戏输入
        //private const string EXIT = "0";
        private const ConsoleKey EXIT_1 = ConsoleKey.D0;
        private const ConsoleKey EXIT_2 = ConsoleKey.NumPad0;

        // 用户输入
        protected string input { get; set; }
        private ConsoleKeyInfo inputKey;

        // 游戏运行标示
        protected bool isRunning { get; set; }

        // 进程总数
        public int progressAmount = 20;
        // 当前进程
        public int progress = 0;

        public Player player_1;

        public List<Charactar> enemys;

        public bool isGameOver;

        public bool isFighting;
        // 进程改变标示
        private bool isProgChange;


        public void Start()
        {
            player_1 = new Player("Silver");
            isRunning = true;
            isFighting = false;
            //Console.WriteLine("游戏开始");
            //Display.InputMessage(isFighting);
        }

        public void Update()
        {
            UpdateInput();
            UpdateGameplay();
        }

        private void UpdateInput()
        {
            //input = Console.ReadLine();
            inputKey = Console.ReadKey(true);
        }

        private void UpdateGameplay()
        {
            isRunning = !EXIT_1.Equals(inputKey.Key) && !EXIT_2.Equals(inputKey.Key);
            Action();
        }

        public void Render()
        {
            Console.Clear();
            if (isGameOver)
            {
                Display.UIGameOver(progress, progressAmount);
            }
            else if (isFighting)
            {
                if (enemys != null && enemys.Count > 0)
                {
                    Display.UIInFighting(progress, progressAmount, player_1, enemys);
                }
                else
                {

                }
            }
            else if (!isFighting)
            {
                Display.UINotFighting(progress, progressAmount, player_1);
            } 
        } 

        public void End()
        {
            Console.WriteLine("游戏结束");
        }

        public bool IsRunning()
        {
            return isRunning;
        }

        private void Action()
        {
            if (!isFighting && (GO_ON_1.Equals(inputKey.Key) || GO_ON_2.Equals(inputKey.Key)))
            {
                progress++;
                //Display.ProgressMessage(progress, progressAmount);
                isProgChange = true;
                if (IsEncounterEnemy())
                {
                    EncounterEnemy();
                }
            }
            else if (!isFighting && (STOP_1.Equals(inputKey.Key) || STOP_2.Equals(inputKey.Key)))
            {
                //Display.ProgressMessage(progress, progressAmount);
                isProgChange = false;
                if (IsEncounterEnemy())
                {
                    EncounterEnemy();
                }
                else
                {
                    player_1.RecoverHP();
                    player_1.RecoverMP();
                }
            }
            else if (isFighting && (ATTACT_1.Equals(inputKey.Key) || ATTACT_2.Equals(inputKey.Key)))
            {
                if (enemys != null && enemys.Count > 0)
                {
                    // 玩家攻击
                    player_1.Attack(enemys[0], player_1.skills[0]);
                    ClearingPlayerAction();

                    // 怪物攻击
                    for (int i = 0; i < enemys.Count; i++)
                    {
                        enemys[i].Attack(player_1, enemys[i].skills[0]);
                    }
                    ClearingEnemyAction();

                    if (isFighting)
                    {
                        Display.FightingMessage(enemys, player_1);
                    }
                    else
                    {
                        Display.EndFightingMessage();
                    }
                }
            }
            else if (isFighting && (RUN_1.Equals(inputKey.Key) || RUN_2.Equals(inputKey.Key)))
            {
                if (enemys != null && enemys.Count > 0)
                {
                    if (IsRunAway())
                    {
                        isFighting = false;
                        Display.RunAwaySuccessMessage();
                    }
                    else
                    {
                        Display.RunAwayDefeatedMessage();
                        // 怪物攻击
                        foreach (Charactar enemy in enemys)
                        {
                            enemy.Attack(player_1, enemy.skills[0]);
                        }
                        ClearingEnemyAction();
                    }

                    if (isFighting)
                    {
                        Display.FightingMessage(enemys, player_1);
                    }
                    else
                    {
                        Display.EndFightingMessage();
                    }
                }
            }
            if (!isGameOver)
            {
                Display.InputMessage(isFighting);
            }
        }

        private bool IsEncounterEnemy()
        {
            //前进遇敌几率>与休息遇敌几率
            if (isProgChange)
            {
                //int encounterOdds = 70;
                int encounterOdds = 100;
                return IsRight(encounterOdds);
            }
            else
            {
                int encounterOdds = 30;
                //int encounterOdds = 100;
                //int encounterOdds = 0;
                return IsRight(encounterOdds);
            }
        }

        private void EncounterEnemy()
        {
            CreatEnemy();
            isFighting = true;
            Display.EncounterMessage(enemys);
            Display.FightingMessage(enemys, player_1);
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

        private void ClearingPlayerAction()
        {
            for(int i = 0; i<enemys.Count; i++)
            {
                if (enemys[i].IsDea())
                {
                    enemys.Remove(enemys[i]);
                }
            }

            if (enemys.Count <= 0)
            {
                isFighting = false;
            }
        }

        private void ClearingEnemyAction()
        {
            if (player_1.IsDea())
            {
                isFighting = false;
                isGameOver = true;
                //isRunning = false;
            }
        }

        private bool IsRunAway()
        {
            float runAwayOdds = 90f / (float)enemys.Count;
            return IsRight(runAwayOdds);
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
