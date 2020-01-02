using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioSyncInstantiation : AudioSyncer
{
    float clipLength;
    [SerializeField]
    float distanceTogo = 100;
    [SerializeField]
    float clipSpeed = 5;
    float speed;
    public GameObject Note;

    private void Start()
    {
        clipLength = FindObjectsOfType<AudioSource>().FirstOrDefault(aS => aS.isPlaying).GetComponent<AudioSource>().clip.length / clipSpeed;
        speed = distanceTogo / clipLength;
    }

    private void Update()
    {
        if (transform.position.z >= distanceTogo)
            Destroy(gameObject);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        Debug.Log("ojoafk");
        if (m_isBeat) return;
    }

    public override void OnBeat()
    {
        base.OnBeat();
        Debug.Log("MANNAGGIADIO");
        StopCoroutine("InstantiateObject");
        StartCoroutine("InstantiateObject");
    }

    public IEnumerator InstantiateObject()
    {
        Debug.Log("Nota!");
        Instantiate(Note, transform.position, Quaternion.identity);
        yield return null;
        m_isBeat = false;
    }


}
