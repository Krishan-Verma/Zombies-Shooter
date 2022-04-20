using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UniversalMobileController
{ 
[CreateAssetMenu(fileName = "Data", menuName = "Data/ButtonsData", order = 1)]
public class ButtonsData : ScriptableObject
{
    public List<CustomizableButtons> customizableButtonsList = new List<CustomizableButtons>();
}

    [System.Serializable]
    public class CustomizableButtons
    {
        public string buttonName;
        public float buttonPosX;
        public float buttonPosY;
        [Range(0.2f, 2)]
        public float buttonWidth;
        [Range(0.2f, 2)]
        public float buttonHeight;

        public CustomizableButtons(string name, float posX, float posY, float width, float height)
        {
            this.buttonName = name;
            this.buttonPosX = posX;
            this.buttonPosY = posY;
            this.buttonWidth = width;
            this.buttonHeight = height;
        }
    }
}
