using System;
using UnityEngine;


    public class GameHandler : MonoBehaviour
    {
        private void Awake()
        {
            SaveObject saveObject = new SaveObject()
            {
                guidelines = "Hello World!"
            };
            string jsonToSave =  JsonUtility.ToJson(saveObject);

            SaveObject loadedSaveObject = JsonUtility.FromJson<SaveObject>(jsonToSave);

        }

        private class SaveObject
        {
            public string guidelines;
        }
        
    }

   
