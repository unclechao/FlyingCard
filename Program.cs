using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 骑士飞行棋
{
    class Program
    {
        //用于存储玩家姓名
        static string[] name = new string[2];  
        //用于存储玩家位置
        static int[] playerPos = { 0, 0 };
        //申请空间用于存储地图
        static int[] map = new int[100];
        static Boolean f = false;
        static Boolean[] isStop = { false, false };
        /// <summary>
        /// 程序主函数
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ShowTittle();
            Init();
            Console.Clear();
            ShowTittle();
            Console.WriteLine("对战开始...");
            Console.WriteLine("{0}的士兵用A表示",name[0]);
            Console.WriteLine("{0}的士兵用B表示", name[1]);
            Console.WriteLine("如果{0}和{1}在同一位置，用<>表示",name[0],name[1]);
            InitMap();
            DrawMap();
            Console.WriteLine();
            Console.WriteLine("开始游戏...");
            while (playerPos[0] < 99 && playerPos[1] < 99)
            {
                if (isStop[0] == false)
                {
                    //玩家A轮次
                    #region
                    Console.WriteLine("{0}按任意键开始掷骰子", name[0]);
                    Console.ReadKey(true);
                    Random r = new Random();
                    int x = r.Next(1, 7);    //x为投掷骰子所得数值
                    Console.WriteLine("{0}投出了{1}", name[0], x);
                    Console.WriteLine("按任意键开始行动");
                    Console.ReadKey(true);
                    playerPos[0] += x;
                    CheckPos();
                    //Todo:走到某位置应调用的函数
                    switch (map[playerPos[0]])
                    {
                        case 0:
                            break;
                        case 1: //玩家A走到幸运轮盘位置
                            ExcuteLuckTurn(name[0]);
                            f = false;  //重置标识符初始状态
                            break;
                        case 2: //玩家A走到地雷位置
                            Console.WriteLine("{0}踩到地雷...", name[0]);
                            Console.ReadKey(true);
                            EcxuteLandMine(name[0]);
                            break;
                        case 3: //玩家A走到休息一轮位置
                            Console.WriteLine("{0}太累了，休息一轮吧...", name[0]);
                            Console.ReadKey(true);
                            ExcutePause(0);
                            break;
                        case 4: //玩家A走到时空隧道位置
                            Console.WriteLine("{0}进入时空隧道...", name[0]);
                            Console.ReadKey(true);
                            ExcuteTimeTurnnel(name[0]);
                            break;
                    }
                    Console.Clear();
                    ShowTittle();
                    Console.WriteLine("对战开始...");
                    Console.WriteLine("{0}的士兵用A表示", name[0]);
                    Console.WriteLine("{0}的士兵用B表示", name[1]);
                    Console.WriteLine("如果{0}和{1}在同一位置，用<>表示", name[0], name[1]);
                    InitMap();
                    DrawMap();
                    Console.WriteLine();
                    Console.WriteLine("{0}所在的位置是{1}", name[0], playerPos[0] + 1);
                    Console.WriteLine("{0}所在的位置是{1}", name[1], playerPos[1] + 1);
                    #endregion
                }
                else
                {
                    isStop[0]=false;
                }

                if (isStop[1] == false)
                {
                    //玩家B轮次
                    #region
                    Console.WriteLine("{0}按任意键开始掷骰子", name[1]);
                    Console.ReadKey(true);
                    Random r = new Random();
                    int y = r.Next(1, 7);
                    Console.WriteLine("{0}投出了{1}", name[1], y);
                    Console.WriteLine("按任意键开始行动");
                    Console.ReadKey(true);
                    playerPos[1] += y;
                    CheckPos();
                    //Todo:走到某位置应调用的函数
                    switch (map[playerPos[1]])
                    {
                        case 0:
                            break;
                        case 1: //玩家A走到幸运轮盘位置
                            ExcuteLuckTurn(name[1]);
                            f = false;  //重置标识符初始状态
                            break;
                        case 2: //玩家A走到地雷位置
                            Console.WriteLine("{0}踩到地雷...", name[1]);
                            Console.ReadKey(true);
                            EcxuteLandMine(name[1]);
                            break;
                        case 3: //玩家A走到休息一轮位置
                            Console.WriteLine("{0}太累了，休息一轮吧...", name[1]);
                            Console.ReadKey(true);
                            ExcutePause(1);
                            break;
                        case 4: //玩家A走到时空隧道位置
                            Console.WriteLine("{0}进入时空隧道...", name[1]);
                            Console.ReadKey(true);
                            ExcuteTimeTurnnel(name[1]);
                            break;
                    }
                    Console.Clear();
                    ShowTittle();
                    Console.WriteLine("对战开始...");
                    Console.WriteLine("{0}的士兵用A表示", name[0]);
                    Console.WriteLine("{0}的士兵用B表示", name[1]);
                    Console.WriteLine("如果{0}和{1}在同一位置，用<>表示", name[0], name[1]);
                    InitMap();
                    DrawMap();
                    Console.WriteLine();
                    Console.WriteLine("{0}所在的位置是{1}", name[0], playerPos[0] + 1);
                    Console.WriteLine("{0}所在的位置是{1}", name[1], playerPos[1] + 1);
                    #endregion
                }
                else
                {
                    isStop[1] = false;
                }    
            }
            if (playerPos[0] > 99)
            {
                Console.WriteLine("{0}胜利了~~~", name[0]);
            }
            else
            {
                Console.WriteLine("{0}胜利了~~~", name[1]);
            }
            Console.ReadKey();
        }
        /// <summary>
        /// 绘制飞行棋游戏表头
        /// </summary>
        static void ShowTittle()
        {
            Console.WriteLine("**********************************");
            Console.WriteLine("**********************************");
            Console.WriteLine("*            骑士飞行棋          *");
            Console.WriteLine("**********************************");
            Console.WriteLine("**********************************");
        }
        /// <summary>
        /// 地图初始化
        /// </summary>
        static void InitMap()
        {
            
            int[] luckTurn = {6,23,40,55,69,83,98};
            int[] landMine = {5,13,17,33,38,50,64,80,94 };
            int[] pause = { 9,27,60,93};
            int[] timeTurnnel = {20,25,45,63,72,88,90 };
            for (int i = 0; i < luckTurn.Length;i++ )
            {
                int p = luckTurn[i];
                map[p] = 1;
            }
            for (int i = 0; i < landMine.Length; i++)
            {
                int p = landMine[i];
                map[p] = 2;
            }
            for (int i = 0; i < pause.Length; i++)
            {
                int p = pause[i];
                map[p] = 3;
            }
            for (int i = 0; i < timeTurnnel.Length; i++)
            {
                int p = timeTurnnel[i];
                map[p] = 4;
            }
        }
        /// <summary>
        /// 初始化飞行棋游戏
        /// </summary>
        static void Init()
        {
            Console.WriteLine("请输入玩家A的姓名：");
            //string[] name = new string[2];
            name[0] = Console.ReadLine().Trim();
            while (name[0] == "")
            {
                Console.WriteLine("玩家A的姓名不能为空，请重新输入！");
                name[0] = Console.ReadLine().Trim();
            }
            Console.WriteLine("请输入玩家B的姓名：");
            name[1] = Console.ReadLine().Trim();
            while (name[1] == ""||name[0]==name[1])
            {
                if (name[1] == "")
                {
                    Console.WriteLine("玩家B的姓名不能为空，请重新输入！");
                    name[1] = Console.ReadLine().Trim();
                }
                if (name[0] == name[1])
                {
                    Console.WriteLine("玩家B的姓名已被玩家A占用，请重新输入！");
                    name[1] = Console.ReadLine().Trim();
                }
            }
        }
        /// <summary>
        /// 根据地图坐标绘制地图
        /// </summary>
        /// <param name="pos">地图坐标</param>
        /// <returns>返回坐标位置地图</returns>
        static string GetMap(int pos)
        {
            string result="";
            if (playerPos[0] == pos && playerPos[1] == pos)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                result = "<>";
            }
            else if (playerPos[0] == pos)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                result = "Ａ";
            }
            else if (playerPos[1] == pos)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                result = "Ｂ";
            }
            else
            {
                switch(map[pos])
                {
                    case 0:
                        Console.Write("□");
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("◎");
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.Write("☆");
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("▲");
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("卐");
                        break;
                }
            }
            Console.ResetColor();
            return result;
        }
        static void DrawMap()
        {
            Console.WriteLine("图列：幸运轮盘-◎    地雷-☆   休息-▲    时空隧道-卐");
            //画第一行
            for (int i = 0; i < 30; i++)
            {
                Console.Write(GetMap(i));
            }
            Console.WriteLine();
            //画第一列
            for (int i = 30; i < 35; i++)
            {
                for (int j = 0; j < 29; j++)
                {
                    Console.Write("  ");   
                }
                Console.WriteLine(GetMap(i));
            }
            //画第二行
            for (int i = 64; i > 34; i--)
            {
                Console.Write(GetMap(i));
            }
            Console.WriteLine();
            //画第二列
            for (int i = 65; i < 70; i++)
            {
                Console.WriteLine(GetMap(i));
            }
            //画第三列
            for (int i = 70; i < 100; i++)
            {
                Console.Write(GetMap(i));
            }
        }
        /// <summary>
        /// 执行时空隧道
        /// </summary>
        /// <param name="name"></param>
        static void ExcuteTimeTurnnel(string target)
        {
            //进入时空隧道，前进10格
            if (target == name[0])
            {
                playerPos[0] = playerPos[0] + 10;
            }
            if (target == name[1])
            {
                playerPos[1] = playerPos[1] + 10;
            }           
        }
        /// <summary>
        /// 执行暂停
        /// </summary>
        /// <param name="target"></param>
        static void ExcutePause(int target)
        {
            //暂停一轮次
            isStop[target] = true;
        }
        /// <summary>
        /// 执行地雷
        /// </summary>
        /// <param name="target"></param>
        static void EcxuteLandMine(string target)
        {
            //踩到地雷，退后5格
            if (target == name[0])
            {
                playerPos[0] = playerPos[0] -5;
            }
            if (target == name[1])
            {
                playerPos[1] = playerPos[1] -5;
            }
        }
        /// <summary>
        /// 执行幸运轮盘
        /// </summary>
        /// <param name="target"></param>
        static void ExcuteLuckTurn(string target)
        {
            Console.Clear();
            DrawMap();
            Console.WriteLine();
            Console.WriteLine("您走到了幸运轮盘位置，请选择运气：");
            Console.WriteLine("1—交换位置   2—轰炸对方");
            while (f == false)  //F为输入数值标识符，我实在想不出来更简单的办法了
            {
                    string userInput =Convert.ToString( Console.ReadLine());
                    if (userInput == "1")
                    {
                        f = true;
                    //交换玩家位置
                        int temp = playerPos[0];
                        playerPos[0] = playerPos[1];
                        playerPos[1] = temp;
                        return;
                    }
                    if (userInput == "2")    
                    {
                        f = true;
                    //轰炸对方
                        if (target == name[0])
                        {
                            playerPos[1] = playerPos[1] - 8;
                        }
                        if (target == name[1])
                        {
                            playerPos[0] = playerPos[0] - 8;
                        }
                        CheckPos();
                        return;                
                    }
                    else
                    {
                        Console.WriteLine("您输入的数值不正确，请重新输入");
                        f=false;
                    }
            }  
        }
        /// <summary>
        /// 玩家位置判断
        /// </summary>
        static void CheckPos()
        {
            for (int i = 0; i < 2; i++)
            {
                if (playerPos[i] > 99)
                {
                    playerPos[i] = 99;
                }
                if (playerPos[i] < 0)
                {
                    playerPos[i] = 0;
                }
            }
        }

    }
}