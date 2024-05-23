using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light sun;
    public float secondsInFullDay = 120f;
    [Range(0, 1)] public float currentTimeOfDay;
    public float timeMultiplier = 1f;

    private float sunInitialIntensity;
    private Quaternion sunInitialRotation;

    void Start()
    {
        sunInitialIntensity = sun.intensity;
        sunInitialRotation = sun.transform.rotation;
        currentTimeOfDay = CalculateCurrentTimeOfDay();
    }

    void Update()
    {
        UpdateSun();
        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;
        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay = 0;
        }
    }

    void UpdateSun()
    {
        sun.transform.rotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);
        
        float intensityMultiplier = 1;
        if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
        {
            intensityMultiplier = 0;
        }
        else if (currentTimeOfDay <= 0.25f)
        {
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
        }
        else if (currentTimeOfDay >= 0.73f)
        {
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
        }

        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }

    float CalculateCurrentTimeOfDay()
    {
        float normalizedAngle = Quaternion.Angle(sun.transform.rotation, sunInitialRotation) / 360f;
        return normalizedAngle + 0.25f; // Прибавляем 0.25, чтобы сделать полдень (12 часов) 0.5, а не 0
    }
}
