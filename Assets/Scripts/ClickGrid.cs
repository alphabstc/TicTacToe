using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickGrid : MonoBehaviour{
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }
    void OnMouseUpAsButton(){
        Debug.Log("Click");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//通过射线来检测是否碰撞
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)){//和棋盘格子碰撞
            Debug.Log("Click Success");
            GameObject grid = hit.collider.gameObject;//获得碰撞的对象
            GameObject.Find("Chessboard").SendMessage("NewChess", grid);//调用棋盘对象的NewChess函数
        }
    }
}
