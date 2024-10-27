using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class SoundSlidersHandler : MonoBehaviour
{
    public UnityEngine.UI.Slider SndVolumeSlider, MscVolumeSlider, AmbVolumeSlider;
    private SoundManager sndm;

    void Start()
    {
        sndm = SoundManager.sndm;
    }

    void OnEnable()
    {
        if (sndm != null && SndVolumeSlider != null)
        {
            SndVolumeSlider.value = SoundManager.SoundVolume;
            MscVolumeSlider.value = SoundManager.MusicVolume;
            AmbVolumeSlider.value = SoundManager.AmbientVolume;
        }
    }
    public void SetMusicVolume(){
        SoundManager.sndm.UpdateMusicVolume(MscVolumeSlider.value);
    }
    public void SetSoundVolume(){
        SoundManager.sndm.UpdateSoundVolume(SndVolumeSlider.value);
    }
    public void SetAmbientVolume(){
        SoundManager.sndm.UpdateAmbientVolume(AmbVolumeSlider.value);
    }
}
