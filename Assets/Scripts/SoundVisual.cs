using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVisual : MonoBehaviour
{
    private const int SAMPLES_SIZE = 1024;
    public bool circle;
    public float radius = 20f;
    public int amountVisual = 128;
    public float rmsValue;
    public float dbValue;
    public float pitchValue;

    public float maxVisualScale = 25f;
    public float visualModifier = 50f;
    public float smoothSpeed = 10f;
    public float keepPercentage = 0.5f;

    [ColorUsage(false,true)]
    public Color[] cutoffColors;
    public Material mat;
    private AudioSource source;
    private float[] samples;
    private float[] spectrum;
    private float sampleRate;

    private Transform[] visualList;
    private float[] visualScale;

    // Start is called before the first frame update
    void Start()
    {
        source = GameObject.Find("Music").GetComponent<AudioSource>();
        samples = new float[SAMPLES_SIZE];
        spectrum = new float[SAMPLES_SIZE];
        sampleRate = AudioSettings.outputSampleRate;

        if (!circle)
            SpawnLine();
        else
            SpawnCircle();
    }

    // Update is called once per frame
    void Update()
    {
        AnalyzeSound();
        UpdateVisual();
    }

    void UpdateVisual()
    {
        int visualIndex = 0;
        int spectrumIndex = 0;
        int averageSize = (int)((SAMPLES_SIZE * keepPercentage) / amountVisual);

        while (visualIndex < amountVisual)
        {
            int j = 0;
            float sum = 0;

            while (j < averageSize)
            {
                sum += spectrum[spectrumIndex];
                spectrumIndex++;
                j++;
            }

            float scaleY = sum / averageSize * visualModifier;
            visualScale[visualIndex] -= Time.deltaTime * smoothSpeed;
            if (visualScale[visualIndex] < scaleY)
                visualScale[visualIndex] = scaleY;
            if (visualScale[visualIndex] > maxVisualScale)
                visualScale[visualIndex] = maxVisualScale;

            visualList[visualIndex].localScale = Vector3.one + Vector3.up * visualScale[visualIndex];
            visualList[visualIndex].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", cutoffColors[(int)visualScale[visualIndex].Remap(0, maxVisualScale, 0, cutoffColors.Length-1)]);
            visualIndex++;

        }

    }

    private void SpawnLine()
    {
        visualScale = new float[amountVisual];
        visualList = new Transform[amountVisual];

        for (int i = 0; i < amountVisual; i++)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube) as GameObject;

            go.transform.parent = transform;
            go.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(mat);
            visualList[i] = go.transform;
            visualList[i].position = transform.position + (Vector3.right * i);
        }
    }

    private void SpawnCircle()
    {
        visualScale = new float[amountVisual];
        visualList = new Transform[amountVisual];

        Vector3 center = transform.position;


        for (int i = 0; i < amountVisual; i++)
        {
            float ang = i * 1f / amountVisual;
            ang = ang * Mathf.PI * 2;

            float x = center.x + Mathf.Cos(ang) * radius;
            float y = center.y + Mathf.Sin(ang) * radius;

            Vector3 pos = center + new Vector3(x, 0, y);

            var go = GameObject.CreatePrimitive(PrimitiveType.Cube) as GameObject;
            go.transform.parent = transform;
            go.transform.position = pos;
            go.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(mat);
            go.transform.rotation = Quaternion.Euler(Vector3.zero);
            visualList[i] = go.transform;
        }

    }

    private void AnalyzeSound()
    {
        source.GetOutputData(samples, 0);

        var i = 0;
        float sum = 0;
        for (; i < SAMPLES_SIZE; i++)
        {
            sum = samples[i] * samples[i];
        }

        rmsValue = Mathf.Sqrt(sum / SAMPLES_SIZE);

        dbValue = 20 * Mathf.Log10(rmsValue / 0.1f);

        source.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
    }
}
