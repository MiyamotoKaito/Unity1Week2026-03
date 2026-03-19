using UnityEngine;

namespace Unity1Week.URA.Enemy
{
    /// <summary>
    ///     敵のスキルのターン管理クラス。
    /// </summary>
    public class EnemySkillTurnTracker
    {
        public EnemySkillTurnTracker(int startTurns)
        {
            _startTurns = startTurns;
            _remainingTurns = startTurns;
        }

        public int StartTurns => _startTurns;
        public int RemainingTurns => _remainingTurns;
        public bool IsSkillReady => _remainingTurns <= 0;

        /// <summary>
        ///     ターンを減らす処理。
        /// </summary>
        /// <param name="value"> 減らすターン </param>
        public void ReduceTurn(int value = 1)
        {
            _remainingTurns = Mathf.Max(_remainingTurns - value, 0);
        }

        /// <summary>
        ///     ターンを増やす処理。
        /// </summary>
        /// <param name="value"> 増やすターン </param>
        public void AddTurn(int value = 1)
        {
            _remainingTurns += value;
        }

        /// <summary>
        ///     ターンをリセットする処理。
        ///     スキル使用後などに呼び出される。
        /// </summary>
        public void ResetTurn()
        {
            _remainingTurns = _startTurns;
        }

        private int _startTurns;
        private int _remainingTurns;
    }
}
