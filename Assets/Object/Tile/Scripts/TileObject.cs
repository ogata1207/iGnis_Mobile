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
    
    //上下左右のポジションをセット
    public void SetPosition(Vector2 position)
    {
        up = position + Vector2.up;
        down = position + Vector2.down;
        right = position + Vector2.right;
        left = position + Vector2.left;
    }
}

//*******************************************************************************************************
//
//  このオブジェクトはTileManagerで管理する
//
//*******************************************************************************************************

public class TileObject : MonoBehaviour
{

    static public TileManager tileManager;
    static public FireController fireController;
    static public TileStatusTable table;        //Tile関連のテーブル

    public StateManager state;                  //オブジェクトのステイト
    public bool isActive;                       //燃え移っている途中 true
    public bool isCompleted;                    //完全に燃えたら     true
    public int tileId;                          //オブジェクトのタイルID

    private int num = 0;
    private NearbyTilePosition nextPosition;    //上下左右のタイルのポジション

    //燃えるまでの時間
    public float burnTime;

    void Start()
    {
        //各種マネージャーの取得
        if (tileManager == null) tileManager = FindObjectOfType<TileManager>();
        if (fireController == null) fireController = FindObjectOfType<FireController>();
        if (table == null) table = TileStatusTable.GetInstance;
        
        //上下左右のタイルのポジションを作成
        nextPosition.SetPosition(transform.position);

        //ステイトの設定
        state = new StateManager();
        state.RequestState(Idle);
    }


    //*******************************************************************************************************
    //
    //  State
    //
    //*******************************************************************************************************

    /// <summary>
    /// 燃え移す待ち
    /// </summary>
    /// <param name="stateInitialize">ステイトに入った瞬間だけ TRUE</param>
    public void Idle(bool stateInitialize)
    {
        //燃え移るステイトに移行
        if (isActive) 
        {
            tileId = table.burningTile; //燃えている途中
            state.RequestState(Burn);
        }
    }

    /// <summary>
    /// 燃え移すステイト
    /// </summary>
    /// <param name="stateInitialize">ステイトに入った瞬間だけ TRUE</param>
    public void Burn(bool stateInitialize)
    {

            

        //時間がきたら燃え移る
        if (state.elapsedTime > burnTime)
        {
            
            //更新をしなくする
            //isActive = false;
            //炎のエフェクトを生成
            if(!isCompleted)fireController.GetObject(transform.position, transform.rotation);
            isCompleted = true;

            //タイルIDの変更
            tileId = table.burnnedTile;

            //周りに燃え移す

            //右側
            if (nextPosition.right.x < tileManager.STAGE_MAX_SIZE)//ステージの最大値より奥は燃えない
            {
                //各種タイルの効果によって燃え移し方を変える
                num += ExecuteByType(nextPosition.right);                  
            }

            //左側
            if (nextPosition.left.x >= 0)                           // 0 : 左端ギリギリ
            {
                num += ExecuteByType(nextPosition.left);
            }

            //下側
            if (nextPosition.down.y >= 0)                           // 0 : 上側ギリギリ
            {
                num += ExecuteByType(nextPosition.down);
            }

            //上側
            if (nextPosition.up.x < tileManager.STAGE_MAX_SIZE)    //ステージの最大値より奥は燃えない
            {
                num += ExecuteByType(nextPosition.up);
            }

            //前後左右のタイルが燃えたらUpdateを非有効にする
            if(num == 4)
            {
                isActive = false;
            }
        }
    }
    //*******************************************************************************************************
    //
    //  Method
    //
    //*******************************************************************************************************
    /// <summary>
    /// 燃え移るまでの時間をセットして、燃え移るステイトを実行
    /// </summary>
    /// <param name="time">燃え移るまでの時間</param>
    public void SetTimeAndExecute(float time)
    {
        //燃えている最中は無視
        if (isActive == true && tileId == table.burningTile) return;

        //既に燃えているなら無視
        if (isCompleted == true && tileId == table.burnnedTile) return;
        
        burnTime = time;
        isActive = true;
    }

    /// <summary>
    /// 種類別に実行(周囲に火を燃え移す)
    /// </summary>
    /// <param name="position">隣のタイルのポジション</param>
    int ExecuteByType(Vector2 position)
    {
        
        if (ExecutionJudgment(position))
        {
            var x = (int)position.x;
            var y = (int)position.y;
            var id = tileManager.tileObject[x, y].tileId;
            
            //現状は、オイルと普通の草ブロックだけ燃える仕様なので
            //その2種類以外は弾く
            if (id == (int)HowToBurn.NONE || id ==(int)HowToBurn.RiverTile) return 0;
            if (id == table.burningTile || id == table.burnnedTile) return 0;
            //タイルIDに合わせて燃え移るまでの時間をセットして
            //隣のタイルのステイトを実行する
            Debug.Log("kita[Tile ID] : " + id);
            tileManager.tileObject[x, y].SetTimeAndExecute(table.burnTime[id]);
            return 1;
        }

        return 0;
    }

    /// <summary>
    /// 隣のタイルが既に燃え移っていないか確認
    /// </summary>
    /// <param name="position">隣のタイルのポジション</param>
    /// <returns>燃え移っていなければ TRUE</returns>
    bool ExecutionJudgment(Vector2 position)
    {
        var x = (int)position.x;
        var y = (int)position.y;
        return !tileManager.tileObject[x, y].isCompleted;
    }
}


