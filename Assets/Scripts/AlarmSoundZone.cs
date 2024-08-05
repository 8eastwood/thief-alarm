using UnityEngine;

public class AlarmSoundZone : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private Alarm _alarm;

    private void OnTriggerEnter(Collider other)
    {
        _alarm.TurnAlarmOn();
    }

    private void OnTriggerExit(Collider other)
    {
        _alarm.TurnAlarmOff();
    }
}
