using Unity1Week.URA.Enemy;
using Unity1Week.URA.Player;
using UnityEngine;

namespace Unity1Week.URA.Stage
{
    /// <summary>
    ///     バトル全体の初期化と管理を行うクラス。
    /// </summary>
    public class BattleInstaller : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private int _playerMaxHealth;
        [SerializeField] private PlayerHealthPresenter _playerHealthPresenter;

        [Header("Stage")]
        [SerializeField] private EnemyPresenter _enemyPresenter;
        [SerializeField] private EnemyDebugDamageDealer _enemyDebugDamageDealer;
        [SerializeField] private CardController _cardController;
        [SerializeField] private CardPresenter _cardPresenter;
        [SerializeField] private GridCard _gridCard;

        [SerializeField] private RoundTransitionView _roundTransitionView;
        [SerializeField] private BackGroundView _backGroundView;

        private PlayerHealthModel _playerHealthModel;
        private StageProgressController _stageProgressController;

        /// <summary>
        ///     プレイヤーを初期化する。
        /// </summary>
        private void InitializePlayer()
        {
            _playerHealthModel = new PlayerHealthModel(_playerMaxHealth);
            _playerHealthPresenter.Initialize(_playerHealthModel);
        }

        /// <summary>
        ///     ステージを初期化する。
        /// </summary>
        private void InitializeStage()
        {
            StageData stageData = StageSelectionContext.SelectedStageData;

            if (stageData == null)
            {
                Debug.LogError("StageDataがない");
                return;
            }

            _stageProgressController = new StageProgressController(
                stageData,
                _playerHealthModel,
                _enemyPresenter,
                _cardController,
                _cardPresenter);

            _stageProgressController.OnRoundStarted += HandleRoundstart;
        }

        /// <summary>
        ///     カードの初期化をする。
        /// </summary>
        private void InitializeCardController()
        {
            _cardController.Init();
            _cardPresenter.StartEvent();
            EffectManager.Instance.Initialize(_stageProgressController, _cardPresenter, _gridCard, _cardController);
        }

        private void InitializeDebug()
        {
            // _enemyDebugDamageDealer.Initialize(_stageProgressController);
        }

        private void HandleRoundstart(int round, RoundData roundData)
        {
            _roundTransitionView.Play(
                roundData.RoundStartMessage,
                () =>
                {
                    _backGroundView.SetBackGround(roundData.BackGroundSprite);
                    _stageProgressController.StartRound();
                },
                () =>
                {
                    _stageProgressController.TrnsitionComplete();
                },
                round == 1
                );
        }

        private void Awake()
        {
            InitializePlayer();
            InitializeStage();
            InitializeCardController();
            InitializeDebug();
        }

        private void Start()
        {
            _stageProgressController.StartStage();
        }

        private void Update()
        {
            _stageProgressController?.Updete(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _stageProgressController?.Reset();
        }
    }
}
