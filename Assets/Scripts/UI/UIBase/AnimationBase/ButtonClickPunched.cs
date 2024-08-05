using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ButtonClickPunched : MonoBehaviour , IPointerDownHandler, IPointerUpHandler {

    RectTransform rectTransform;
    public bool canAnimate = true;
    float duration = 0.1f;
    public AnimationCurve buttonCurve;
    Button btn;
    Toggle tgl;

    IEnumerator animate;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        //get button
        if( GetComponent<Button>()!=null)
            btn = GetComponent<Button>();
        else if( GetComponent<Toggle>()!=null)
            tgl = GetComponent<Toggle>();
    }

    enum PointerState
    {
        None,
        Down,
        Up
    }
    PointerState pState = PointerState.None;

    public virtual void OnPointerUp(PointerEventData data)
    {
        if (canAnimate && ((btn!=null) ?btn.IsInteractable():false))
        {
            pState = PointerState.Up;
            if (animate != null) StopCoroutine(animate);
            StartCoroutine(animate = Animate(new Vector2(0.9f, 0.9f), Vector2.one));
        }
        else if (canAnimate && ((tgl!=null) ?tgl.IsInteractable():false))
        {
            pState = PointerState.Up;
            if (animate != null) StopCoroutine(animate);
            StartCoroutine(animate = Animate(new Vector2(0.9f, 0.9f), Vector2.one));
        }
    }

    public virtual void OnPointerDown(PointerEventData data)
    {
        if (canAnimate && ((btn!=null) ?btn.IsInteractable():false))
        {
            pState = PointerState.Down;
            if (animate != null) StopCoroutine(animate);
            StartCoroutine(animate = Animate(Vector2.one, new Vector2(0.9f, 0.9f)));
        }
        else if (canAnimate && ((tgl!=null) ?tgl.IsInteractable():false))
        {
            pState = PointerState.Down;
            if (animate != null) StopCoroutine(animate);
            StartCoroutine(animate = Animate(Vector2.one, new Vector2(0.9f, 0.9f)));
        }
    }

    IEnumerator Animate(Vector2 fromScale, Vector2 toScale)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {

            float time = buttonCurve.Evaluate(elapsed / duration);
            rectTransform.localScale = Vector2.LerpUnclamped(fromScale, toScale, time);
            elapsed += Time.unscaledDeltaTime;
            yield return null;

        }
        rectTransform.localScale = toScale;
        yield return null;
    }
    
}
