using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Linq;

[System.Serializable]
public class BlockRegistry
{
    public int id;
    public Sprite sprite;
}

public class TileManager : MonoBehaviour {

    static public readonly int STAGE_MAX_SIZE = 50;

    public int tileNum;
    //地形
    public Tilemap tileMap;
    
    //
    static public int[,] tileIdList;
    public GameObject tileObj;
    public TileObject[,] tileObject;

    [Space(16)]

    [Header("スプライトの登録とIDの振り分け")]

    [SerializeField]
    public BlockRegistry[] registeredBlock;

    private TitleState state;

#if UNITY_EDITOR
    private CursorObject cursor;
#endif


    // Use this for initialization
    void Awake()
    {
        state = FindObjectOfType<TitleState>();

#if UNITY_EDITOR
        cursor = FindObjectOfType<CursorObject>();
#endif


        tileIdList = new int[STAGE_MAX_SIZE, STAGE_MAX_SIZE];
        tileObject = new TileObject[STAGE_MAX_SIZE, STAGE_MAX_SIZE];
        
        //登録されたブロックのIDをタイルにあわせてIDリストに入れる
        foreach (var item in tileIdList.WithIndex())
        {
            
            var x = item.X;
            var y = item.Y;

            var sprite = tileMap.GetSprite(new Vector3Int(x, y, 0));

            foreach (var block in registeredBlock)
            {
                //タイルオブジェクトの生成
                var obj = Instantiate(tileObj, new Vector3(x, y), transform.rotation);
                tileObject[x, y] = obj.GetComponent<TileObject>();

                //一致するタイルがあればそのタイルのIDを入れる
                //なければ -1
                if (block.sprite == sprite)
                {
                    tileObject[x, y].tileId = block.id;
                    tileIdList[x, y] = block.id;
                    tileNum++;
                    break;
                }
                else
                {
                    //tileObject[x, y].tileId = -1;
                    tileIdList[x, y] = -1;
                }

                
            }

        }

    }

    void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.S))
        {
            var x = (int)cursor.transform.position.x;
            var y = (int)cursor.transform.position.y;
            tileObject[x, y].SetTime(1.0f);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            var x = (int)cursor.transform.position.x;
            var y = (int)cursor.transform.position.y;
            Debug.Log("TileID : " + tileObject[x,y].tileId);
        }

#endif
        
        //各タイルのステイトを更新
        foreach (var tile in tileObject.WithIndex().Where(index => index.Element.isActive == true))
        {
            tile.Element.state.Update();
        }
    }

    public void Burning()
    {
        var x = (int)cursor.transform.position.x;
        var y = (int)cursor.transform.position.y;
        tileObject[x, y].SetTime(1.0f);
    }
}



//テスト
static public class Extentions
{
    //次元配列用に入れ物を用意する
    public struct IndexedItem<T>
    {
        public T Element { get; }
        public int X { get; }
        public int Y { get; }
        internal IndexedItem(T element, int x, int y)
        {
            this.Element = element;
            this.X = x;
            this.Y = y;
        }
    }

    //2次元配列用拡張メソッド(ループ用)
    public static IEnumerable<IndexedItem<T>> WithIndex<T>(this T[,] self)
    {
        if (self == null)
            throw new ArgumentNullException(nameof(self));

        for (int x = 0; x < self.GetLength(0); x++)
        {  
            for (int y = 0; y < self.GetLength(1); y++)
            {
                yield return new IndexedItem<T>(self[x, y], x, y);
            }
        }
    }
}