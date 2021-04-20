using UnityEngine;
using UnityEngine.UI;

public class TypeGameSelector : MonoBehaviour
{
    public static bool IsSingle;
    [SerializeField] private TypeSelectButton _typeSelectButton;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        IsSingle = false;
        _typeSelectButton.image.sprite = _typeSelectButton.TwoPlayerSprite;
    }
    public void TypeSelectButtonOnClick()
    {
        IsSingle = !IsSingle;
        if (IsSingle)
        {
            _animator.SetTrigger("IsVertical");
            _typeSelectButton.image.sprite = _typeSelectButton.SinglePlayerSprite;
        }
        else
        {
            _animator.SetTrigger("IsHorizontal");
            _typeSelectButton.image.sprite = _typeSelectButton.TwoPlayerSprite;
        }
    }
}