using System.Collections;
using UnityEngine;

public class BorderController : MonoBehaviour {
    [SerializeField] GameObject _leftArrows;
    [SerializeField] GameObject _rightArrows;

    [SerializeField] float _stepLenght;
    RectTransform _myRectTransform;
    public RectTransform MyRectTransform {
        get
        {
            if (_myRectTransform == null) {
                _myRectTransform = GetComponent<RectTransform>();
            }
            return _myRectTransform;
        }
    }

    public void MoveBorder(int step) {
        StartCoroutine(MoveAnimation(step));
    }

    IEnumerator MoveAnimation(int step) {
        if (step < 0) {
            _rightArrows.SetActive(true);
        } else {
            _leftArrows.SetActive(true);
        }
        LeanTween.value(gameObject, MyRectTransform.anchoredPosition.x, MyRectTransform.anchoredPosition.x + _stepLenght * step, 0.5f)
                 .setOnUpdate((float val) => {
            MyRectTransform.anchoredPosition = new Vector2(val, 0f);
        }).setEase(LeanTweenType.easeInOutCirc) ;
        yield return new WaitForSeconds(0.5f);
        _leftArrows.SetActive(false);
        _rightArrows.SetActive(false);
        yield return null;
    }
}
