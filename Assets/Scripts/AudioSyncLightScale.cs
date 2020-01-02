using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncLightScale : AudioSyncer
{
    public float beatIntensity;
    public float restIntensity;

    public Color[] beatColors;
    public Color restColor;

    private int m_randomIndx;
    private IEnumerator MoveToScale(float _target)
    {
        float _curr = GetComponent<Light>().intensity;
        float _initial = _curr;
        float _timer = 0;

        while (_curr != _target)
        {
            _curr = Mathf.Lerp(_initial, _target, _timer / timeToBeat);
            _timer += Time.deltaTime;

            GetComponent<Light>().intensity = _curr;

            yield return null;
        }

        m_isBeat = false;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (m_isBeat) return;

        GetComponent<Light>().intensity = Mathf.Lerp(GetComponent<Light>().intensity, restIntensity, restSmoothTime * Time.deltaTime);
        GetComponent<Light>().color = Color.Lerp(GetComponent<Light>().color, restColor, restSmoothTime * Time.deltaTime);
    }

    public override void OnBeat()
    {
        base.OnBeat();

        StopCoroutine("MoveToScale");
        StartCoroutine("MoveToScale", beatIntensity);

        Color _c = RandomColor();

        StopCoroutine("MoveToColor");
        StartCoroutine("MoveToColor", _c);
    }

    private IEnumerator MoveToColor(Color _target)
    {
        Color _curr = GetComponent<Light>().color;
        Color _initial = _curr;
        float _timer = 0;

        while (_curr != _target)
        {
            _curr = Color.Lerp(_initial, _target, _timer / timeToBeat);
            _timer += Time.deltaTime;

            GetComponent<Light>().color = _curr;

            yield return null;
        }

        m_isBeat = false;
    }

    private Color RandomColor()
    {
        if (beatColors == null || beatColors.Length == 0) return Color.white;
        m_randomIndx = Random.Range(0, beatColors.Length);
        return beatColors[m_randomIndx];
    }
}