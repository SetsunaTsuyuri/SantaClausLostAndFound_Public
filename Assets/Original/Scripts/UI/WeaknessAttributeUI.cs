using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonRPG3D
{
    /// <summary>
    /// 弱点属性の表示UI
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class WeaknessAttributeUI : MonoBehaviour, ICacheableRequiredComponents
    {
        /// <summary>
        /// 表示する属性
        /// </summary>
        [SerializeField]
        ActionData.AttributeType attribute = ActionData.AttributeType.None;

        [SerializeField]
        Text selfText = null;

        private void Reset()
        {
            CacheRequiredComponents();
        }

        public void CacheRequiredComponents()
        {
            selfText = GetComponent<Text>();
        }

        /// <summary>
        /// 初期化する
        /// </summary>
        public void Init()
        {
            //表示する属性が敵の弱点属性に含まれているならアクティブにする

            selfText.enabled = false;

            BattleManager battleManager = ManagersMaster.Instance.BattleM;
            CreatureData data = battleManager.Enemy.GetData();
            ActionData.AttributeType[] weaknessAttributes = data.WeaknessAttributes;
            foreach (var weakness in weaknessAttributes)
            {
                if (attribute == weakness)
                {
                    selfText.enabled = true;
                    break;
                }
            }
        }
    }
}
