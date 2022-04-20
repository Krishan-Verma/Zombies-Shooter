using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UniversalMobileController {
    public class FloatingJoyStick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
    {
        [SerializeField] private RectTransform joystickBackground;
        [SerializeField] private RectTransform joyStick;

        [Range(0, 10f)] [SerializeField] private float joystickMovementRange = 1f;

        private Vector2 joyStickInput = Vector2.zero;


        private Vector2 joystickCurrentPosition = new Vector2(0, 0);
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color pressedColor = Color.white;
        [SerializeField] private State joystickState;
        /*List of events*/
        public UnityEvent onGameStart;
        public UnityEvent onPressedJoystick;
        public UnityEvent onStartedDraggingJoystick;
        public UnityEvent onStoppedDraggingJoystick;

        private void Start()
        {
            onGameStart.Invoke();
            SetJoystickColor(normalColor);
        }

        public void OnPointerDown(PointerEventData eventdata)
        {
            if (UniversalMobileController_Manager.editMode || joystickState == State.Un_Interactable) { return; }
            SetJoystickColor(pressedColor);
            joystickCurrentPosition = eventdata.position;
            joystickBackground.position = eventdata.position;
            joyStick.anchoredPosition = Vector2.zero;
            OnDrag(eventdata);
            onPressedJoystick.Invoke();
        }
        public void OnPointerUp(PointerEventData eventdata)
        {
            if (UniversalMobileController_Manager.editMode || joystickState == State.Un_Interactable) { return; }
            SetJoystickColor(normalColor);

            joyStickInput = new Vector2(0, 0);
            joyStick.anchoredPosition = new Vector2(0, 0);
            onStoppedDraggingJoystick.Invoke();
        }
        public void OnDrag(PointerEventData eventdata)
        {
            if (UniversalMobileController_Manager.editMode || joystickState == State.Un_Interactable) { return; }

            onStartedDraggingJoystick.Invoke();
            Vector2 direction = eventdata.position - joystickCurrentPosition;

            if (direction.magnitude > joystickBackground.sizeDelta.x / 2f)
            {
                joyStickInput = direction.normalized;
            }
            else
            {
                joyStickInput = direction / (joystickBackground.sizeDelta.x / 2f);
            }

            joyStick.anchoredPosition = (joyStickInput * joystickBackground.sizeDelta.x / 2f) * joystickMovementRange;
        }

        private void SetJoystickColor(Color color)
        {
            joystickBackground.gameObject.GetComponent<Image>().color = color;
            joyStick.gameObject.GetComponent<Image>().color = color;
        }

        public float GetVerticalValue()
        {
            return joyStickInput.y;
        }
        public float GetHorizontalValue()
        {
            return joyStickInput.x;
        }
        public Vector2 GetHorizontalAndVerticalValue()
        {
            return joyStickInput;
        }
        public void SetState(State state)
        {
            joystickState = state;
        }

        public State GetState()
        {
            return joystickState;
        }
    }
    public enum State
    {
        Interactable, Un_Interactable
    }
}