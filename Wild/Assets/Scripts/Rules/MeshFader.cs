using System.Collections;
using UnityEngine;

namespace Rules
{
    public class MeshFader : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;
        private Material _material;
        private Coroutine _fadeCoroutine;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _material = _meshRenderer.material;
        }

        public void FadeTo(float targetAlpha, float duration)
        {
            if (_fadeCoroutine != null)
                StopCoroutine(_fadeCoroutine);
            
            _fadeCoroutine = StartCoroutine(FadeRoutine(targetAlpha, duration));
        }

        private IEnumerator FadeRoutine(float targetAlpha, float duration)
        {
            var colour = _material.color;
            var startAlpha = colour.a;
            var time = 0f;

            while (time < duration)
            {
                var alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
                _material.color = new Color(colour.r, colour.g, colour.b, alpha);
                time += Time.deltaTime;
                yield return null;
            }
            
            _material.color = new Color(colour.r, colour.g, colour.b, targetAlpha);
        }
        
        public void FadeOut(float duration) => FadeTo(0f, duration);
        public void FadeIn(float duration) => FadeTo(1f, duration);
    }
}