using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerupIndex
{
    ORANGEMUSHROOM = 0,
    REDMUSHROOM = 1
}

public class PowerupManagerEV : MonoBehaviour
{
    // reference of all player stats affected
    public IntVariable marioJumpSpeed;
    public IntVariable marioMaxSpeed;
    public PowerupInventory powerupInventory;
    public List<GameObject> powerupIcons;

    // Start is called before the first frame update
    void Start()
    {
        resetPowerup();
        if (!powerupInventory.gameStarted) {
            powerupInventory.gameStarted = true;
            powerupInventory.Setup(powerupIcons.Count);
        }
        else {
            // re-render the contents of the powerup from the previous time
            for (int i = 0; i < powerupInventory.Items.Count; i++) {
                Powerup p = powerupInventory.Get(i);
                if (p != null) {
                    AddPowerupUI(i, p.powerupTexture);
                }
            }
        }
    }

    public void resetPowerup()
    {
        for (int i = 0; i < powerupIcons.Count; i++) {
            powerupIcons[i].SetActive(false);
        }
    }

    void AddPowerupUI(int index, Texture t)
    {
        powerupIcons[index].GetComponent<RawImage>().texture = t;
        powerupIcons[index].SetActive(true);
    }

    public void AddPowerup(Powerup p)
    {
        powerupInventory.Add(p, (int) p.index);
        AddPowerupUI((int) p.index, p.powerupTexture);
    }

    public void ResetValues()
    {
        powerupInventory.Clear();
    }

    public void OnApplicationQuit()
    {
        ResetValues();
    }

    void cast(int index)
    {
        Powerup p = powerupInventory.Get(index);
        if (p != null) {
            powerupIcons[index].SetActive(false);
            marioMaxSpeed.ApplyChange(p.absoluteSpeedBooster);
            marioJumpSpeed.ApplyChange(p.absoluterJumpBooster);
            StartCoroutine(removeEffect(index, p));
        }
    }

    IEnumerator removeEffect(int index, Powerup p)
    {
        yield return new WaitForSeconds(p.duration);
        marioMaxSpeed.ApplyChange(-p.absoluteSpeedBooster);
        marioJumpSpeed.ApplyChange(-p.absoluterJumpBooster);

        powerupInventory.Remove(index);
        
    }

    public void consumePowerup(KeyCode k)
    {
        switch(k) {
            case KeyCode.Z:
                cast(0);
                break;
            case KeyCode.X:
                cast(1);
                break;
            default:
                break;
        }
    }
}
