using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace CodTools.UI
{
    public class SceneTransitionPanel : MonoBehaviour
    {
        [SerializeField] Image panel;

        public void FadeIn(float duration, Action callback) {
            panel.gameObject.SetActive(true);
            panel.DOFade(1, duration).OnComplete(() => {
                panel.gameObject.SetActive(false);
                callback();
            });
        }

        public void FadeOut(float duration, Action callback) {
            panel.gameObject.SetActive(true);
            panel.DOFade(0, duration).OnComplete(() => {
                panel.gameObject.SetActive(false);
                callback();
            });
        }
    }
}
