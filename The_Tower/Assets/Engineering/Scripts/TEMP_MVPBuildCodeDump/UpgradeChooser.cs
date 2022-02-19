using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeChooser : MonoBehaviour
{
    GameObject upgrade1Button, upgrade2Button, upgrade3Button, player;
    Upgrade upgrade;
    public Text upgrade1Text, upgrade2Text, upgrade3Text;

    // Start is called before the first frame update
    void Start()
    {
        upgrade1Button = transform.GetChild(1).gameObject;
        upgrade2Button = transform.GetChild(2).gameObject;
        upgrade3Button = transform.GetChild(3).gameObject;
        player = GameObject.Find("Player");
    }

    public void Open()
    {
        upgrade = new Upgrade();
        upgrade1Text.text = "Upgrade 1: Acceleration increases by " + upgrade.Upgrade1;
        upgrade2Text.text = "Upgrade 2: Speed increases by " + upgrade.Upgrade2;
        upgrade3Text.text = "Upgrade 3: Jump increases by " + upgrade.Upgrade3;
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void choseUpgrade1()
    {
        player.GetComponent<PlayerMovement>().spd += upgrade.Upgrade1;
        Close();
    }

    public void choseUpgrade2()
    {
        player.GetComponent<PlayerMovement>().maxspd += upgrade.Upgrade2;
        Close();
    }

    public void choseUpgrade3()
    {
        player.GetComponent<PlayerMovement>().jumpPower += upgrade.Upgrade3;
        Close();
    }
}
