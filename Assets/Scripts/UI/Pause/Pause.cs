using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    [SerializeField] private Sprite _volumeOnSprite;
    [SerializeField] private Sprite _volumeOffSprite;
    [SerializeField] private Button _volumeOnOffButton;

    public void ResumeButtonOnClick()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void BackToMenuButtonOnClick()
    {
        Time.timeScale = 1f;
        GameManager.Instance.GameFlow.TransitToScene(UIConsts.SCENE_ID.MENU);
    }

    public void VolumeOnOffButtonOnClick()
    {
        if (_volumeOnOffButton.image.sprite == _volumeOnSprite)
        {
            _volumeOnOffButton.image.sprite = _volumeOffSprite;
            MusicManager.setMusicVolume(0f);

        }
        else
        {
            _volumeOnOffButton.image.sprite = _volumeOnSprite;
            MusicManager.setMusicVolume(0.3f);
        }

    }
}