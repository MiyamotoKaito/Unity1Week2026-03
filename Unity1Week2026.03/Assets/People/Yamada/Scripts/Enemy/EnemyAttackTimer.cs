using UnityEngine;

namespace Unity1Week.URA.Enemy
{
    /// <summary>
    ///     敵の攻撃タイマー管理クラス。
    /// </summary>
    public class EnemyAttackTimer
    {
        public EnemyAttackTimer(float attackInterval)
        {
            _attackInterval = attackInterval;
            _remaingTime = attackInterval;
        }

        public float AttackInterval => _attackInterval;
        public float RemainingTime => _remaingTime;
        public bool IsAttackReady => _remaingTime <= 0;

        /// <summary>
        ///     攻撃までの残り時間を進める。
        /// </summary>
        /// <param name="deltaTime"></param>
        public void UpdateTimer(float deltaTime)
        {
            _remaingTime = Mathf.Max(_remaingTime - deltaTime, 0);
        }

        /// <summary>
        ///     タイマーをリセットする。
        /// </summary>
        public void ResetTimer()
        {
            _remaingTime = _attackInterval;
        }

        private float _attackInterval;
        private float _remaingTime;
    }
}
