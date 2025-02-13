using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound;

    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private float _delay = 0.5f;
    private float _stepOfVolumeAlteration = 0.1f;

    private Coroutine _coroutine;

    public void TurnAlarmOn()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(ChangeAlarmVolume(_maxVolume));
    }

    public void TurnAlarmOff()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        StartCoroutine(ChangeAlarmVolume(_minVolume));

        if (_alarmSound.volume <= _minVolume)
        {
            ResetVolume();
        }
    }

    private IEnumerator ChangeAlarmVolume(float targetVolume)
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        if (_alarmSound.isPlaying == false)
        {
            _alarmSound.Play();
        }

        while (_alarmSound.volume != targetVolume)
        {
            _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, targetVolume, _stepOfVolumeAlteration);
            
            yield return wait;
        }

        if (_alarmSound.volume <= _minVolume)
        {
            _alarmSound.Stop();
            StopAllCoroutines();
        }
    }

    private void ResetVolume()
    {
        _alarmSound.volume = 0f;
    }
}
