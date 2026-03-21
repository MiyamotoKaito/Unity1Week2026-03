using Unity1Week.URA.System;
using UnityEngine;

public class MonoSceneLoad : MonoBehaviour
{
    public void LoadScene(string _sceneName)
    {
        SceneLoader.LoadScene(_sceneName);
    }

    [SerializeField] private string _sceneName;
}
