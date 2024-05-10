using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundBarControl : MonoBehaviour
{
    public Image image;
    public AudioManager audioManager;

    Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeFillAmount()
    {
        image.fillAmount = slider.value;
    }

    public void ChangeRealSound()
    {
        audioManager.ChangeSound(slider.value);
    }
}
