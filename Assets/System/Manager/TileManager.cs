﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
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
    public int[,] tileIdList;

    [Space(16)]

    [Header("スプライトの登録とIDの振り分け")]

    [SerializeField]
    public BlockRegistry[] registeredBlock;

    // Use this for initialization
    void Awake()
    { 
        tileIdList = new int[STAGE_MAX_SIZE, STAGE_MAX_SIZE];
        

        //登録されたブロックのIDをタイルにあわせてIDリストに入れる
        for(int y=0;y<STAGE_MAX_SIZE;y++)
        {
            for(int x=0;x<STAGE_MAX_SIZE;x++)
            {
                var sprite = tileMap.GetSprite(new Vector3Int(x, y, 0));

                foreach(var block in registeredBlock)
                {
                    //一致するタイルがあればそのタイルのIDを入れる
                    //なければ -1
                    if (block.sprite == sprite)
                    {
                        tileIdList[x, y] = block.id;
                        tileNum++;
                    }
                    else
                    {
                        tileIdList[x, y] = -1;
                    }
                    
                }

            }
        }
    }
      
	
}
