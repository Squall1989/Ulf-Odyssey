using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightIntense : MonoBehaviour
{
    [SerializeField] private Light2D light;
    [SerializeField] private float intense;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FlashCorout());
    }

    private IEnumerator FlashCorout()
    {
        float intenseStart = 0;
        float step = intense / Application.targetFrameRate;
        while(true)
        {
            light.intensity += step;
            intenseStart += Mathf.Abs(step);

            if(Mathf.Abs(intenseStart) >= intense)
            {
                step *= -1;
                intenseStart = 0;
            }

            yield return null;
        }
    }
}
