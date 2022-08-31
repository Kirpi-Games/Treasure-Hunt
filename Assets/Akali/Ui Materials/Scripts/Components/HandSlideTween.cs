using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Akali.Ui_Materials.Scripts.Components
{
    public class HandSlideTween : MonoBehaviour
    {
        private enum TweenType : byte
        {
            X,
            Y
        }

        [SerializeField] private TweenType type;
        [SerializeField] private GameObject other;
        private const float Duration = 0.8f;

        private void OnEnable()
        {
            var image = gameObject.GetComponent<Image>();
            var value = type is TweenType.X
                ? image.rectTransform.anchoredPosition.x
                : image.rectTransform.anchoredPosition.y;

            switch (type)
            {
                case TweenType.X:
                    image.rectTransform.DOAnchorPosX(-value, Duration)
                        .OnComplete(() =>
                        {
                            other.SetActive(true);
                            transform.parent.gameObject.SetActive(false);
                        });
                    break;
                case TweenType.Y:
                    image.rectTransform.DOAnchorPosY(-value, Duration)
                        .OnComplete(() =>
                        {
                            other.SetActive(true);
                            transform.parent.gameObject.SetActive(false);
                        });
                    break;
                default: return;
            }
        }

        private void OnDisable()
        {
            gameObject.transform.DOKill();
        }
    }
}