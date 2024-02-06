using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Ebac.Core.Singleton;

public class EffectManager : Singleton<EffectManager>
{
    public PostProcessVolume processVolume;
    public float duration = 1;
    [SerializeField] private Vignette _vignette;

    [NaughtyAttributes.Button]
    public void ChangeVignette()
    {
        StartCoroutine(FlashVignette());
    }

    IEnumerator FlashVignette()
    {
        Vignette tmp;

        if(processVolume.profile.TryGetSettings<Vignette>(out tmp))
        {
            _vignette =tmp;
        }

        ColorParameter c = new ColorParameter();

        float time = 0;
        float vLerp = 0;
        while(time < duration)
        {
            c.value = Color.Lerp(Color.black, Color.red, time / duration);
            vLerp = Mathf.Lerp(0f, .5f, time/duration);
            time += Time.deltaTime;
            _vignette.color.Override(c);
            _vignette.intensity.Override(vLerp); 
            yield return new WaitForEndOfFrame();
        }
        time = 0;
        while(time < duration)
        {
            c.value = Color.Lerp(Color.red, Color.black, time / duration);
            vLerp = Mathf.Lerp(.5f, 0f, time/duration);
            time += Time.deltaTime;
            _vignette.color.Override(c);
            _vignette.intensity.Override(vLerp); 
            yield return new WaitForEndOfFrame();
        }


    }
}
