using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private Slider _musicSlider;

    private static float _musicVolume;
    public static float MusicVolume
    {
        get { return _musicVolume; }
        private set { }
    }
    private void Start()
    {
        _soundSlider.value = MusicManager.MusicVolume;
        _musicSlider.value = MusicManager.MusicVolume;
        _musicVolume = _musicSlider.value;
    }

    private void Update()
    {
        SetVolume();
    }

    private void SetVolume()
    {

        _musicVolume = _musicSlider.value;
        MusicManager.setMusicVolume(_musicSlider.value);
        MusicManager.setSoundVolume(_soundSlider.value);
    }
}