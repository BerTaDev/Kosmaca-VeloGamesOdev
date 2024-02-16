using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FixedTimeManager : MonoBehaviour
{

    public float slowMotionTimescale;
    public AudioMixer masterMixer;
    public float slowMotiotPitchScale;
    float activePitch;
    bool slowmo;
    private float startTimescale;
    private float startFixedDeltaTime;
    public static FixedTimeManager singleton;
    private void Awake()
    {
        singleton = this;
    }
    void Start()
    {
        startTimescale = Time.timeScale;
        startFixedDeltaTime = Time.fixedDeltaTime;
        masterMixer.SetFloat("Pitch", 1);
    }
    private void Update()
    {
        masterMixer.GetFloat("Pitch", out activePitch);
        if (slowmo && activePitch != slowMotiotPitchScale)
        {
            masterMixer.SetFloat("Pitch", Mathf.Lerp(activePitch, slowMotiotPitchScale, 10 * Time.deltaTime));
        }
        if (!slowmo && activePitch != 1)
        {
            masterMixer.SetFloat("Pitch", Mathf.Lerp(activePitch, 1, 10 * Time.deltaTime));
        }
    }
    public void StartSlowMotion()
    {
        slowmo = true;
        Time.timeScale = slowMotionTimescale;
        Time.fixedDeltaTime = startFixedDeltaTime * slowMotionTimescale;
    }

    public void StopSlowMotion()
    {
        slowmo = false;
        Time.timeScale = startTimescale;
        Time.fixedDeltaTime = startFixedDeltaTime;
    }

    public void SlowMo(float time)
    {
        StartCoroutine(slowmotion(time));
    }
    IEnumerator slowmotion(float time)
    {
        StartSlowMotion();
        yield return new WaitForSeconds(time);
        StopSlowMotion();
        StopCoroutine(slowmotion(0));
    }
}
