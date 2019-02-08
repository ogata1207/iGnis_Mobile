using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//前後左右のタイルのポジション
public struct NearbyTilePosition
{
    public Vector2 up;
    public Vector2 down;
    public Vector2 right;
    public Vector2 left;
    public void SetPosition(Vector2 position)
    {
        up = position + Vector2.up;
        down = position + Vector2.down;
        right = position + Vector2.right;
        left = position + Vector2.left;
    }
}

public class TileObject : MonoBehaviour
{

    static public TileManager tileManager;
    static public FireManager fireManager;
    public StateManager state;
    public bool isActive;       //燃え移っている途中 true
    public bool isCompleted;    //完全に燃えたら     true
    public int tileId;
    private NearbyTilePosition nextPosition;

    //燃えるまでの時間
    public float burnTime;

    void Start()
    {
        if (tileManager == null) tileManager = FindObjectOfType<TileManager>();
        if (fireManager == null) fireManager = FindObjectOfType<FireManager>();

        //上下左右のタイルのポジションを作成
        nextPosition.SetPosition(transform.position);

        //ステイトの設定
        state = new StateManager();
        state.RequestState(Idle);
    }


    //*************************************************************************************************************************************************************************
    //
    //  State
    //
    //*************************************************************************************************************************************************************************

    //燃え移す待ち
    public void Idle(bool stateInitialize)
    {
        if (isActive) state.RequestState(Burn);
    }

    //燃え移すステイト
    public void Burn(bool stateInitialize)
    {
        if (stateInitialize) tileId = 98;
        if (state.elapsedTime > burnTime)
        {

            //更新をしなくする
            isActive = false;
            isCompleted = true;

            //タイルIDの変更
            tileId = 99;

            //周りに燃え移す

            //右側
            if (nextPosition.right.x <= tileManager.STAGE_MAX_SIZE)
            {
                //各種タイルの効果によって燃え移し方を変える
                ExecuteByType(nextPosition.right);
            }

            //左側
            if (nextPosition.left.x >= 0)
            {
                ExecuteByType(nextPosition.left);
            }

            //下側
            if (nextPosition.down.y >= 0)
            {
                ExecuteByType(nextPosition.down);
            }

            //上側
            if (nextPosition.up.x <= tileManager.STAGE_MAX_SIZE)
            {
                ExecuteByType(nextPosition.up);
            }



            //炎のエフェクトを生成
            fireManager.GetObject(transform.position, transform.rotation);




        }
    }
    //*************************************************************************************************************************************************************************
    //
    //  Method
    //
    //*************************************************************************************************************************************************************************
    //
    public void SetTimeAndExecute(float time)
    {
        if (isActive == true && tileId == 99) return;
        burnTime = time;
        isActive = true;
    }

    //種類別に実行(周囲に火を燃え移す)
    void ExecuteByType(Vector2 position)
    {
        if (ExecutionJudgment(position))
        {
            var x = (int)position.x;
            var y = (int)position.y;

            switch (tileManager.tileObject[x, y].tileId)
            {
                case 1:     //草タイル
                    tileManager.tileObject[x, y].SetTimeAndExecute(1.0f);
                    break;
            }
        }
    }

    //実行するかどうかの判定 (完了済みならfalse)
    bool ExecutionJudgment(Vector2 position)
    {
        var x = (int)position.x;
        var y = (int)position.y;
        return !tileManager.tileObject[x, y].isCompleted;
    }
}


