using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniversalMobileController;

namespace UniversalMobileController
{
    public class MobileUIButton : MonoBehaviour, IDragHandler
    {
        private bool buttonExists = false;
        private bool isSelected = false;

        private RectTransform rectTransform;

        private Slider resizeSilder;

        private Color defualtColor;

        public ButtonsData buttonList;

        public string resizeSliderName = "ResizeSlider";

        public Canvas myCanvas;

        private void Start()
        {
            SetDefaultValues();
            StartCoroutine(LoadButtonData());

        }
        IEnumerator LoadButtonData()
        {

            yield return new WaitForSeconds(1f);
            if (buttonList.customizableButtonsList.Count > 0)
            {
                foreach (CustomizableButtons customizableButtons in buttonList.customizableButtonsList)
                {
                    if (customizableButtons.buttonName == gameObject.name)
                    {
                        buttonExists = true;
                        rectTransform.localPosition = new Vector2(customizableButtons.buttonPosX, customizableButtons.buttonPosY);
                        rectTransform.localScale = new Vector3(customizableButtons.buttonHeight, customizableButtons.buttonHeight);
                        break;
                    }
                }
                if (!buttonExists)
                {
                    CustomizableButtons buttonData = new CustomizableButtons(gameObject.name, rectTransform.localPosition.x, rectTransform.localPosition.y, 1, 1);
                    buttonList.customizableButtonsList.Add(buttonData);
                    buttonExists = true;
                }
            }
            else
            {
                CustomizableButtons buttonData = new CustomizableButtons(gameObject.name, rectTransform.localPosition.x, rectTransform.localPosition.y, 1, 1);
                buttonList.customizableButtonsList.Add(buttonData);
                buttonExists = true;
            }
        }
        private void Update()
        {
            if (UniversalMobileController_Manager.editMode)
            {
                if (isSelected)
                {
                    rectTransform.localScale = new Vector3(resizeSilder.value, resizeSilder.value, resizeSilder.value);
                    foreach (CustomizableButtons customizableButtons in buttonList.customizableButtonsList)
                    {
                        if (customizableButtons.buttonName == gameObject.name)
                        {
                            customizableButtons.buttonWidth = resizeSilder.value;
                            customizableButtons.buttonHeight = resizeSilder.value;
                        }
                    }
                }
            }
        }
        public void SetDefaultValues()
        {
            defualtColor = gameObject.GetComponent<Image>().color;
            myCanvas = gameObject.GetComponentInParent<Canvas>();
            rectTransform = GetComponent<RectTransform>();
            buttonList = Resources.Load<ButtonsData>(SaveLocationSettings.buttonDataPath);
        }
        public void PressButton()
        {
            if (UniversalMobileController_Manager.editMode)
            {
                if (isSelected)
                {
                    isSelected = false;
                    resizeSilder = null;
                    gameObject.GetComponent<Image>().color = defualtColor;
                }
                else
                {
                    isSelected = true;
                    resizeSilder = GameObject.Find(resizeSliderName).GetComponent<Slider>();
                    resizeSilder.value = rectTransform.localScale.x;
                    gameObject.GetComponent<Image>().color = Color.yellow;
                    Debug.Log(gameObject.name + "Selected " + isSelected);

                }

            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (UniversalMobileController_Manager.editMode)
            {

                if (isSelected)
                {
                    Vector2 pos;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
                    transform.position = myCanvas.transform.TransformPoint(pos);
                    foreach (CustomizableButtons customizableButtons in buttonList.customizableButtonsList)
                    {
                        if (customizableButtons.buttonName == gameObject.name)
                        {
                            customizableButtons.buttonPosX = rectTransform.localPosition.x;
                            customizableButtons.buttonPosY = rectTransform.localPosition.y;
                        }
                    }
                }
            }

        }
    }
}