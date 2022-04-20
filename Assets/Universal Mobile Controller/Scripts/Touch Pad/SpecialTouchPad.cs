using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
namespace UniversalMobileController
{
    public class SpecialTouchPad : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        private Vector2 distanceBetweenTouch;
        private Vector2 PointerOld;
        private int eventPointerID;
        private bool pressingTouchPad;
        private Vector2 touchPadInput;
        [Range(0.2f, 20)]
        public float senstivityX = 3f;
        [Range(0.2f, 20)]
        public float senstivityY = 3f;
        public State touchPadState;
        public UnityEvent onGameStart;
        public UnityEvent onPressedTouchPad;
        public UnityEvent onStartedDraggingTouchPad;
        public UnityEvent onStoppedDraggingTouchPad;

        void Update()
        {
            if (UniversalMobileController_Manager.editMode|| touchPadState == State.Un_Interactable) { return; }

            if (pressingTouchPad)
            {

                if (eventPointerID < Input.touches.Length && eventPointerID >= 0)
                {
                    distanceBetweenTouch = Input.touches[eventPointerID].position - PointerOld;
                    PointerOld = Input.touches[eventPointerID].position;
                }
                else
                {
                    distanceBetweenTouch = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
                    PointerOld = Input.mousePosition;
                }
            }
            else
            {
                distanceBetweenTouch = Vector2.zero;
            }
            touchPadInput.x = distanceBetweenTouch.x * Time.deltaTime * senstivityX;
            touchPadInput.y = distanceBetweenTouch.y * Time.deltaTime * senstivityY;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            pressingTouchPad = true;
            eventPointerID = eventData.pointerId;
            PointerOld = eventData.position;
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            pressingTouchPad = false;
        }
        public float GetVerticalValue()
        {
            return touchPadInput.y;
        }
        public float GetHorizontalValue()
        {
            return touchPadInput.x;
        }
        public Vector2 GetHorizontalAndVerticalValue()
        {
            return touchPadInput;
        }
        public void SetState(State state)
        {
            touchPadState = state;
        }

        public State GetState()
        {
            return touchPadState;
        }
    }
}