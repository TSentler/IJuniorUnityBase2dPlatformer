using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioVolumeSmooth : MonoBehaviour, IVolumable
    {
        [SerializeField] private float _duration = 3f;

        private AudioSource _audioSource;
        private Coroutine _audioVolumeCoroutine;

        private float CurrentVolume 
        {
            get => _audioSource.volume;
            set => _audioSource.volume = value;
        }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void StopAudioVolumeCoroutine()
        {
            if (_audioVolumeCoroutine != null)
                StopCoroutine(_audioVolumeCoroutine);
        }

        public void SetVolume(float targetVolume)
        {
            if (targetVolume > 0f && _audioSource.isPlaying == false)
            {
                _audioSource.Play();
            }

            StopAudioVolumeCoroutine();
            _audioVolumeCoroutine = StartCoroutine(AlarmVolumeChanging(targetVolume));
        }

        private IEnumerator AlarmVolumeChanging(float targetVolume)
        {
            var stepDirection = _audioSource.volume < targetVolume ? 1f : -1f;
            var elapsed = CurrentVolume;

            while (stepDirection * (CurrentVolume - targetVolume) < 0)
            {
                CurrentVolume = Mathf.Lerp(0f, 1f, elapsed);
                yield return new WaitForEndOfFrame();
                elapsed += stepDirection * Time.deltaTime / _duration;
            }

            CurrentVolume = targetVolume;
            if (Mathf.Approximately(CurrentVolume, 0f))
            {
                _audioSource.Stop();
            }
        }
    }
}
