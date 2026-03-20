using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     音楽関係を管理するクラス
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [System.Serializable]
    public class SoundData
    {
        public AudioClip Clip => _clip;
        public string Name => _name;
        public float Volume => _volume;

        [SerializeField] private AudioClip _clip;
        [SerializeField] private string _name;
        [SerializeField, Range(0, 1)] private float _volume;
    }
    [Header("プレイヤー")]
    [ReadOnly, SerializeField] private AudioSource _bgmPlayer;

    [Header("SEリスト")]
    [SerializeField] private List<SoundData> _seList;
    [Header("BGMリスト")]
    [SerializeField] private List<SoundData> _bgmList;

    /// <summary>
    ///     SE再生
    /// </summary>
    /// <param name="name"></param>
    /// <param name="volume"></param>
    public void PlaySE(string name)
    {
        foreach (var se in _seList)
        {
            if (se.Name == name)
            {
                GameObject sePlayer = new GameObject("SEPlayer");
                sePlayer.transform.SetParent(transform);

                var source = sePlayer.AddComponent<AudioSource>();
                source.volume = se.Volume;
                source.spatialBlend = 0f;
                source.clip = se.Clip;
                source.Play();
                Destroy(sePlayer, se.Clip.length);
            }
        }
    }

    /// <summary>
    ///     BGM再生(ループ)
    /// </summary>
    /// <param name="name"></param>
    public void PlayBGM(string name)
    {
        foreach (var bgm in _bgmList)
        {
            if (bgm.Name == name)
            {
                if (_bgmPlayer.clip == bgm.Clip && _bgmPlayer.isPlaying)
                    return;

                _bgmPlayer.clip = bgm.Clip;
                _bgmPlayer.loop = true;
                _bgmPlayer.volume = bgm.Volume;
                _bgmPlayer.Play();
            }
        }
    }

    /// <summary>
    ///     音楽のフェード
    /// </summary>
    /// <param name="fadeTime"></param>
    public void FadeBGM(float fadeTime)
    {
        _bgmPlayer.DOFade(0f, fadeTime);
    }

    /// <summary>
    ///     BGMのストップ
    /// </summary>
    public void Stop()
    {
        _bgmPlayer.Stop();
    }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        _bgmPlayer = GetComponentInChildren<AudioSource>();
        PlayBGM("BGM");
    }
}