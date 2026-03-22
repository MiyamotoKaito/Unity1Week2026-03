
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity1Week.URA.Stage;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; }
    private StageProgressController _stageProgressController;
    private CardPresenter _cardPresenter;
    private GridCard _gridCard;
    private CardController _cardController;
    private TextMeshProUGUI _effectValueText;
    private GameObject _effectValueTextObj;
    public void Initialize(StageProgressController stageProgressController,
        CardPresenter cardPresenter,
        GridCard gridCard,
        CardController cardController)
    {
        _stageProgressController = stageProgressController;
        _cardPresenter = cardPresenter;
        _gridCard = gridCard;
        _cardController = cardController;
    }

    public void PlayAttack(int damage)
    {
        _stageProgressController.DamageCurrentEnemy(damage);
    }
    public void PlayHeal(int healAmount)
    {
        _stageProgressController.HealPlayer(healAmount);
    }
    public void AddAtackTurn(int turn)
    {
        _stageProgressController.AddTurnToEnemy(turn);
    }
    public void AddEnemyAttackTimer(float time)
    {
        _stageProgressController.AddEnemyAttackTimer(time);
    }
    public void ReduceEnemyAttackTimer(float amount)
    {
        _stageProgressController.ReduceEnemyAttackTimer(amount);
    }
    public void ReduceEnemySkillTurn(int value = 1)
    {
        _stageProgressController.ReduceEnemySkillTurn(value);
    }
    public void ApplyMirrorFont()
    {
        _cardPresenter.MirrorTexts();
    }
    public void ApplyReverseText()
    {
        _cardController.ReverseTexts();
    }
    public void DuplicateJammerPair(List<CardData> dataList)
    {
        _cardPresenter.ReplaceCardContents(dataList);
    }

    public void ReduceCurrentPower(int value)
    {
        _cardPresenter.ReduceAttackPowerForAllAttackCards(value);
    }

    public void ShuffleAllCardPositions()
    {
        _gridCard.GridCards();
    }

    public void ShuffleSomeCardTexts(int shuffleCount)
    {
        _cardPresenter.ShuffleSomeCards(shuffleCount);
    }

    public void Clairvoyance()
    {
        _cardPresenter.Clairvoyance(4);
    }
    public void CardMatchText(Card card)
    {
        string effectText = string.Empty;
        if (card != null)
        {
            if (card.TryGetAttackBase(out var attack))
            {
                effectText = $"{attack.AttackPower}のダメージを与える";
            }
            else if (card.TryGetHealBase(out var heal))
            {
                effectText = $"{heal.HealAmount}の体力を回復する";
            }
            else if (card.TryGetTimeBase(out var time))
            {
                effectText = $"{time.TimeToAdd}秒間敵の攻撃を遅らせた";
            }
            else if (card.TryGetTurnBase(out var turn))
            {
                effectText = $"{turn.TurnToAdd}ターン敵の行動を遅らせた";
            }
            else if (card.TryClairvoyance(out var clairvoyance))
            {
                effectText = $"カードを4枚透視した";
            }
            else if(card.TryGetShuffleCardPositionSkill(out var shuffleCardPositionSkill))
            {
                effectText = $"敵がカードの位置をシャッフルした";
            }
            else if (card.TryGetShieldEnemySkill(out var shieldEnemySkill))
            {
                effectText = $"敵がシールドを張った";
            }
            else if (card.TryGetReducePowerSkill(out var reduceEnemyAttackTimerSkill))
            {
                effectText = $"プレイヤーの攻撃力が減少した";
            }
            else if(card.TryGetDuplicateJammerSkill(out var duplicateJammerSkill))
            {
                effectText = $"カードの内容が一部シャッフルされた";
            }
            else if(card.TryGetAllTextShuffleSkill(out var allTextShuffleSkill))
            {
                effectText = $"カードの内容が全てシャッフルされた";
            }
            
        }
        else
        {
            effectText = string.Empty;
        }
        if(_effectValueText == null)
        {
           _effectValueTextObj = GameObject.Find("Effect");
           _effectValueText = _effectValueTextObj?.GetComponentInChildren<TextMeshProUGUI>();
        }
        _effectValueText.text = effectText;
    }
    public void Play()
    {
       _effectValueText.DOFade(0f, 1f).SetEase(Ease.OutCubic).OnComplete(() =>
       {
            _effectValueText.text = string.Empty;
           _effectValueText.DOFade(1f, 0f).SetEase(Ease.InCubic);
       });
       _effectValueTextObj.transform.DOMoveY(_effectValueTextObj.transform.position.y + 20f, 1f).SetEase(Ease.OutCubic).OnComplete(() =>
       {
           _effectValueTextObj.transform.DOMoveY(_effectValueTextObj.transform.position.y - 20f, 1f).SetEase(Ease.InCubic);
       });
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _effectValueTextObj = GameObject.Find("Effect");
        _effectValueText = _effectValueTextObj?.GetComponentInChildren<TextMeshProUGUI>();
    }
}
