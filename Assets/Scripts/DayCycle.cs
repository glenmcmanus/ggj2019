﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DayCycle : MonoBehaviour
{
    public Light light;
    public Color daylight;
    public Color moonlight;

    [Tooltip("The duration of a day (in terms of daylight)")]
    public float length = 180f;
    [Tooltip("Duration between day-to-night update ticks")]
    public float stepDelay = 1f;
    WaitForSeconds delay;

    public PostProcessProfile dayProfile;
    Bloom dayBloom;
    Vignette dayVignette;
    ColorGrading dayGrading;


    public PostProcessProfile nightProfile;
    Bloom nightBloom;
    Vignette nightVignette;
    ColorGrading nightGrading;

    public PostProcessVolume volume;
    Bloom bloom;
    Vignette vignette;
    ColorGrading colorGrading;

    private void Awake()
    {
        dayBloom = dayProfile.GetSetting<Bloom>();
        dayVignette = dayProfile.GetSetting<Vignette>();
        dayGrading = dayProfile.GetSetting<ColorGrading>();

        nightBloom = nightProfile.GetSetting<Bloom>();
        nightVignette = nightProfile.GetSetting<Vignette>();
        nightGrading = nightProfile.GetSetting<ColorGrading>();

        bloom = volume.profile.GetSetting<Bloom>();
        vignette = volume.profile.GetSetting<Vignette>();

        colorGrading = volume.profile.GetSetting<ColorGrading>();

        delay = new WaitForSeconds(stepDelay);

        StartCycle();
    }

    public void StartCycle()
    {
        StopAllCoroutines();
        light.color = daylight;

        bloom.intensity.value = dayBloom.intensity.value;
        bloom.color.value = dayBloom.color.value;
        bloom.threshold.value = dayBloom.threshold.value;

        vignette.intensity.value = dayVignette.intensity.value;
        vignette.color.value = dayVignette.color.value;
        vignette.smoothness.value = dayVignette.smoothness.value;

        StartCoroutine(DayToNight());
    }

    public IEnumerator DayToNight()
    {
        float initTime = Time.time;
        float curTime = Time.time - initTime;
        float t;
        while (curTime < length)
        {
            //Debug.Log("Interpolating pp effects");
            curTime = Time.time - initTime;
            //Debug.Log("curTime: " + curTime);
            t = curTime / length;
            bloom.intensity.Interp(dayBloom.intensity, nightBloom.intensity, t);
            bloom.color.Interp(dayBloom.color, nightBloom.color, t);
            bloom.threshold.Interp(dayBloom.threshold, nightBloom.threshold, t);

            vignette.intensity.Interp(dayVignette.intensity, nightVignette.intensity, t);
            vignette.color.Interp(dayVignette.color, nightVignette.color, t);
            vignette.smoothness.Interp(dayVignette.smoothness, nightVignette.smoothness, t);
            vignette.opacity.Interp(dayVignette.opacity, nightVignette.opacity, t);

            colorGrading.temperature.Interp(dayGrading.temperature, nightGrading.temperature, t);
            colorGrading.colorFilter.Interp(dayGrading.colorFilter, nightGrading.colorFilter, t);
            colorGrading.postExposure.Interp(dayGrading.postExposure, nightGrading.postExposure, t);
            colorGrading.contrast.Interp(dayGrading.contrast, nightGrading.contrast, t);

            light.color = Vector4.Lerp(daylight, moonlight, t);

            yield return stepDelay;
        }

        //Debug.Log("Finished interpolating pp effects");
    }
}
