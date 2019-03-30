using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public Classfiy classfiy = Classfiy.CEREALS;
    public Color color = Color.WHITE;
    
    public enum Classfiy
    {
        // 肉类
        MEAT = 1,
        // 水果
        FRUIT = 2,
        // 蔬菜
        VEGETABLE = 3,
        // 谷物
        CEREALS = 4,
        // 垃圾食品
        JUNKFOOD = 5,
        // 甜品
        DESSERT = 6,
        // 菌类
        FUNGUS = 7
    }

    public enum Color
    {
        // 彩色
        COLORFUL = 0,
        // 白色
        WHITE = 1,
        // 红色
        RED = 2,
        // 绿色
        GREEN = 3,
        // 蓝色
        BLUE = 4,
        // 黄色
        YELLOW = 5,
        // 橘色
        ORANGE = 6,
        // 紫色
        PURPLE = 7,
        // 黑色
        BLACK = 8
    }
}
