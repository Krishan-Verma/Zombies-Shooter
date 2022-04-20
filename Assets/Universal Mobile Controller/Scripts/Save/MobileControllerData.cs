using System.Collections.Generic;
using UnityEngine;

namespace UniversalMobileController
{
    [System.Serializable]
    public class MobileControllerData
    {
        public List<CustomizableButtons> customizableButtonsList = new List<CustomizableButtons>();

        public MobileControllerData(ButtonsData buttonsData)
        {
            for (int i = 0; i < buttonsData.customizableButtonsList.Count; i++)
            {
                customizableButtonsList.Add(buttonsData.customizableButtonsList[i]);
                Debug.Log(customizableButtonsList.Count + " " + buttonsData.customizableButtonsList[i].buttonName);
            }
        }
    }
}
