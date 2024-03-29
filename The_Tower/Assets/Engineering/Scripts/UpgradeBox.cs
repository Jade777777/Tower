using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBox : MonoBehaviour
{
    GameObject interactPrompt;
    public GameObject UpgradeChooser;
    public Dialogue dialogue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactPrompt.SetActive(true);
            print("prompt triggered");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<Player>().interact == true && !UpgradeChooser.activeSelf)
        {
            print("box active");
            UpgradeChooser.SetActive(true);
            UpgradeChooser.GetComponent<UpgradeChooser>().Open();
            UpgradeChooser.GetComponent<UpgradeChooser>().SetUpgradeBox(gameObject);
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactPrompt.SetActive(false);
        }
    }

    void Start()
    {
        interactPrompt = transform.GetChild(0).gameObject; 
        UpgradeChooser = GameObject.Find("UI").gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
