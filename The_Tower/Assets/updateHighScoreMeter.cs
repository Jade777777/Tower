using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class updateHighScoreMeter : MonoBehaviour
{
    public bool fullScore = false, currentScore = false;
    void Update()
    {
        if (fullScore) GetComponent<Text>().text = "Highest Floor Reached: " + PersistantGameManager.Instance.highestLevel + ("\n") + "Floor Reached: " + PersistantGameManager.Instance.currentLevel;
        else if (currentScore) GetComponent<Text>().text = "This Floor: " + PersistantGameManager.Instance.currentLevel;
        else GetComponent<Text>().text = "Highest Floor Reached: " + PersistantGameManager.Instance.highestLevel;
    }
}
