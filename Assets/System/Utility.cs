using UnityEngine;
using System.Collections.Generic;
using System;


namespace OGT_Utility
{
    //拡張メソッド
    static public class Extentions
    {
        #region 2次元配列用のループ
        //WithIndex用の構造体
        public struct WithIndexStruct<T>
        {
            //配列の中身
            public T Element { get; }
            //縦
            public int x { get; }
            //横
            public int y { get; }

            //同アセンブリ内専用のコンストラクタ(このスクリプトのみアクセス可能)
            internal WithIndexStruct(T element, int x, int y)
            {
                this.Element = element;
                this.x = x;
                this.y = y;
            }
        }

        //2次元配列用拡張メソッド(ループ用)
        public static IEnumerable<WithIndexStruct<T>> WithIndex<T>(this T[,] self)
        {
            if (self == null)
                throw new ArgumentNullException(nameof(self));

            //Xの要素の数だけループ
            for (int x = 0; x < self.GetLength(0); x++)
            {
                //Yの要素の数だけループ
                for (int y = 0; y < self.GetLength(1); y++)
                {
                    yield return new WithIndexStruct<T>(self[x, y], x, y);
                }
            }
        }
        #endregion
    }
}