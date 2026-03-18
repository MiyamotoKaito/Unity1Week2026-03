using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; }

    public void PlayAttack(ICardEffect effect)
    {
        effect.Exucute();
    }
    public void PlayDefense(ICardEffect effect)
    {
        effect.Exucute();
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
