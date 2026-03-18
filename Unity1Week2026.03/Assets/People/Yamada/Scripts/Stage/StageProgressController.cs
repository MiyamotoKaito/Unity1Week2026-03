using Unity1Week.URA.Battle;
using Unity1Week.URA.Enemy;
using Unity1Week.URA.Player;
using UnityEngine;

namespace Unity1Week.URA.Stage
{
    /// <summary>
    ///     ステージの進行状況を管理し、ラウンドの開始、敵のダメージ処理、ゲームオーバーやステージクリアの状態を制御します。
    /// </summary>
    public class StageProgressController
    {
        public bool IsStageCleared => _isStageCleared;
        public bool IsGameOver => _isGameOver;
        public EnemyRuntimeModel CurrentEnemyRuntimeModel => _currentEnemyRuntimeModel;
        public int CurrentRoundIndex => _currentRoundIndex + 1;

        public StageProgressController(
            StageData stageData,
            PlayerHealthModel playerHealthModel,
            EnemyPresenter enemyPresenter)
        {
            _stageData = stageData;
            _playerHealthModel = playerHealthModel;
            _enemyPresenter = enemyPresenter;
            _currentRoundIndex = 0;

            _playerHealthModel.OnPlayerDied += HandlePlayerDied;
        }

        /// <summary>
        ///     イベントの購読解除など、ステージの状態をリセットするための処理。
        /// </summary>
        public void Reset()
        {
            _playerHealthModel.OnPlayerDied -= HandlePlayerDied;
            UnsubscribeEnemy();
        }

        /// <summary>
        ///     ステージ(ゲーム）の開始処理。
        /// </summary>
        public void StartStage()
        {
            _currentRoundIndex = 0;
            _isStageCleared = false;
            _isGameOver = false;

            StartRound();
        }

        public void Updete(float deltaTime)
        {
            if (_isStageCleared || _isGameOver)
            {
                return;
            }
            _currentEnemyBattleProvider?.Update(deltaTime);
        }

        public void ReduceEnemySkillTurn()
        {
            if (_isStageCleared || _isGameOver)
            {
                return;
            }
            _currentEnemyBattleProvider?.ReduceSkillTurn();
        }

        public void DamageCurrentEnemy(int damage)
        {
            if (_isStageCleared || _isGameOver)
            {
                return;
            }
            _currentEnemyRuntimeModel?.TakeDamage(damage);
        }

        private readonly StageData _stageData;
        private readonly PlayerHealthModel _playerHealthModel;
        private readonly EnemyPresenter _enemyPresenter;

        private int _currentRoundIndex;
        private bool _isStageCleared;
        private bool _isGameOver;
        private EnemyRuntimeModel _currentEnemyRuntimeModel;
        private EnemyAttackTimer _currentEnemyAttackTimer;
        private EnemySkillTurnTracker _currentEnemySkillTurnTracker;
        private EnemyBattleProvider _currentEnemyBattleProvider;

        /// <summary>
        ///     ラウンドを開始する。
        ///     現在のラウンドインデックスに基づいて、Enemy関連のモデルとビューを初期化する。
        /// </summary>
        private void StartRound()
        {
            if (_currentRoundIndex >= _stageData.RoundDatas.Count)
            {
                _isStageCleared = true;
                return;
            }

            // 今回のラウンドのEnemyDataを取得し、初期化する。
            RoundData currentRoundData = _stageData.RoundDatas[_currentRoundIndex];
            EnemyData enemyData = currentRoundData.EnemyData;

            _currentEnemyRuntimeModel = new EnemyRuntimeModel(enemyData);
            _currentEnemyRuntimeModel.OnEnemyDied += HandleEnemyDied;

            _currentEnemyAttackTimer = new EnemyAttackTimer(enemyData.AttackIntervalSeconds);
            _currentEnemySkillTurnTracker = new EnemySkillTurnTracker(enemyData.SkillTurnInterval);

            _currentEnemyBattleProvider = new EnemyBattleProvider(
                _currentEnemyRuntimeModel,
                _playerHealthModel,
                _currentEnemyAttackTimer,
                _currentEnemySkillTurnTracker
                );

            _enemyPresenter.Initialize(
                _currentEnemyRuntimeModel,
                _currentEnemyAttackTimer,
                _currentEnemySkillTurnTracker
                );
        }

        /// <summary>
        ///     プレイヤーの死亡時の処理をする。
        /// </summary>
        private void HandlePlayerDied()
        {
            _isGameOver = true;
            //TODO : ゲームオーバー処理をここに追加する。
        }

        /// <summary>
        ///     敵の死亡時の処理をする。
        /// </summary>
        private void HandleEnemyDied()
        {
            UnsubscribeEnemy();
            _currentRoundIndex++;

            if (_currentRoundIndex < _stageData.RoundDatas.Count)
            {
                StartRound();
            }
            else
            {
                _isStageCleared = true;
                //TODO : ステージクリア処理をここに追加する。
            }
        }

        /// <summary>
        ///     見ている敵のイベントの購読を解除する。
        /// </summary>
        private void UnsubscribeEnemy()
        {
            if (_currentEnemyRuntimeModel != null)
            {
                _currentEnemyRuntimeModel.OnEnemyDied -= HandleEnemyDied;
            }
        }
    }
}
