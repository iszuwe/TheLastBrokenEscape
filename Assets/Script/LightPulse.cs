using UnityEngine;

public class LightPulse : MonoBehaviour
{
    public Light targetLight;
    public float pulseSpeed = 2f;
    public float minIntensity = 2f;
    public float maxIntensity = 5f;

    void Update()
    {
        targetLight.intensity = minIntensity + Mathf.PingPong(Time.time * pulseSpeed, maxIntensity - minIntensity);
    }
}
