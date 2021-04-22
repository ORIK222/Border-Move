using UnityEngine;
using UnityEngine.UI;

public class TypeSelectButton : MonoBehaviour
{    
    public Image image
    {
        get { return _image;}
        set { _image = value; }
    }
    public Sprite SinglePlayerSprite;
    public Sprite TwoPlayerSprite;
    private Image _image;


    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    private void Start()
    {
        if (TypeGameSelector.IsSingle)
            _image.sprite = SinglePlayerSprite;
        else
            _image.sprite = TwoPlayerSprite;
    }
}
