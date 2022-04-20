using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
namespace UniversalMobileController {
    public class SpecialButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public UnityEvent onButtondown;
        public UnityEvent onButtonup;
        [SerializeField] private State buttonState;
        public void OnPointerDown(PointerEventData eventData)
        {
            if (UniversalMobileController_Manager.editMode || buttonState == State.Un_Interactable) { return; }

            onButtondown.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (UniversalMobileController_Manager.editMode || buttonState == State.Un_Interactable) { return; }

            onButtonup.Invoke();
        }

        public void SetState(State state)
        {
            buttonState = state;
        }
        public State GetState()
        {
            return buttonState;
        }
    }
}
