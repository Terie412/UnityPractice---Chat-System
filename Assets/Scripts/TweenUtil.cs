using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TweenUtil: Singleton<TweenUtil>
{
    public static void ChangeAnchorPositionTo(RectTransform rt, Vector2 des, float t)
    {
        DOTween.To(
            () =>
            {
                return rt.anchoredPosition;
            },
            v =>
            {
                rt.anchoredPosition = v;
            },
            des, t
        );
    }

    public static void ChangeSliderValueTo(Slider s, float des, float t)
    {
        DOTween.To(
            () =>
            {
                return s.value;
            },
            v =>
            {
                s.value = v;
            },
            des, t
        );
    }
}
