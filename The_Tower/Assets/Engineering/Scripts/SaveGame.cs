using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    PersistantGameManager pData;
    private void Start()
    {
        pData = FindObjectOfType<PersistantGameManager>();
    }
    public void Save()
    {
        pData.SaveGame();
    }

}
