using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_190322
{
    class Game
    {
        #region 公共属性
        // 输入按键信息对象
        private ConsoleKeyInfo inputKey;
        // 游戏模式标识 0:未选定 1:闯关模式 2:建造模式
        private int modelFlag = 0;
        // 游戏运行中标识
        public bool isRunning { get; private set; }
        #endregion 公共属性
        #region 闯关属性
        // 迷宫对象
        private Maze mazeStage;
        // 记分牌对象,负责读取,保存,记录玩家姓名和分数
        private Scordboard mazeSb;
        // 通过标识
        private bool isWon;
        #endregion 闯关属性
        #region 建造属性
        // 场地可用方块数组
        private char[] mazeBlocks = new char[] { Maze.WALL, Maze.ROAD, Maze.OBSTACLE, Maze.PLAYER, Maze.GOAL };
        // 建造场地对象
        private StageBuilder builder;
        // 唯一玩家表示
        private bool isOnlyOnePlayer = false;
        // 唯一终点标识
        private bool isOnlyOneGoal = false;
        #endregion 建造属性

        /// <summary>
        /// 游戏开始方法
        /// </summary>
        public void Start()
        {
            isRunning = true;
            Render();
        }
        /// <summary>
        /// 游戏循环
        /// </summary>
        public void Loop()
        {
            UpdateInput();
            UpdateLogic();
            Render();
        }
        /// <summary>
        /// 游戏结束方法
        /// </summary>
        public void End()
        {
            //Render();
        }
        /// <summary>
        /// 更新输入按键方法
        /// </summary>
        private void UpdateInput()
        {
            inputKey = Console.ReadKey(true);
        }
        /// <summary>
        /// 更新逻辑方法
        /// </summary>
        private void UpdateLogic()
        {
            switch (modelFlag)
            {
                case 0:
                    BranchLogic();
                    break;
                case 1:
                    PlayLogic();
                    break;
                case 2:
                    BuildLogic();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 渲染方法
        /// </summary>
        private void Render()
        {
            switch (modelFlag)
            {
                case 0:
                    BranchRender();
                    break;
                case 1:
                    PlayRender();
                    break;
                case 2:
                    BuildRender();
                    break;
                default:
                    break;
            }
        }

        #region 指定模式方法
        /// <summary>
        /// 未确定模式逻辑方法
        /// </summary>
        private void BranchLogic()
        {
            switch (inputKey.Key)
            {
                case ConsoleKey.P:
                    PlayStart();
                    modelFlag = 1;
                    break;
                case ConsoleKey.B:
                    BuildStart();
                    modelFlag = 2;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 未确定模式的渲染方法
        /// </summary>
        private void BranchRender()
        {
            Console.Clear();
            Console.WriteLine("请选择游戏模式:");
            Console.WriteLine("P-闯关, B-建造");
        }
        #endregion 指定模式方法

        #region 闯关方法
        /// <summary>
        /// 闯关模式的开始方法
        /// </summary>
        private void PlayStart()
        {
            Console.WriteLine("请输入名字:");
            string playerName = Console.ReadLine();
            mazeSb = new Scordboard(playerName);
            mazeStage = new Maze();
            isWon = false;
        }
        /// <summary>
        /// 闯关模式的逻辑方法
        /// </summary>
        private void PlayLogic()
        {
            switch (inputKey.Key)
            {
                case ConsoleKey.W:
                    mazeStage.PlayerMove(mazeStage.playerPosX, mazeStage.playerPosY - 1);
                    break;
                case ConsoleKey.S:
                    mazeStage.PlayerMove(mazeStage.playerPosX, mazeStage.playerPosY + 1);
                    break;
                case ConsoleKey.A:
                    mazeStage.PlayerMove(mazeStage.playerPosX - 1, mazeStage.playerPosY);
                    break;
                case ConsoleKey.D:
                    mazeStage.PlayerMove(mazeStage.playerPosX + 1, mazeStage.playerPosY);
                    break;
                case ConsoleKey.Escape:
                    mazeSb.SaveScore();
                    isRunning = false;
                    break;
                case ConsoleKey.R:
                    mazeStage.Reset();
                    isWon = false;
                    break;
                default:
                    break;
            }

            bool isGoal = IsGoal();
            bool hasNext = mazeStage.HasNext();
            if (!isWon && isGoal && hasNext)
            {
                mazeSb.score += 10;
                mazeStage.NextLevel();
            }
            else if (!isWon && isGoal && !hasNext)
            {
                mazeSb.score += 10;
                isWon = true;
            }
        }
        /// <summary>
        /// 闯关模式的渲染方法
        /// </summary>
        private void PlayRender()
        {
            // 按下移动键但实际没有移动时,不刷新
            if ((ConsoleKey.W.Equals(inputKey.Key)
                || ConsoleKey.S.Equals(inputKey.Key)
                || ConsoleKey.A.Equals(inputKey.Key)
                || ConsoleKey.D.Equals(inputKey.Key))
                && !mazeStage.isMoved)
            {
                return;
            }
            Console.Clear();
            if (mazeSb.isNewPlayer)
            {
                Console.WriteLine("{0}欢迎进入游戏,{1}为终点,移动到终点则胜利", mazeSb.playerName, Maze.GOAL);
            }
            else
            {
                Console.WriteLine("{0}欢迎再次回来", mazeSb.playerName);
            }
            Console.WriteLine("WASD-控制方向, R-重置本关, ESC-退出");
            Console.WriteLine("你当前成绩为{0}", mazeSb.score);
            mazeStage.Display();
            if (isWon)
            {
                Console.WriteLine("恭喜你赢了");
            }
            else if (!isWon && !isWon && IsGoal() && mazeStage.HasNext())
            {
                Console.WriteLine("进入下一关");
                System.Threading.Thread.Sleep(1000);
            }
            switch (inputKey.Key)
            {
                case ConsoleKey.Escape:
                    Console.WriteLine("已为你保存成绩");
                    Console.WriteLine("感谢你的使用");
                    break;
                case ConsoleKey.R:
                    Console.WriteLine("重置完毕");
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 判断抵达终点
        /// </summary>
        /// <returns></returns>
        private bool IsGoal()
        {
            return mazeStage.goalPosX == mazeStage.playerPosX && mazeStage.goalPosY == mazeStage.playerPosY;
        }
        #endregion 闯关方法

        #region 建造方法
        /// <summary>
        /// 建造模式的开始方法
        /// </summary>
        private void BuildStart()
        {
            Console.WriteLine("请依次输入地图的宽高(取值范围(0, 20], 建议范围[8, 10]):");
            string inputLine = string.Empty;
            int row = 0;
            int col = 0;
            while (!int.TryParse(inputLine, out col) || col <= 0 || col > 20)
            {
                Console.Write("宽度(列数): ");
                inputLine = Console.ReadLine();
            }
            inputLine = string.Empty;
            while (!int.TryParse(inputLine, out row) || row <= 0 || row > 20)
            {
                Console.Write("高度(行数): ");
                inputLine = Console.ReadLine();
            }
            builder = new StageBuilder(row, col, Maze.ROAD);
            Console.WriteLine("地图初始完成,请开始编辑");
        }
        /// <summary>
        /// 建造模式的逻辑方法
        /// </summary>
        private void BuildLogic()
        {
            switch (inputKey.Key)
            {
                case ConsoleKey.W:
                    builder.CursorMove(builder.cursorPosX, builder.cursorPosY - 1);
                    break;
                case ConsoleKey.S:
                    builder.CursorMove(builder.cursorPosX, builder.cursorPosY + 1);
                    break;
                case ConsoleKey.A:
                    builder.CursorMove(builder.cursorPosX - 1, builder.cursorPosY);
                    break;
                case ConsoleKey.D:
                    builder.CursorMove(builder.cursorPosX + 1, builder.cursorPosY);
                    break;
                case ConsoleKey.UpArrow:
                    ChangeBlock(-1);
                    break;
                case ConsoleKey.DownArrow:
                    ChangeBlock(1);
                    break;
                case ConsoleKey.Escape:
                    isOnlyOnePlayer = IsOnlyOnePlayer();
                    isOnlyOneGoal = IsOnlyOneGoal();
                    if (isOnlyOneGoal && isOnlyOnePlayer)
                    {
                        builder.Save();
                        modelFlag = 0;
                    }
                    break;
                case ConsoleKey.R:
                    builder.Reset();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 建造模式的渲染方法
        /// </summary>
        private void BuildRender()
        {
            // 按下移动键但实际没有移动时,不刷新
            if ((ConsoleKey.W.Equals(inputKey.Key)
                || ConsoleKey.S.Equals(inputKey.Key)
                || ConsoleKey.A.Equals(inputKey.Key)
                || ConsoleKey.D.Equals(inputKey.Key))
                && !builder.isMoved)
            {
                return;
            }
            Console.Clear();
            Console.WriteLine("WASD-控制方向, ↑↓-切换场地模块, R-重置地图, ESC-退出并保存");
            Console.WriteLine("当前位置({0}, {1}) {2}", builder.cursorPosX, builder.cursorPosY, builder.GetBlock());
            builder.Display();
            switch (inputKey.Key)
            {
                case ConsoleKey.Escape:
                    if (isOnlyOneGoal && isOnlyOnePlayer)
                    {
                        Console.WriteLine("已为你保存地图");
                    }
                    else if (!isOnlyOnePlayer)
                    {
                        Console.WriteLine("只能存在一个玩家,请去除多余玩家");
                    }
                    else if (!isOnlyOneGoal)
                    {
                        Console.WriteLine("只能存在一个终点,请去除多余终点");
                    }
                    break;
                case ConsoleKey.R:
                    Console.WriteLine("重置完毕");
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 切换场地方块
        /// </summary>
        /// <param name="offset">切换的偏移值,可为负数</param>
        private void ChangeBlock(int offset)
        {
            int index = IndexOf(builder.GetBlock());
            int nextIndex = (mazeBlocks.Length + index + offset) % mazeBlocks.Length;
            builder.SetBlock(mazeBlocks[nextIndex]);
        }
        /// <summary>
        /// 获取场地方块位于方块数组(mazeBlocks)的索引
        /// </summary>
        /// <param name="block">待查找的方块</param>
        /// <returns></returns>
        private int IndexOf(char block)
        {
            int ret = 0;
            for(int i = 0; i < mazeBlocks.Length; i++)
            {
                if(mazeBlocks[i] == block)
                {
                    ret = i;
                }
            }
            return ret;
        }
        /// <summary>
        /// 判断是否有且只有一个玩家
        /// </summary>
        /// <returns></returns>
        private bool IsOnlyOnePlayer()
        {
            bool onlyOnePlayer = false;
            int playerNum = 0;
            for(int i = 0; i < builder.stageCharSet.Length; i++)
            {
                if(builder.stageCharSet[i] == Maze.PLAYER)
                {
                    playerNum++;
                }
                if(playerNum > 1)
                {
                    return onlyOnePlayer;
                }
            }
            if(playerNum == 1)
            {
                onlyOnePlayer = true;
            }

            return onlyOnePlayer;
        }
        /// <summary>
        /// 判断是否有且只有一个终点
        /// </summary>
        /// <returns></returns>
        private bool IsOnlyOneGoal()
        {
            bool onlyOneGoal = false;
            int goalNum = 0;
            for (int i = 0; i < builder.stageCharSet.Length; i++)
            {
                if (builder.stageCharSet[i] == Maze.GOAL)
                {
                    goalNum++;
                }
                if (goalNum > 1)
                {
                    return onlyOneGoal;
                }
            }
            if (goalNum == 1)
            {
                onlyOneGoal = true;
            }

            return onlyOneGoal;
        }
        #endregion 建造方法
    }
}
