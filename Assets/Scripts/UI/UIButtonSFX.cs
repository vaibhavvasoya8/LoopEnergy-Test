using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonSFX : MonoBehaviour
{
    Button _button;
    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    private void OnEnable()
    {
        _button.onClick.AddListener(PlaySFX);
    }
    private void OnDisable()
    {
        _button.onClick.RemoveListener(PlaySFX);
    }

    private void PlaySFX()
    {
        AudioManager.instance.Play(AudioType.UIButton);
    }
}
