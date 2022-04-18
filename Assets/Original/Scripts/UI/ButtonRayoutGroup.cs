using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonRPG3D
{
    /// <summary>
    /// ボタンのレイアウトグループ
    /// </summary>
    public class ButtonRayoutGroup : MonoBehaviour
    {
        /// <summary>
        /// ボタンの並び方
        /// </summary>
        public enum RayoutType
        {
            Vertical,
            Horizontal
        }

        /// <summary>
        /// ボタンの並び方
        /// </summary>
        [field: SerializeField]
        public RayoutType Type { get; private set; } = RayoutType.Vertical;

        private void Start()
        {
            Init();
        }

        /// <summary>
        /// 初期化する
        /// </summary>
        public void Init()
        {
            Selectable[] selectables = SetNavigationToLoop();
            SelectFirst(selectables);
        }

        /// <summary>
        /// ループするようナビゲーションを設定する
        /// </summary>
        /// <returns>Selectableの配列</returns>
        public Selectable[] SetNavigationToLoop()
        {
            Selectable[] selectables = GetComponentsInChildren<Selectable>();
            int count = selectables.Length;
            for (var i = 0; i < count; i++)
            {
                Navigation newNavigation = selectables[i].navigation;
                newNavigation.mode = Navigation.Mode.Explicit;

                int next = i == 0 ? count - 1 : i - 1;
                int prev = (i + 1) % count;
                switch (Type)
                {
                    case RayoutType.Vertical:
                        newNavigation.selectOnUp = selectables[next];
                        newNavigation.selectOnDown = selectables[prev];
                        break;

                    case RayoutType.Horizontal:
                        newNavigation.selectOnLeft = selectables[next];
                        newNavigation.selectOnRight = selectables[prev];
                        break;
                }

                selectables[i].navigation = newNavigation;
            }

            return selectables;
        }

        /// <summary>
        /// 配列0番目を選択状態にする
        /// </summary>
        public void SelectFirst(Selectable[] selectables)
        {
            if (selectables == null || selectables.Length == 0)
            {
                return;
            }

            selectables[0].Select();
        }
    }
}
