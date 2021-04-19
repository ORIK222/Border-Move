using UnityEngine;
using UnityEngine.UI;

public class TypeGameSelector : MonoBehaviour
{
    public static bool IsSingle;
    //[SerializeField] private UIPosition _uiPosition;
    [SerializeField] private TypeSelectButton _typeSelectButton;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        //_uiPosition.SetPosition();
    }
    public void TypeSelectButtonOnClick()
    {
        IsSingle = !IsSingle;
        if (IsSingle)
        {
            _animator.SetTrigger("IsVertical");
            _typeSelectButton.image.sprite = _typeSelectButton.SinglePlayerSprite;
            //_uiPosition.ChangePosition();
        }
        else
        {
            _animator.SetTrigger("IsHorizontal");
            _typeSelectButton.image.sprite = _typeSelectButton.TwoPlayerSprite;
            //_uiPosition.ChangePosition();
        }
    }
}