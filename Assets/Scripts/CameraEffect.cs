using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraEffect : AudioSyncer
{
    [SerializeField]
    float earthquakeTime = 0.5f;
    [SerializeField]
    float earthquakeMagnitude = 0.25f;

    private Volume postVolume;
    private VolumeProfile postProfile;

    Vector3 startRot;

    private void Start()
    {
        postVolume = Camera.main.GetComponent<Volume>();
        postProfile = postVolume.profile;
        startRot = transform.rotation.eulerAngles;
    }

    public override void OnBeat()
    {
        base.OnBeat();
        StartCoroutine(EarthQuake());
    }

    public IEnumerator EarthQuake()
    {
        //postProfile.GetSetting<ChromaticAberration>().spectralLut.overrideState = false;
        //postProfile.GetSetting<ChromaticAberration>().intensity.value = .5f;
        //Vector3 originalPos = Camera.main.transform.position;
        Vector3 rotationAmount = Random.insideUnitSphere * earthquakeMagnitude;
        float elapsed = 0f;

        while (elapsed < earthquakeTime)
        {
            float x = Random.Range(-1f, 1f) * earthquakeMagnitude;
            float z = Random.Range(-1f, 1f) * earthquakeMagnitude;

            //Camera.main.transform.localPosition = new Vector3(x, originalPos.y, z);
            transform.localRotation = Quaternion.Euler(rotationAmount + startRot);
            elapsed += Time.deltaTime;

            yield return null;
        }

        Camera.main.transform.localRotation = Quaternion.Euler(startRot);
        //postProfile.GetSetting<ChromaticAberration>().spectralLut.overrideState = true;
        //postProfile.GetSetting<ChromaticAberration>().intensity.value = 0;

        StopCoroutine(EarthQuake());
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        
        Vector3 rotationAmount = new Vector3(5, 0, 15 * (-InputManager.GetHorizontalAxis()));
        transform.localRotation = Quaternion.Euler(rotationAmount);
        if (m_isBeat) return;
    }
}
