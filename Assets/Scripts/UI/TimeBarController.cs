using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBarController : MonoBehaviour {

    [SerializeField] List<Image> _images;
    float _progress = 0;
    float _roundLenght;
    bool _active;
    public float Progress {
        get { return _progress; }
        set {
            _progress = value; 
            UpdateProgress(_progress);
        }
    }

    public static Multiplier.MultiplierType type = Multiplier.MultiplierType.Good;

    private void Start()
    {
        Progress = 0f;
    }

    private void Update()
    {
        if (_active && Progress > 0f) {
            Progress -= (Time.deltaTime / _roundLenght);
        }
    }

    private void UpdateProgress(float progress) 
    {
        for (int i = 0; i < _images.Count; i++)
        {
            _images[i].fillAmount = progress;
        }
    } 
    private IEnumerator GetReadyCountDown() 
    {
        Progress = 1.0f;
        yield return null;
    }

    public Multiplier.MultiplierType GetMultiplierTypeDependingOnTheReaction()
    {
        if (_images[0].fillAmount > 0.8f)
            type = Multiplier.MultiplierType.Excellent;
        else if (_images[0].fillAmount > 0.65f)
            type = Multiplier.MultiplierType.Great;
        else 
            type = Multiplier.MultiplierType.Good;
        return type;

    }
    public void SignalGetReady() {
        _active = false;
        StartCoroutine(GetReadyCountDown());
    }    
    public void StartRound(float roundLenght) {
        Progress = 1.0f;
        _roundLenght = roundLenght;
        _active = true;
    }


}
