using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound;

    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private bool _isPlaying = false;
    private float _delay = 0.5f;
    private float _stepOfVolumeAlteration = 0.1f;

    public void TurnAlarmOn()
    {
        _isPlaying = true;
        StartCoroutine(IncreaseVolume(_maxVolume));
    }

    public void TurnAlarmOff()
    {
        _isPlaying = false;
        StartCoroutine(DecreaseVolume(_minVolume));

        if (_alarmSound.volume <= 0)
        {
            ResetVolume();
        }
    }

    private IEnumerator IncreaseVolume(float targetVolume)
    {
        _alarmSound.volume = _minVolume;
        _alarmSound.Play();

        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (_isPlaying == true)
        {
            if (_alarmSound.volume < targetVolume)
            {
                _alarmSound.volume += _stepOfVolumeAlteration;
            }
            else { break; }

            yield return wait;
        }
    }

    private IEnumerator DecreaseVolume(float targetVolume)
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (_isPlaying == false)
        {
            if (_alarmSound.volume > targetVolume)
            {
                _alarmSound.volume -= _stepOfVolumeAlteration;
            }
            else
            {
                _alarmSound.Stop();
                StopAllCoroutines();

                break;
            }

            yield return wait;
        }
    }

    private void ResetVolume()
    {
        _alarmSound.volume = 0f;
    }
}
