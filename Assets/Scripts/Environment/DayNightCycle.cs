using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light sun;
    public float secondsInFullDay = 120f; // 2 минуты на полный цикл дня
    [Range(0, 1)] public float currentTimeOfDay; // Текущее время суток, установленное вручную для начального положения солнца
    public float timeMultiplier = 1f;

    private float sunInitialIntensity;
    private Quaternion sunInitialRotation;

    void Start()
    {
        sunInitialIntensity = sun.intensity;
        sunInitialRotation = sun.transform.rotation;

        // Определение начального времени суток на основе текущего положения солнца
        currentTimeOfDay = CalculateCurrentTimeOfDay();
    }

    void Update()
    {
        UpdateSun();

        // Увеличение времени суток в зависимости от прошедшего времени
        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

        // Если время суток превышает 1, вернуть его к 0
        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay = 0;
        }
    }

    void UpdateSun()
    {
        // Поворот солнца в зависимости от текущего времени суток
        sun.transform.rotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);

        // Изменение интенсивности света в зависимости от времени суток
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
        // Определение времени суток на основе текущего положения солнца
        float normalizedAngle = Quaternion.Angle(sun.transform.rotation, sunInitialRotation) / 360f;
        return normalizedAngle + 0.25f; // Прибавляем 0.25, чтобы сделать полдень (12 часов) 0.5, а не 0
    }
}
