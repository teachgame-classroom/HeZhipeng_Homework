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

        public static bool IsRight(int odds)
        {
            bool isEncounter_1 = GetRandom(1, 100) <= odds;
            bool isEncounter_2 = GetRandom(1, 100) <= odds;
            if (isEncounter_1 && isEncounter_2)
            {
                return true;
            }
            else
            {
                return GetRandom(1, 100) <= odds;
            }
        }

        public static bool IsRight(float odds)
        {
            bool isEncounter_1 = GetRandom(1f, 100f) <= odds;
            bool isEncounter_2 = GetRandom(1f, 100f) <= odds;
            if (isEncounter_1 && isEncounter_2)
            {
                return true;
            }
            else
            {
                return GetRandom(1f, 100f) <= odds;
            }
        }
    }
}
