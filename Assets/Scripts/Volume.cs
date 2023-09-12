using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    private AudioSource audioSource;

    // Start is called before the first frame update
    private void Start()
    {
        // Get the audio source component
        audioSource = GetComponent<AudioSource>();

        // Initialize the slider value to the current audio source volume
        volumeSlider.value = audioSource.volume;
    }

    public void ChangeVolume()
    {
        // Get the slider value
        float volume = volumeSlider.value;

        // Update the audio source volume based on the slider value
        audioSource.volume = volume;
    }
}
