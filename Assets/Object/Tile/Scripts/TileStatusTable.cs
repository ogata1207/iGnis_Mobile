using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public enum HowToBurn
{
    NONE      = 0,  //無し(無効)
    GrassTile = 1,  //通常のタイル(ゆっくり燃える)
    OilTile   = 2,  //設置型の燃えるタイル
    RiverTile = 3,  //川(オイルを設置可能)
    
}
[System.Serializable]
public class TileRegistry
{
    public HowToBurn howToBurn;
    public int id;
    public Sprite sprite;
}

[CreateAssetMenu(fileName = "TileStatusTable", menuName = "OGT/Status Table/TileStatus")]
public class TileStatusTable : ScriptableObject
{
    [SerializeField, Header("Resourcesフォルダに置く")]
    public static string FILE_PATH = "StatusTable/TileStatusTable";


    private static TileStatusTable instance;

    #region Instance
    public static TileStatusTable GetInstance
    {
        get
        {
            if(instance == null)
            {
                var table = (TileStatusTable)Resources.Load(FILE_PATH);
                if (table == null) Debug.LogError("指定のパスにTileStatusTableが存在ません");
                else instance = table;
            }
            return instance;
        }
    }
    #endregion

    #region Tile関連
    //燃えている最中のブロック
    public int burningTile = 98;
    
    //燃えた後のブロック
    public int burnnedTile = 99;
    
    //各種タイルを登録する
    [SerializeField]
    public TileRegistry[] registeredTile;

    //エディタ上でEnumを更新したときに自動的にEnumに沿ってIDを振り分ける
    public void UpdateTileID()
    {
        foreach (var tile in registeredTile)
        {
            tile.id = (int)tile.howToBurn;
        }
        
    }
    #endregion

    public float[] burnTime;

}
