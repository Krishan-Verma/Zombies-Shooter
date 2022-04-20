using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UniversalMobileController{
    public class UniversalMobileController_Manager : MonoBehaviour
    {
        public static bool editMode = false;
        public GameObject resizeSlider;
        public Text editButtonText;
        public void PressEditMode()
        {
            if (editMode)
            {
                editButtonText.text = "Enter Layout Edit Mode";
                editMode = false;
                resizeSlider.SetActive(false);
            }
            else
            {
                resizeSlider.SetActive(true);
                editButtonText.text = "Exit Layout Edit Mode";
                editMode = true;
            }
        }
    }
}
