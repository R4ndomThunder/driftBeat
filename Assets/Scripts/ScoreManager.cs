using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Ranks
{
    SS, S, A, B, C
}

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    [HideInInspector]
    public int score, combo = 1;
    [HideInInspector]
    public float accuracy;
    KartController car;
    public Ranks ranks;

    [HideInInspector]
    public int errors = 0;
    private void Start()
    {
        accuracy = 100.0f;
        car = FindObjectOfType<KartController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (accuracy >= 99.5f)
            ranks = Ranks.SS;
        else if (accuracy >= 90.0f)
            ranks = Ranks.S;
        else if (accuracy >= 80)
            ranks = Ranks.A;
        else if (accuracy >= 70f)
            ranks = Ranks.B;
        else
            ranks = Ranks.C;


        scoreText.text = $"<color=#8B2635>S:</color><color=#4C86A8> {score.ToString("000000")}</color>\n<color=#8B2635>C:</color><color=#A5B452> x{combo.ToString("f0")} <color=#8B2635>R:</color> {ranks.ToString()}";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Note"))
        {
            var multiplier = 1;

            accuracy += 1;

            if (accuracy >= 100 && errors < 2)
                accuracy = 100;
            else if (accuracy >= 90 && errors >= 2)
                accuracy = 90;
            else if (accuracy >= 80 && errors >= 5)
                accuracy = 80;

            switch (ranks)
            {
                case Ranks.SS:
                    multiplier = 10;
                    break;
                case Ranks.S:
                    multiplier = 8;
                    break;
                case Ranks.A:
                    multiplier = 6;
                    break;
                case Ranks.B:
                    multiplier = 4;
                    break;
                case Ranks.C:
                    multiplier = 2;
                    break;
            }

            if (car.drifting)
                score += 20 * multiplier;
            else
                score += 10 * multiplier;

            combo++;
            Destroy(other.gameObject);
        }
    }
}
