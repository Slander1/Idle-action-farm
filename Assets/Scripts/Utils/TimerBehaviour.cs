using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public abstract class TimerBehaviour : MonoBehaviour
    {
        protected abstract float TimerCooldown { get; }
        private Coroutine _coroutine;
        private void OnEnable()
        {
            StartTimer();
        }

        private void OnDisable()
        {
            StopTimer();
        }

        protected void StartTimer()
        {
            _coroutine = StartCoroutine(TimerCoroutine());
        }
        protected void StopTimer()
        {
            StopCoroutine(_coroutine);
        }

        private IEnumerator TimerCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(TimerCooldown);
                OnTimer();
            }
        }

        protected abstract void OnTimer();
    }
}