using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG3D
{
    /// <summary>
    /// 入力の管理者
    /// </summary>
    public class InputManager : ManagerBase
    {
        /// <summary>
        /// 軸の入力
        /// </summary>
        public enum AxisState
        {
            Neutral = 0,
            Input = 1
        }

        /// <summary>
        /// 軸の入力状態
        /// </summary>
        public AxisState CurrentAxisState { get; private set; } = AxisState.Neutral;

        /// <summary>
        /// 水平
        /// </summary>
        public static readonly string Horizontal = "Horizontal";

        /// <summary>
        /// 垂直
        /// </summary>
        public static readonly string Vertical = "Vertical";

        /// <summary>
        /// 決定
        /// </summary>
        public static readonly string Submit = "Submit";

        /// <summary>
        /// キャンセル
        /// </summary>
        public static readonly string Cancel = "Cancel";

        /// <summary>
        /// 直前のフレームの軸入力
        /// </summary>
        public Vector2 PreviousFlameAxisInput { get; private set; } = Vector2.zero;

        protected override void LateUpdateInnner()
        {
            base.LateUpdateInnner();

            PreviousFlameAxisInput = Get4Directions();
        }

        /// <summary>
        /// 決定ボタンが押されたか
        /// </summary>
        /// <returns></returns>
        public bool GetSubmitButtonDown()
        {
            return Input.GetButtonDown(Submit);
        }

        /// <summary>
        /// キャンセルボタンが押されたか
        /// </summary>
        /// <returns></returns>
        public bool GetCancelButtonDown()
        {
            return Input.GetButtonDown(Cancel);
        }

        /// <summary>
        /// 4方向の入力がされているか
        /// </summary>
        /// <returns></returns>
        public bool AxisIsInput()
        {
            return Get4Directions().sqrMagnitude > 0.0f;
        }

        /// <summary>
        /// 4方向の入力を取得する
        /// </summary>
        /// <returns>それぞれの値は0or1</returns>
        public Vector2 Get4Directions()
        {
            Vector3 axisRaw = Vector3.zero;
            axisRaw.x = Input.GetAxisRaw(Horizontal);
            axisRaw.y = Input.GetAxisRaw(Vertical);

            // 水平、垂直の両方に入力がある場合
            if (axisRaw.x != 0.0f && axisRaw.y != 0.0f)
            {
                // 入力が弱い方を打ち消す
                float axisX = Input.GetAxis(Horizontal);
                float axisY = Input.GetAxis(Vertical);
                if (Mathf.Abs(axisX) <= Mathf.Abs(axisY))
                {
                    axisRaw.x = 0.0f;
                }
                else
                {
                    axisRaw.y = 0.0f;
                }
            }

            return axisRaw;
        }

        /// <summary>
        /// 4方向の入力の押下を取得する
        /// </summary>
        /// <returns>方向</returns>
        public Vector2 Get4DirectionsDown()
        {
            if (PreviousFlameAxisInput != Vector2.zero)
            {
                return Vector2.zero;
            }

            return Get4Directions();
        }

        /// <summary>
        /// 垂直方向の入力を取得する
        /// </summary>
        /// <returns>垂直方向</returns>
        public float GetVirticalRaw()
        {
            return Input.GetAxisRaw(Vertical);
        }

        /// <summary>
        /// 水平方向の入力を取得する
        /// </summary>
        /// <returns>水平方向</returns>
        public float GetHorizontallRaw()
        {
            return Input.GetAxisRaw(Horizontal);
        }
    }
}
