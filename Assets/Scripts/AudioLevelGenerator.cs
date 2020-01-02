using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLevelGenerator : AudioSyncer
{
    float clipLength;
    [SerializeField]
    float distanceTogo = 100;
    [SerializeField]
    float clipSpeed = 5;
    [SerializeField]
    float sineSize = 5;
    [SerializeField]
    int amplitude = 2;
    float speed;
    public GameObject Note;
    public Material mat;
    [ColorUsage(false, true)]
    public Color[] notesColor;


    public bool started = false;

    KartController car;

    private void Start()
    {
        car = FindObjectOfType<KartController>();        
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (m_isBeat) return;

        if (clipLength == 0)
            clipLength = AudioSpectrum.clip.length / clipSpeed;
        if (speed == 0)
        {
            speed = int.Parse((distanceTogo / clipLength).ToString("f0"));
            car.maxSpeed = speed * 4.7f;
        }

        if (AudioSpectrum.source.isPlaying && !started)
            started = true;

        if (!AudioSpectrum.source.isPlaying && started && GameObject.FindGameObjectsWithTag("Note").Length == 0)
            Application.LoadLevel(0);
    }

    public override void OnBeat()
    {
        base.OnBeat();
        var x = Mathf.Sin(Mathf.PingPong(Time.time,amplitude)*sineSize);
        var newPos = new Vector3(x, 0, transform.position.z);
        transform.position = newPos;
        var g = Instantiate(Note, transform.position, Quaternion.identity);        
        g.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(mat);
        g.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", notesColor[Random.Range(0, notesColor.Length)]);
    }
}
