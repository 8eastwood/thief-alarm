using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound;
    private float _volume = 0f;

    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private bool _isPlaying = false;
    private float _delay = 0.5f;

    public void TurnAlarmOn()
    {
        _isPlaying = true;
        StartCoroutine(IncreaseVolume(_maxVolume));
    }

    public void TurnAlarmOff()
    {
        _isPlaying = false;
        StartCoroutine(DecreaseVolume(_minVolume));

        if (_alarmSound.volume == 0)
        {
            ResetVolume();
        }
    }

    private IEnumerator IncreaseVolume(float targetVolume)
    {
        _alarmSound.Play();

        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (_isPlaying == true)
        {
            if (_alarmSound.volume < targetVolume)
            {
                //_volume += 0.1f;
                _alarmSound.volume += 0.1f;
                Debug.Log("slowly increasing " + _alarmSound.volume);
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
                //_volume -= 0.1f;
                _alarmSound.volume -= 0.1f;
                Debug.Log("slowly fading " + _alarmSound.volume);
            }
            else
            {
                _alarmSound.Stop();
                break;
            }

            yield return wait;
        }

        Debug.Log("while decreasing is ended");
    }

    private void ResetVolume()
    {
        _alarmSound.volume = 0f;
        StopAllCoroutines();
    }
}
