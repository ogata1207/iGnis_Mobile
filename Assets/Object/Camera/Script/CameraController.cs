using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [Header("カーソルが追跡し始める座標(Viewport座標)")]
    public Vector2 chaseMin;
    public Vector2 chaseMax;
    private CursorObject cursor;
   
	// Use this for initialization
	void Start () {
        cursor = FindObjectOfType<CursorObject>();
	}

	// Update is called once per frame
	public void Move (Vector2 dir) {

        //カーソルのポジションをViewport座標に変換
        var cursorScreenPos = Camera.main.WorldToViewportPoint(cursor.transform.position+(Vector3)dir);

        //カーソルが範囲外にでたら追跡する
        var pos = transform.position;

        if (cursorScreenPos.x < chaseMin.x) pos += (Vector3)Vector2.left;      //　左側の判定
        if (cursorScreenPos.x > chaseMax.x) pos += (Vector3)Vector2.right;     //  右側の判定
        if (cursorScreenPos.y < chaseMin.y) pos += (Vector3)Vector2.down;      //  下側の判定
        if (cursorScreenPos.y > chaseMax.y) pos += (Vector3)Vector2.up;        //　上側の判定

        //判定結果の反映
        transform.position = pos;


    }
}
