using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace UniversalMobileController
{
    public static class SaveSystem
    {
        public static void SaveGame(ButtonsData buttonsData)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + SaveLocationSettings.savePath;
            FileStream stream = new FileStream(path, FileMode.Create);
            MobileControllerData data = new MobileControllerData(buttonsData);
            formatter.Serialize(stream, data);
            stream.Close();
            Debug.Log("Saved Data " + path);
        }

        public static MobileControllerData LoadData()
        {
            string path = Application.persistentDataPath + SaveLocationSettings.savePath;
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                MobileControllerData data = (MobileControllerData)formatter.Deserialize(stream);
                stream.Close();
                return data;
            }
            else
            {
                Debug.Log("Data could not be loaded because save file was not found in " + path);
                Debug.Log("Default values will be loaded. (This is not an error you just need to save in order to load values from the save file)");
                return null;
            }
        }
    }
}
