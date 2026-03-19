using System.Collections.Generic;
using Unity1Week.URA.Enemy;
using Unity1Week.URA.Player;

namespace Unity1Week.URA.Battle
{
    /// <summary>
    ///     敵の攻撃やスキルの発動を管理するクラス。
    /// </summary>
    public class EnemyBattleProvider
    {
        public EnemyBattleProvider(
            EnemyRuntimeModel enemyRuntimeModel,
            PlayerHealthModel playerHealthModel,
            EnemyAttackTimer enemyAttackTimer,
            EnemySkillTurnTracker enemySkillTurnTracker)
        {
            _enemyRuntimeModel = enemyRuntimeModel;
            _playerHealthModel = playerHealthModel;
            _enemyAttackTimer = enemyAttackTimer;
            _enemySkillTurnTracker = enemySkillTurnTracker;
        }

        /// <summary>
        ///     毎フレーム呼んで、敵の攻撃タイマーを更新する処理。
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Update(float deltaTime)
        {
            _enemyAttackTimer.UpdateTimer(deltaTime);

            if (_enemyAttackTimer.IsAttackReady)
            {
                _playerHealthModel.TakeDamage(_enemyRuntimeModel.CurrentAttackPower);
                _enemyAttackTimer.ResetTimer();
            }
        }

        /// <summary>
        ///     スキルターンを減らす呼び出し口。
        /// </summary>
        public void ReduceSkillTurn()
        {
            _enemySkillTurnTracker.ReduceTurn();
            TryExecuteEnemySkill();
        }

        /// <summary>
        ///     敵のスキルが発動可能か確認して、発動させる。
        /// </summary>
        private void TryExecuteEnemySkill()
        {
            // スキルが発動可能な状態でなければ、何もしない。
            if (!_enemySkillTurnTracker.IsSkillReady)
            {
                return;
            }

            // スキルの実行に必要な情報をまとめたコンテキストを作成する。
            EnemySkillContext skillContext = new EnemySkillContext(
                _enemyRuntimeModel,
                _playerHealthModel,
                _enemySkillTurnTracker);

            // 敵のスキルを順番に実行する。
            IReadOnlyList<EnemySkillBase> skills = _enemyRuntimeModel.Data.EnemySkills;

            // スキルが複数ある場合を想定して、ループで回す。
            for (int i = 0; i < skills.Count; i++)
            {
                EnemySkillBase skill = skills[i];
                skill.Execute(skillContext);
            }

            _enemySkillTurnTracker.ResetTurn();
        }

        private readonly EnemyRuntimeModel _enemyRuntimeModel;
        private readonly PlayerHealthModel _playerHealthModel;
        private readonly EnemyAttackTimer _enemyAttackTimer;
        private readonly EnemySkillTurnTracker _enemySkillTurnTracker;
    }
}