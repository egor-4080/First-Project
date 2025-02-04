using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YG.Insides
{
    public class PlatformEventsYG2 : MonoBehaviour
    {
        public enum UpdateType
        {
            Awake,
            Start,
            OnEnable,
            OnDisable,
#if RU_YG2
            [InspectorName("������� (����� ExecuteEvent)")]
#endif
            Manual
        }

        public List<string> platforms = new List<string>();
        public UnityEvent platformAction;
        public UpdateType whenToEvent = UpdateType.Start;

        private void Awake()
        {
            if (whenToEvent == UpdateType.Awake)
                ExecuteEvent();
        }

#if UNITY_EDITOR
        private void Reset()
        {
            if (platformAction == null)
                platformAction = new UnityEvent();

            UnityEventTools.AddPersistentListener(platformAction, DeactivateGameObject);
        }
#endif

        private void Start()
        {
            if (whenToEvent == UpdateType.Start)
                ExecuteEvent();
        }

        private void OnEnable()
        {
            if (whenToEvent == UpdateType.OnEnable)
                ExecuteEvent();
        }

        private void OnDisable()
        {
            if (whenToEvent == UpdateType.OnDisable)
                ExecuteEvent();
        }

        public void ExecuteEvent()
        {
            if (platforms.Contains(YG2.platform))
            {
                platformAction?.Invoke();
            }
        }

        public void DeactivateGameObject()
        {
            gameObject.SetActive(false);
        }
    }
}