using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    Slider backgroundSlider, gameSoundSlider;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent.name == "Music Volume")
        {
            // Add control for background music
            backgroundSlider = GetComponent<Slider>();
            backgroundSlider.value = AudioManager.instance.GetBackgroundVolume(); // set the volume to the current volume

            if (backgroundSlider)
            {
                backgroundSlider.onValueChanged.AddListener(value => AudioManager.instance.ControlBackgroundVolume(backgroundSlider.value));
            }
        }

        if (transform.parent.name == "Volume")
        {
            // Add control for game sounds volume
            gameSoundSlider = GetComponent<Slider>();
            gameSoundSlider.value = AudioManager.instance.GetGameSoundVolume(); // set the volume to the current volume

            if (gameSoundSlider)
            {
                gameSoundSlider.onValueChanged.AddListener(value => AudioManager.instance.ControlGameSoundVolume(gameSoundSlider.value));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
