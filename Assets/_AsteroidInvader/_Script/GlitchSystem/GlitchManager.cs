using UnityEngine;
using System.Collections;
using System;
// by @JavierBullrich

public class GlitchManager : MonoBehaviour {
    public GameObject[] stuffToSpawn;
    public Kino.DigitalGlitch glitchCamera;

    void ShowEye(bool status)
    {
        foreach(GameObject stuf in stuffToSpawn)
        {
            stuf.SetActive(status);
        }
    }

    public void StartTheGlitch(Action skinChange, Action skinReturn)
    {
        StartCoroutine(StartGlitch(skinChange, skinReturn));
    }

    public void GlitchLevel(float glitch)
    {
        glitchCamera.intensity = glitch;
    }

    public void HintGlithc(float intensity, float time)
    {
        GlitchLevel(intensity);
        StartCoroutine(glitchHint(time));
    }

    IEnumerator glitchHint(float time)
    {
        yield return new WaitForSeconds(time);
        GlitchLevel(0f);
    }

    IEnumerator StartGlitch(Action skinChange, Action skinReturn)
    {
        GlitchLevel(0.3f);
        yield return new WaitForSeconds(0.2f);
        GlitchLevel(0.7f);
        yield return new WaitForSeconds(0.2f);
        GlitchLevel(0.9f);
        yield return new WaitForSeconds(0.3f);
        skinChange();
        GlitchLevel(0.5f);
        yield return new WaitForSeconds(0.2f);
        GlitchLevel(0.1f);
        yield return new WaitForSeconds(0.2f);
        ShowEye(true);
        yield return new WaitForSeconds(0.1f);

        float despawnTime = 0;
        float letterDelay = stuffToSpawn[1].GetComponent<UIText>().letterPause;
        foreach (char letter in stuffToSpawn[1].GetComponent<UIText>().message)
            despawnTime += letterDelay;
        despawnTime += 0.95f;
        yield return new WaitForSeconds(despawnTime);
        ShowEye(false);
        GlitchLevel(0.3f);
        yield return new WaitForSeconds(0.2f);
        GlitchLevel(0.7f);
        yield return new WaitForSeconds(0.2f);
        GlitchLevel(0.9f);
        yield return new WaitForSeconds(0.3f);
        skinReturn();
        GlitchLevel(0.5f);
        yield return new WaitForSeconds(0.2f);
        GlitchLevel(0f);
    }
}
