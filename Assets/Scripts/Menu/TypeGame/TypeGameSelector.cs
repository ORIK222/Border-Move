using UnityEngine;
using UnityEngine.UI;

public class TypeGameSelector : MonoBehaviour
{
    public static bool IsSingle;
    public static bool IsScore;

    [SerializeField] private TypeSelectButton _typeSelectButton;
    private Animator _animator;

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

    private void Start()
    {
        _animator = GetComponent<Animator>();
        IsSingle = false;
        _typeSelectButton.image.sprite = _typeSelectButton.TwoPlayerSprite;
    }
  
}