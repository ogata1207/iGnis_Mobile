using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Linq;

using OGT_Utility;



public class TileManager : MonoBehaviour {

    //(テーブルに移動　検討中)
    public int STAGE_MAX_SIZE = 50;

    //ステータス テーブル
    private TileStatusTable table;

    //燃えるタイルの数
    public int tileNum;
    
    //地形
    public Tilemap tileMap;
    
    //タイルオブジェクト(ステイト) 
    public GameObject baseObj;          //タイルオブジェクト
    public TileObject[,] tileObject;    //管理用の配列

    private CursorObject cursor;
 
    // Use this for initialization
    void Awake()
    {
        //テーブルを取得
        table = TileStatusTable.GetInstance;

        //カーソルの取得
        cursor = FindObjectOfType<CursorObject>();


        //タイルオブジェクトを生成
        //中身は各タイルのState
        tileObject = new TileObject[STAGE_MAX_SIZE, STAGE_MAX_SIZE];
        
        //登録されたブロックのIDをタイルにあわせてIDリストに入れる
        foreach (var index in tileObject.WithIndex())
        {
            var x = index.x;
            var y = index.y;
            
            //スプライトの取得
            var sprite = tileMap.GetSprite(new Vector3Int(x, y, 0));

            //登録されているタイルと比較
            foreach (var block in table.registeredTile)
            {
                //タイルオブジェクトの生成
                var obj = Instantiate(baseObj, new Vector3(x, y), transform.rotation);
                
                //生成したオブジェクトのタイルステイトを取得
                tileObject[x, y] = obj.GetComponent<TileObject>();

                //一致するタイルがあればそのタイルのIDを入れる
                //なければ -1
                if (block.sprite == sprite)
                {
                    //IDの登録
                    tileObject[x, y].tileId = block.id;
                    
                    //カウント
                    tileNum++;
                    break;
                }
                else
                {
                    //登録されているタイルと一致しない場合
                    tileObject[x, y].tileId = -1;
                }
            }
        }
    }

    void Update()
    {
        #if UNITY_EDITOR
        DebugKey();
        #endif

        //各タイルのステイトを更新
        foreach (var tile in tileObject.WithIndex().Where(index => index.Element.isActive == true))
        {
            tile.Element.state.Update();
        }
    }

    //(仮) ボタンを押すとカーソルの位置にあるタイルが燃える
    public void Burning()
    {
        var x = (int)cursor.transform.position.x;
        var y = (int)cursor.transform.position.y;
        tileObject[x, y].SetTimeAndExecute(1.0f);
    }

    /// <summary>
    /// デバッグ用のキー
    /// </summary>
    public void DebugKey()
    {

        //確認用
        if (Input.GetKeyDown(KeyCode.S))
        {
            var x = (int)cursor.transform.position.x;
            var y = (int)cursor.transform.position.y;
            tileObject[x, y].SetTimeAndExecute(1.0f);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            var x = (int)cursor.transform.position.x;
            var y = (int)cursor.transform.position.y;
            Debug.Log("TileID : " + tileObject[x, y].tileId);
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            var x = (int)cursor.transform.position.x;
            var y = (int)cursor.transform.position.y;

            var pos = tileMap.LocalToCell(new Vector3(x, y, 0));

            //オイルIDに変換
            tileObject[pos.x, pos.y].tileId = (int)HowToBurn.OilTile;
            
        }

    }
}


