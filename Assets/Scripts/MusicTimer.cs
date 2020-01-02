using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicTimer : MonoBehaviour
{
    AudioSource source;
    Image timerImg;
    TextMeshProUGUI clipNameText;
    // Start is called before the first frame update
    void Start()
    {
        source = FindObjectOfType<AudioSource>();
        timerImg = GetComponent<Image>();
        clipNameText = GetComponentInChildren<TextMeshProUGUI>();
        clipNameText.text = source.clip.name;
    }

    // Update is called once per frame
    void Update()
    {
        timerImg.fillAmount = source.time.Remap(0, source.clip.length, 0, 1);
    }
}
