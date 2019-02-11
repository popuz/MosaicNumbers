using System.Collections;
using UnityEngine;

public class SplashScreenImageUI : MonoBehaviour
{    
    [SerializeField] private AnimationCurve _animCurve;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Material _blurMat;

    private static readonly int _blurMatSizeProperty = Shader.PropertyToID("_Size");
    //[SerializeField] private MusicPlayer _musicPlayer;

    private const float START_BLUR_SIZE = 6f;
    private const float END_BLUR_SIZE = 4f;    
    private const float FADE_TIME = 2f;

    private void Awake() => _blurMat.SetFloat(_blurMatSizeProperty, START_BLUR_SIZE);

    private void Start()
    {
        if(!_canvasGroup.gameObject.activeSelf) 
            _canvasGroup.gameObject.SetActive(true);        
        
        StartCoroutine(FadeOut(FADE_TIME));
        //StartCoroutine(_musicPlayer.IntroPlay(0f, 5f));
    }

    private IEnumerator FadeOut(float fadeTime)
    {
        yield return FadeOutSplashImage(fadeTime);
        yield return FadeInBlurOfBKG(fadeTime);
    }

    private IEnumerator FadeOutSplashImage(float fadeTime)
    {
        var t = 0f;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            var animCurveValue = _animCurve.Evaluate(t / fadeTime);

            CanvasGroupAlphaFadeOut(animCurveValue);
            RemoveBlurAnimated(animCurveValue);
            yield return null;
        }
        _canvasGroup.gameObject.SetActive(false);
    }    
    private void CanvasGroupAlphaFadeOut(float fadeOutRate) => _canvasGroup.alpha = 1f - fadeOutRate;
    private void RemoveBlurAnimated(float lerpTime) => _blurMat.SetFloat(_blurMatSizeProperty, Mathf.Lerp(START_BLUR_SIZE, 0f, lerpTime));
    
    private IEnumerator FadeInBlurOfBKG(float fadeTime)
    {
        var t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            AddBlurAnimated(t / fadeTime);            
            yield return null;
        }       
    } 
    private void AddBlurAnimated(float lerpTime) => _blurMat.SetFloat(_blurMatSizeProperty, Mathf.Lerp(0f, END_BLUR_SIZE, lerpTime));
        
}