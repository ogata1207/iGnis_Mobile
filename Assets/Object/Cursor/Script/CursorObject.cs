using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorObject : MonoBehaviour {

    private Vector2 moveDirection;
    private float moveDelay = 0.1f;
    private float startTime;
    private bool isDelay = false;
    private CameraController cameraController;

	// Use this for initialization
	void Start () {
        cameraController = FindObjectOfType<CameraController>();
	}
	
	// Update is called once per frame
	void Update () {

        //次の移動までの間隔
        if (Time.time - startTime > moveDelay)
        {
            isDelay = false;
        }
        
        //MoveDelay秒たっていなければ次の移動を受け付けない
        if (isDelay) return;
        
        //入力があった場合
        if (moveDirection != Vector2.zero)
        {
            //移動方向にレイを飛ばす
            var ray = new Ray2D(transform.position, moveDirection);
            

            //レイの長さ
            var dist = moveDirection.magnitude;

            //レイの結果(衝突判定)
            var hit = Physics2D.Raycast(ray.origin, ray.direction, dist);
           

            //移動先が壁だった場合、カーソルを動かさずに終了する
            if (hit.collider != null && hit.transform.tag == "Wall")
            {
                //瞬間移動対策
                isDelay = true;
                startTime = Time.time;

                moveDirection = Vector2.zero;
                return;
            }

            //壁に当たらない場合
            //カーソルの移動
            var pos = transform.position;
            transform.position = pos + (Vector3)moveDirection;

            //カーソルの移動に合わせてカメラを移動
            cameraController.Move(moveDirection);

            //瞬間移動対策
            isDelay = true;
            startTime = Time.time;
        }
        
	}
 
    //----------------------------------------------------------------
    // UIのボタン用　関数
    //----------------------------------------------------------------
    public void MoveUp()
    {
        moveDirection = Vector2.up;
    }
    public void MoveDown()
    {
        moveDirection = Vector2.down;
    }
    public void MoveLeft()
    {
        moveDirection = Vector2.left;
    }
    public void MoveRight()
    {
        moveDirection = Vector2.right;
    }
    public void MoveStop()
    {
        moveDirection = Vector2.zero;
    }
}
