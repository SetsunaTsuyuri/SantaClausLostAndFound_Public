using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// フィールドのデータ
    /// </summary>
    [Serializable]
    public class FieldData : ScriptableObject
    {
        /// <summary>
        /// 正規表現
        /// </summary>
        static readonly Regex CRLF = new Regex("\r\n");

        /// <summary>
        /// 下層レイヤーをCSV配列何番目から取得するか
        /// </summary>
        static readonly int UnderLayerIndex = 0;

        /// <summary>
        /// 中層レイヤーCSV配列何番目から取得する
        /// </summary>
        static readonly int MiddleLayerIndex = 1;

        /// <summary>
        /// 上層レイヤーをCSV配列何番目から取得するか
        /// </summary>
        static readonly int UpperLayerIndex = 2;

        /// <summary>
        /// 幅
        /// </summary>
        [field: SerializeField]
        public int Width { get; private set; } = 0;

        /// <summary>
        /// 高さ
        /// </summary>
        [field: SerializeField]
        public int Height { get; private set; } = 0;

        /// <summary>
        /// セルの配列
        /// </summary>
        [field: SerializeField]
        public Cell[] Cells { get; private set; } = null;

        /// <summary>
        /// プレイヤーの初期位置
        /// </summary>
        [field: SerializeField]
        public Vector2Int InitialPositonOfPlayer { get; private set; } = Vector2Int.zero;

        /// <summary>
        /// プレイヤーの初期方向
        /// </summary>
        [field: SerializeField]
        public Creature.Direction InitialDirectionOfPlayer { get; private set; } = Creature.Direction.North;

        /// <summary>
        /// 床ブロックのプレファブ
        /// </summary>
        [field: SerializeField]
        public GameObject FloorBlockA { get; private set; } = null;

        /// <summary>
        /// 床ブロックBのプレファブ
        /// </summary>
        [field: SerializeField]
        public GameObject FloorBlockB { get; private set; } = null;

        /// <summary>
        /// 水ブロックのプレファブ
        /// </summary>
        [field: SerializeField]
        public GameObject WaterBlock { get; private set; } = null;

        /// <summary>
        /// 壁ブロックAのプレファブ
        /// </summary>
        [field: SerializeField]
        public GameObject WallBlockA { get; private set; } = null;

        /// <summary>
        /// 壁ブロックBのプレファブ
        /// </summary>
        [field: SerializeField]
        public GameObject WallBlockB { get; private set; } = null;

        /// <summary>
        /// 橋のプレファブ
        /// </summary>
        [field: SerializeField]
        public GameObject Bridge { get; private set; } = null;

        /// <summary>
        /// 隠し通路のプレファブ
        /// </summary>
        [field: SerializeField]
        public GameObject SecretPassage { get; private set; } = null;

        /// <summary>
        /// 草Aのプレファブ
        /// </summary>
        [field: SerializeField]
        public GameObject GrassA { get; private set; } = null;

        [field: SerializeField]
        public GameObject GrassB { get; private set; } = null;

        /// <summary>
        /// 木のプレファブ
        /// </summary>
        [field: SerializeField]
        public GameObject Tree { get; private set; } = null;

        /// <summary>
        /// 初期化する
        /// </summary>
        /// <param name="width">幅</param>
        /// <param name="height">高さ</param>
        public void Init(int width, int height)
        {
            Width = width;
            Height = height;
            Cells = new Cell[Width * Height];
        }

        /// <summary>
        /// 初期化する
        /// </summary>
        /// <param name="width">幅</param>
        /// <param name="height">高さ</param>
        /// <param name="csvArray">それぞれのセルのタイプを定めるCSVテキストの配列</param>
        public void Init(int width, int height, string[] csvArray)
        {
            Init(width, height);

            // 下層レイヤー
            Cell.UnderType[] underLayerTypes = new Cell.UnderType[Cells.Length];

            int[] underLayerIntValues = ToArrayInt(csvArray[UnderLayerIndex]);
            for (int i = 0; i < Cells.Length; i++)
            {
                underLayerTypes[i] = (Cell.UnderType)Enum.ToObject(typeof(Cell.UnderType), underLayerIntValues[i]);
            }

            // 中層レイヤー
            Cell.MiddleType[] middleLayerTypes = new Cell.MiddleType[Cells.Length];

            int[] middleLayerIntValues = ToArrayInt(csvArray[MiddleLayerIndex]);
            for (int i = 0; i < Cells.Length; i++)
            {
                middleLayerTypes[i] = (Cell.MiddleType)Enum.ToObject(typeof(Cell.MiddleType), middleLayerIntValues[i]);
            }

            // 上層レイヤー
            Cell.UpperType[] upperLayerTypes = new Cell.UpperType[Cells.Length];

            int[] upperLayerIntValues = ToArrayInt(csvArray[UpperLayerIndex]);
            for (int i = 0; i < Cells.Length; i++)
            {
                upperLayerTypes[i] = (Cell.UpperType)Enum.ToObject(typeof(Cell.UpperType), upperLayerIntValues[i]);
            }

            // セルを配置する
            for (int i = 0; i < Cells.Length; i++)
            {
                Cell.UnderType under = underLayerTypes[i];
                Cell.MiddleType middle = middleLayerTypes[i];
                Cell.UpperType upper = upperLayerTypes[i];
                Cells[i] = new Cell(under, middle ,upper);
            }
        }

        /// <summary>
        /// CSVをint型の配列に変換する
        /// </summary>
        /// <param name="csv">CSV</param>
        /// <returns>int型配列</returns>
        private int[] ToArrayInt(string csv)
        {
            int[] result = new int[Cells.Length];

            csv = CRLF.Replace(csv, string.Empty);

            // 先頭、末尾の改行を消す
            csv = csv.Trim('\n');

            // テキストを行で区切る
            string[] rows = csv.Split('\n');

            // 中身を反転させる
            Array.Reverse(rows);

            // Y座標
            for (int y = 0; y < rows.Length; y++)
            {
                string row = rows[y];

                // 末尾のカンマを消す
                row = row.TrimEnd(',');

                // 行をカンマで区切る
                string[] columns = row.Split(',');

                // X座標
                for (int x = 0; x < columns.Length; x++)
                {
                    string column = columns[x];

                    // 値を数値に変換する
                    int value = int.Parse(column);

                    // 数値を配列に代入する 
                    int index = ToIndex(x, y);
                    result[index] = value;
                }
            }

            return result;
        }

        /// <summary>
        /// 指定された座標のセルを取得する
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        /// <returns>セル</returns>
        public Cell GetCell(int x, int y)
        {
            if (IsOutOfRange(x, y))
            {
                return null;
            }

            int index = y * Width + x;
            return Cells[index];
        }

        /// <summary>
        /// 指定された座標のセルを取得する
        /// </summary>
        /// <param name="position">座標</param>
        /// <returns>セル</returns>
        public Cell GetCell(Vector2Int position)
        {
            return GetCell(position.x, position.y);
        }

        /// <summary>
        /// 指定された座標にセルを設定する
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        /// <param name="cell">セル</param>
        public void SetCell(int x, int y, Cell cell)
        {
            if (IsOutOfRange(x, y))
            {
                return;
            }

            int index = y * Width + x;
            Cells[index] = cell;
        }

        /// <summary>
        /// 指定された座標にセルを設定する
        /// </summary>
        /// <param name="position">X,Y座標</param>
        /// <param name="cell">セル</param>
        public void SetCell(Vector2Int position, Cell cell)
        {
            SetCell(position.x, position.y, cell);
        }

        /// <summary>
        /// 領域外の座標が指定されたか？
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        /// <returns></returns>
        public bool IsOutOfRange(int x, int y)
        {
            bool result = false;

            if (x < 0 || x >= Width)
            {
                result = true;
            }
            else if (y < 0 || y >= Height)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 領域外の座標が指定されたか？
        /// </summary>
        /// <param name="position">X,Y座標</param>
        /// <returns></returns>
        public bool IsOutOfRange(Vector2Int position)
        {
            return IsOutOfRange(position.x, position.y);
        }

        /// <summary>
        /// 座標を配列の添え字に変換する
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        /// <returns>配列の添え字</returns>
        public int ToIndex(int x, int y)
        {
            return y * Width + x;
        }

        /// <summary>
        /// 座標を配列の添え字に変換する
        /// </summary>
        /// <param name="position">座標</param>
        /// <returns>配列の添え字</returns>
        public int ToIndex(Vector2Int position)
        {
            return ToIndex(position.x, position.y);
        }

        /// <summary>
        /// 配列の添え字を座標に変換する
        /// </summary>
        /// <param name="index">配列の添え字</param>
        /// <returns>座標</returns>
        public Vector2Int ToPosition(int index)
        {
            int x = index % Width;
            int y = index / Width;
            return new Vector2Int(x, y);
        }
    }
}
