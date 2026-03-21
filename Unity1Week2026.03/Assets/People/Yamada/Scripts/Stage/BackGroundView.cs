using UnityEngine;

namespace Unity1Week.URA.Stage
{
    /// <summary>
    ///     背景を管理するクラス。
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class BackGroundView : MonoBehaviour
    {
        public void SetBackGround(Sprite sprite)
        {
            this.GetComponent<SpriteRenderer>().sprite = sprite;
        }          
    }
}
