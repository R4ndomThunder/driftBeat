using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioSpectrum : MonoBehaviour
{
    public static float spectrumValue { get; private set; }
    public static AudioClip clip { get; private set; }
    public static AudioSource source { get; private set; }
    private float[] m_audioSpectrum;

    private void Start()
    {  
        m_audioSpectrum = new float[128];
        source = GetComponent<AudioSource>();
        clip = source.clip;

    }

    private void Update()
    {
        GetComponent<AudioSource>().GetSpectrumData(m_audioSpectrum, 0, FFTWindow.BlackmanHarris);
        if (m_audioSpectrum != null && m_audioSpectrum.Length > 0)
        {
            spectrumValue = m_audioSpectrum[0] * 100;
        }
    }
}