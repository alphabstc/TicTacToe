using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessboard : MonoBehaviour{//棋盘类
    private GameObject[,] Chesses = new GameObject[3, 3];//存放棋盘格子对象的数组
    private int[,] value = new int[3,3];//存放棋盘每个格子状态的数组
    private int counter = 0;//计数器 当前棋盘上有多少个棋子
    private GameObject[] chess = new GameObject[9];//存放棋子对象的数组
    private bool gameover = true;//是否游戏结束
    // Start is called before the first frame update
    void Start(){
        Debug.Log("Start");
        //创建所有棋盘格子对象
        for(int i = 0;i < 3;i++){
            for(int j = 0;j <3;j++){
                if (i%2 != j%2){//根据坐标创造黑白相间的棋盘格子
                    Chesses[i, j] = Instantiate(Resources.Load("White Cube"), new Vector3(i, 0, j), Quaternion.identity) as GameObject;
                }
                else
                {
                    Chesses[i, j] = Instantiate(Resources.Load("Black Cube"), new Vector3(i, 0, j), Quaternion.identity) as GameObject;
                }
                Chesses[i, j].transform.parent = this.transform;
            }
        }
        Restart();
    }

    void Restart(){//重新开始游戏
        for(int i = 0; i < counter; i++)
            Destroy(chess[i]);//销毁棋子
        counter = 0;//计数器清零
        for(int i = 0; i < 3;i++){
            for(int j = 0; j <3;j++){
                //初始化棋盘状态
                value[i, j] = 100 + i * 10 + j;//为超过100且互不相等的数 则都不会相等 不会触发胜利条件
            }
        }    
        gameover = false;
    }

    // Update is called once per frame
    void Update(){
        
    }

    bool judgeWin(){
        //同一行 同一列 或者对角线上的状态相等 说明有玩家胜利
        for(int i = 0; i < 3; ++i)
            if (value[i, 1] == value[i, 0] && value[i, 2] == value[i, 0])
                return true;
        for(int i = 0; i < 3; ++i)
            if (value[1, i] == value[0, i] && value[2, i] == value[0, i])
                return true;
        if (value[1, 1] == value[0, 0] && value[2, 2] == value[0, 0])
            return true;
        if (value[1, 1] == value[0, 2] && value[2, 0] == value[0, 2])
            return true;                
        return false;
    }

    void OnGUI(){
        //生成一个菜单栏
        GUI.Box (new Rect (10, 10, 100, 50), "菜单");
        if (GUI.Button (new Rect (20, 35, 80, 20), "新游戏")) {
            Restart();//按下新游戏键则重新开始游戏
        }
        //判断是否有玩家胜利
        bool result = judgeWin();
        if (result){//有玩家胜利
            //胜利的是最后一个落子的玩家
            GUI.Box(new Rect (Screen.width / 2 - 100, Screen.height / 2 + 50, 200, 50), counter % 2 == 0 ? "蓝方胜" : "红方胜");
            gameover = true;
        }
        else{
            if (counter == 9){//没有胜利 但已经下完了9个棋子 则平局
                GUI.Box (new Rect (Screen.width / 2 - 100, Screen.height / 2 + 50, 200, 50), "平 局");
                gameover = true;
            }
        }
    }
    public void NewChess(GameObject grid){
        if (gameover)//游戏已经结束
            return;
        //已经放过了棋子
        if (value[(int)(grid.transform.position.x + 1e-5), (int)(grid.transform.position.z + 1e-5)] < 2)
            return;
        if (counter % 2 == 0){
            //是红方落子
            Debug.Log("Click Red");
            chess[counter++] = Instantiate(Resources.Load("Red Chess"), grid.transform.position + new Vector3(0, 1, 0), Quaternion.identity) as GameObject;
            value[(int)(grid.transform.position.x + 1e-5), (int)(grid.transform.position.z + 1e-5)] = 0;
        }
        else{
            //是蓝方落子
            Debug.Log("Click Blue");
            chess[counter++] = Instantiate(Resources.Load("Blue Chess"), grid.transform.position + new Vector3(0, 1, 0), Quaternion.identity) as GameObject;
            value[(int)(grid.transform.position.x + 1e-5), (int)(grid.transform.position.z + 1e-5)] = 1;
        }
    }
}

