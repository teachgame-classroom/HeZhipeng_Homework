using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLoopExercise_Hezhipeng
{
    public class Tools
    {
        public static int GetRandom(int min, int max)
        {
            int result = int.MinValue;
            Random random = new Random(Guid.NewGuid().GetHashCode());
            double randomNum = random.NextDouble();
            int dis = max - min + 1;
            result = (int)(dis * randomNum + min);
            result = result > max ? max : result;
            return result;
        }

        public static float GetRandom(float min, float max)
        {
            float result = float.MinValue;
            Random random = new Random(Guid.NewGuid().GetHashCode());
            double randomNum = random.NextDouble();
            float dis = max - min + 1f;
            result = (float)(dis * randomNum + min);
            result = result > max ? max : result;
            return result;
        }
    }
}
