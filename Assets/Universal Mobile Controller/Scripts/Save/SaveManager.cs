using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
namespace UniversalMobileController
{
    public class SaveManager : MonoBehaviour
    {
        public ButtonsData buttonsData;
        private void Start()
        {
            LoadData();
        }
        public void SaveData()
        {
            SaveSystem.SaveGame(buttonsData);
        }
        public void LoadData()
        {
            MobileControllerData data = SaveSystem.LoadData();
            if (data != null)
            {
                buttonsData.customizableButtonsList.Clear();
                foreach (CustomizableButtons customizableButtons in data.customizableButtonsList)
                {
                    buttonsData.customizableButtonsList.Add(customizableButtons);
                }
                Debug.Log("Data was loaded successfully");
            }
        }
    }
}
