
using System.Collections.Generic;
using Unity1Week.URA.Stage;
using UnityEngine;
public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; }
    private StageProgressController _stageProgressController;
    private CardPresenter _cardPresenter;
    private GridCard _gridCard;
    public void Initialize(StageProgressController stageProgressController, CardPresenter cardPresenter,GridCard gridCard)
    {
        _stageProgressController = stageProgressController;
        _cardPresenter = cardPresenter;
        _gridCard = gridCard;
    }

    public void PlayAttack(int damage)
    {
        _stageProgressController.DamageCurrentEnemy(damage);
    }
    public void PlayHeal(int healAmount)
    {
        _stageProgressController.HealPlayer(healAmount);
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
        _cardPresenter.ReversTexts();
    }
    public void DuplicateJammerPair(List<CardData> dataList)
    {
        _cardPresenter.ReplaceCardContents(dataList);
    }

    public void ReduceCurrentPower(int value)
    {
        throw new System.NotImplementedException();
    }

    public void ShuffleAllCardPositions()
    {
        _gridCard.GridCards();
    }

    public void ShuffleSomeCardTexts(int shuffleCount)
    {
        _cardPresenter.ShuffleSomeCards(shuffleCount);
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
}
