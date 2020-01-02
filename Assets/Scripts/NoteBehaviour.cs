using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehaviour : MonoBehaviour
{
    Transform car;
    ScoreManager sManager;

    // Start is called before the first frame update
    void Start()
    {
        car = FindObjectOfType<KartController>().transform;
        sManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < (car.position.z - 2))
        {
            sManager.combo = 1;
            sManager.accuracy--;
            sManager.errors++;
            Destroy(gameObject);
        }
    }
}
