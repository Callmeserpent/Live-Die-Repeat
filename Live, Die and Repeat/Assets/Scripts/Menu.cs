using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    // Text fields
    public TextMeshProUGUI levelText, hitpointText, glimsText, upgradeCostText, expText;

    //Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform expBar;

    //Character Selection
    public void OnArrowClick(bool right)
    {
        if(right)
        {
            currentCharacterSelection++;

            //limit
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;
            
            OnSelectionChanged();
        }
        else
        {
            currentCharacterSelection--;

            //limit
            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
            
            OnSelectionChanged();
        }
    }

    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    // Weapon Upgrade
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
            UpdateMenu();
    }

    //Update character information
    public void UpdateMenu()
    {
        //Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
            upgradeCostText.text = "MAX";
        else
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();

        //Meta
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        glimsText.text = GameManager.instance.glims.ToString();

        //expBar
        int currentLevel = GameManager.instance.GetCurrentLevel();

        if (currentLevel == GameManager.instance.expTable.Count)
        {
            expText.text = GameManager.instance.experience.ToString() + "total experience points"; //display total exp
            expBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelExp = GameManager.instance.GetExpToLevel(currentLevel - 1);
            int currentLevelExp = GameManager.instance.GetExpToLevel(currentLevel);

            int diff = currentLevelExp - prevLevelExp;
            int currentExpIntoLevel = GameManager.instance.experience - prevLevelExp;

            float completionRatio = (float)currentExpIntoLevel / (float)diff;
            expBar.localScale = new Vector3(completionRatio, 1, 1);
            expText.text = currentExpIntoLevel.ToString() + " / " + diff;
        }
    }
}
