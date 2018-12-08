using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour {

    public Slider Slider;
    public AudioSource myMusic1;

    // Update is called once per frame
    void Update()
    {
        myMusic1.volume = Slider.value;
    }
}
