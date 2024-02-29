using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soabdmucontroler : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;

    public void MusicVolume()
    {
        SoundManager.Instance.MusicVolume(_musicSlider.value);
    }

    public void SoundVolume()
    {
        SoundManager.Instance.SoundVolume(_sfxSlider.value);
    }
}
