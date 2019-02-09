using UnityEngine;
using UnityEditor;
using OGT_Utility;

public class TileStatusTableWindow : EditorWindow
{
    static Vector2 windowSize = new Vector2(450, 500);
    [MenuItem(itemName: "Setting/Statu Table/TileStatus")]
    static void Create()
    {
        var window = GetWindow<TileStatusTableWindow>("Tile Status");
        window.minSize = windowSize;
        window.maxSize = windowSize;
        
    }
    //*******************************************************************************

    //ScriptableObject
    public TileStatusTable table;
    
    //タイルの登録と管理
    public SerializedProperty registryTiles;
    public SerializedObject serializeObject;
    public int currentIndex;

    //燃える時間の設定
    public SerializedProperty burnTime;
    public HowToBurn howToBurn　= HowToBurn.GrassTile;

    private void OnGUI()
    {
        //初期化
        {
            //テーブルの初期化
            if (table == null)
            {
                table = TileStatusTable.GetInstance;
                serializeObject = new SerializedObject(table);

                //タイルの登録と管理
                registryTiles = serializeObject.FindProperty("registeredTile");
                currentIndex = 0;
               
                //燃える時間
                 burnTime = serializeObject.FindProperty("burnTime");
            }

            
        }




        //***********************************************************************************
        // タイルの登録
        //***********************************************************************************
        EditorGUILayout.BeginVertical(GUI.skin.box);
        {
            //ラベル
            EditorGUILayout.LabelField("タイルのID振り分け");

            EditorGUILayout.Space();

            //燃え始め
            EditorGUILayout.LabelField("燃え始めのID");
            table.burningTile = EditorGUILayout.IntField("ID :", table.burningTile);
            EditorGUILayout.Space();

            //燃えた後
            EditorGUILayout.LabelField("燃えた後のID");
            table.burnnedTile = EditorGUILayout.IntField("ID :", table.burnnedTile);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("各種タイルの登録と管理");
            {
                //構造体の最大数の設定
                registryTiles.arraySize = EditorGUILayout.IntField("MaxSize", registryTiles.arraySize);
                if (registryTiles.arraySize != 0)
                {
                    //スライドでインデックスを決定
                    currentIndex = EditorGUILayout.IntSlider(currentIndex, 0, registryTiles.arraySize - 1);

                    //スライドで指定した配列の中身を抽出
                    SerializedProperty selectTileStatus = registryTiles.GetArrayElementAtIndex(currentIndex);

                    //抽出した配列の中身を表示
                    EditorGUILayout.PropertyField(selectTileStatus,true);

                    //プロパティの更新
                    serializeObject.ApplyModifiedProperties();
                }
            }
        }
        EditorGUILayout.EndVertical();

        //***********************************************************************************
        // 燃える時間の設定
        //***********************************************************************************

        EditorGUILayout.BeginVertical(GUI.skin.box);
        {
            EditorGUILayout.LabelField("各種タイルの燃えるまでの時間を設定");
            EditorGUILayout.Space();

            //Enumの要素と同じ数の配列を作る
            burnTime.arraySize = sizeof(HowToBurn);

            //Enumで変更する要素の番号を決める
            howToBurn = (HowToBurn)EditorGUILayout.EnumPopup("設定するタイル", howToBurn);
            SerializedProperty selectBurnTime = burnTime.GetArrayElementAtIndex((int)howToBurn);

            //燃えるまでの時間を設定
            if(howToBurn != 0)
            EditorGUILayout.PropertyField(selectBurnTime, new GUIContent("燃えるまでの時間"));

            //プロパティの更新
            serializeObject.ApplyModifiedProperties();
        }
        EditorGUILayout.EndVertical();

        //***********************************************************************************
        // 終了
        //***********************************************************************************
        EditorGUILayout.BeginVertical(GUI.skin.box);
        {
            //TileStatuTableを保存する
            if (GUILayout.Button("保存"))
            {
                //Enumに合わせてIDを振り分け
                table.UpdateTileID();

                //セーブ
                EditorUtility.SetDirty(table);
                AssetDatabase.SaveAssets();

                //閉じる
                var window = GetWindow<TileStatusTableWindow>("Tile Status");
                window.Close();
            }
        }
        EditorGUILayout.EndVertical();
    }
}
