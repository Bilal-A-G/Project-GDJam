using UnityEngine;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public float fadeSpeed;
    public int hangTime;

    public Image cover;

    private float _coverAlpha;
    private bool _reverse;
    private static bool _fade;
    public void Fade()
    {
        if(_fade) return;
        
        cover.gameObject.SetActive(true);
        _fade = true;
    }

    private void Update()
    {
        if(!_fade) return;
        
        _coverAlpha += (_reverse ? -1 : 1) * Time.deltaTime * fadeSpeed;
        _coverAlpha = Mathf.Clamp(_coverAlpha, 0, 1 + hangTime);

        if (_coverAlpha == 1 + hangTime)
        {
            _reverse = true;
        }
        else if (_coverAlpha == 0 && _reverse)
        {
            _fade = false;
            _reverse = false;
            cover.gameObject.SetActive(false);
            return;
        }

        cover.color = new Color(cover.color.r, cover.color.g, cover.color.b, _coverAlpha);
    }
}