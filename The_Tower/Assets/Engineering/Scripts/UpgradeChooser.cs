using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeChooser : MonoBehaviour
{
    GameObject upgrade1Button, upgrade2Button, upgrade3Button, player, upgradeBox;
    Upgrade upgrade;
    public Text upgrade1Text, upgrade2Text, upgrade3Text;

    // Start is called before the first frame update
    void Start()
    {
        upgrade1Button = transform.GetChild(0).gameObject;
        upgrade2Button = transform.GetChild(1).gameObject;
        upgrade3Button = transform.GetChild(2).gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Open()
    {
        upgrade = new Upgrade();
        upgrade1Text.text = DisplayText(upgrade.Upgrade1Type, upgrade.Upgrade1);
        upgrade2Text.text = DisplayText(upgrade.Upgrade2Type, upgrade.Upgrade2);
        upgrade3Text.text = DisplayText(upgrade.Upgrade3Type, upgrade.Upgrade3);
    }
    public void Close()
    {
        gameObject.SetActive(false);
        Destroy(upgradeBox);
    }

    public void SetUpgradeBox(GameObject box) {
        upgradeBox = box;
    }

    public void choseUpgrade1()
    {
        ApplyUpgrade(upgrade.Upgrade1Type, upgrade.Upgrade1);
        Close();
    }

    public void choseUpgrade2()
    {
        ApplyUpgrade(upgrade.Upgrade2Type, upgrade.Upgrade2);
        Close();
    }

    public void choseUpgrade3()
    {
        ApplyUpgrade(upgrade.Upgrade3Type, upgrade.Upgrade3);
        Close();
    }

    private string DisplayText(int type, float value) {
        switch(type) {
            case 0:
            return "Restore HP";
            case 1:
            return "Restore HP and increase max HP by 1.";
            case 2:
            return "Upgrade 3: Jump increases by " + value;
            case 3:
            return "Upgrade 2: Speed increases by " + value;
            case 4:
            return "Upgrade 1: Acceleration increases by " + value;
            default:
            return null;
        }
    }

    private void ApplyUpgrade(int type, float value){
        switch(type) {
            case 0:
            player.GetComponent<Player>().HP = player.GetComponent<Player>().maxHP;
            break;
            case 1:
            player.GetComponent<Player>().maxHP += 1;
            player.GetComponent<Player>().HP = player.GetComponent<Player>().maxHP;
            break;
            case 2:
            player.GetComponent<Player>().jumpPower += value;
            break;
            case 3:
            player.GetComponent<Player>().maxspd += value;
            break;
            case 4:
            player.GetComponent<Player>().spd += value;
            break;
            default:
            return;
        }
    }
}
