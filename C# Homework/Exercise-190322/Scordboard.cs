using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_190322
{
    /// <summary>
    /// 记分牌类型,用于用于读写,记录用户名称和积分
    /// </summary>
    class Scordboard
    {
        // 声明计分文件路径
        public const string SCORE_PATH = "./Scores.txt";
        // 声明分数记录格式
        public const string SCORE_FORMAT = "{0},{1}";
        // 玩家名字,默认为Player
        public string playerName { get; set; } = "Player";
        // 玩家分数,默认为0
        public int score { get; set; } = 0;
        // 新玩家标识
        public bool isNewPlayer { get; private set; } = true;
        // 初始一个长度,避免找不到文件时影响程序
        private string[] scoreLines = new string[1];
        // 玩家分数记录所在(位于scoreLines的)索引
        private int scoreIndex = 0;
        // 记录初始成绩
        private int startScore = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        public Scordboard(string name)
        {
            playerName = name;
            ReadScores();
        }
        /// <summary>
        /// 读取历史成绩
        /// </summary>
        private void ReadScores()
        {
            if (File.Exists(SCORE_PATH))
            {
                scoreLines = File.ReadAllLines(SCORE_PATH);
                for (int i = 0; i < scoreLines.Length; i++)
                {
                    if (scoreLines[i].StartsWith(playerName))
                    {
                        score = int.Parse(scoreLines[i].Split(',')[1]);
                        startScore = score;
                        scoreIndex = i;
                        isNewPlayer = false;
                        break;
                    }
                }

                // 循环结束 若是新玩家,则需要扩展scoreLines
                if (isNewPlayer)
                {
                    string[] tempLinse = new string[scoreLines.Length + 1];
                    for(int i = 0; i < scoreLines.Length; i++)
                    {
                        tempLinse[i] = scoreLines[i];
                    }
                    scoreLines = tempLinse;
                    scoreIndex = scoreLines.Length - 1;
                }
            }
        }
        /// <summary>
        /// 保存成绩
        /// </summary>
        public void SaveScore()
        {
            // 成绩有变化时才保存
            if (startScore != score)
            {
                // 若没有成绩文件,则创建一个
                if (!File.Exists(SCORE_PATH))
                {
                    File.Create(SCORE_PATH).Close();
                }
                scoreLines[scoreIndex] = string.Format(SCORE_FORMAT, playerName, score);
                File.WriteAllLines(SCORE_PATH, scoreLines);
            }
        }
        /// <summary>
        /// 判断指定玩家是否为新玩家
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsNewPlayer(string name)
        {
            bool ret = true;
            if (File.Exists(SCORE_PATH))
            {
                string[] scoreLines = File.ReadAllLines(SCORE_PATH);
                for (int i = 0; i < scoreLines.Length; i++)
                {
                    if (scoreLines[i].StartsWith(name))
                    {
                        ret = false;
                        break;
                    }
                }
            }
            return ret;
        }
    }
}
