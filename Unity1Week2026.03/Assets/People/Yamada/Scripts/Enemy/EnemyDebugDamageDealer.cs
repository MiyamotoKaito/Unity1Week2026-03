using Unity1Week.URA.Stage;
using UnityEngine;

namespace Unity1Week.URA.Enemy
{
    public class EnemyDebugDamageDealer : MonoBehaviour
    {
        public void Initialize(StageProgressController stageProgressController)
        {
            _stageProgressController = stageProgressController;
            Debug.Log("EnemyDebugDamageDealer initialized.");
        }

        public void NomalDamageClick()
        {
            Debug.Log("通常攻撃");
            _stageProgressController.DamageCurrentEnemy(_normalDamage);
        }

        public void BigDamageClick()
        {
            Debug.Log("大ダメージ");
            _stageProgressController.DamageCurrentEnemy(_bigDamage);
        }

        public void ReduceSkillTurnClick()
        {
            Debug.Log("スキルターン減少");
            _stageProgressController.ReduceEnemySkillTurn();
        }

        [SerializeField] private int _normalDamage;
        [SerializeField] private int _bigDamage;

        private StageProgressController _stageProgressController;
    }
}
