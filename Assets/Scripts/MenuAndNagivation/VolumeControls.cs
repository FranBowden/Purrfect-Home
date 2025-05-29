using UnityEngine;
using UnityEngine.UI;

public class VolumeControls : MonoBehaviour
{

    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioSource backgroundMusic;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backgroundMusic.volume = volumeSlider.value;
        volumeSlider.onValueChanged.AddListener(SetVolume);

    }

    private void SetVolume(float value)
    {
        backgroundMusic.volume = Mathf.Pow(value, 2f);
    }
}
