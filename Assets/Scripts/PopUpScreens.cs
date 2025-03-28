using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PopUpScreens : MonoBehaviour
{
    [Header("Cauldron Objects")]
    public GameObject cauldronUpgrade;
    private TextMeshProUGUI sporeOneText;
    private TextMeshProUGUI sporeTwoText;
    private TextMeshProUGUI sporeThreeText;
    private Image button2;
    private Image button3;
    private Image button4;
    public Sprite setImage2;
    public Sprite setImage3;
    public Sprite setImage4;


    [Header("Witch Objects")]
    public GameObject witchUpgrade;
    private TextMeshProUGUI sporeOneTextWitch;
    private TextMeshProUGUI sporeTwoTextWitch;
    private TextMeshProUGUI sporeThreeTextWitch;

    [Header("Text References")]
    public GameObject textPopUp;
    private TextMeshProUGUI textObject;

    [Header("Spore References")]
    public GameObject sporeCounter;
    private TextMeshProUGUI sporeOneCounter;
    private TextMeshProUGUI sporeTwoCounter;
    private TextMeshProUGUI sporeThreeCounter;
    private TextMeshProUGUI sporeFourCounter;

    private GameplayManager gameplayManager;

    private Slider spellSlider;
    private GameObject faultyTree;

    void Start()
    {
        sporeOneText = GameObject.Find("CauldronUpgrade/Currency/Currency1/Count").GetComponent<TextMeshProUGUI>();
        sporeTwoText = GameObject.Find("CauldronUpgrade/Currency/Currency2/Count").GetComponent<TextMeshProUGUI>();
        sporeThreeText = GameObject.Find("CauldronUpgrade/Currency/Currency3/Count").GetComponent<TextMeshProUGUI>();
        sporeOneTextWitch = GameObject.Find("Upgrades/Currency/Currency1/Count").GetComponent<TextMeshProUGUI>();
        sporeTwoTextWitch = GameObject.Find("Upgrades/Currency/Currency2/Count").GetComponent<TextMeshProUGUI>();
        sporeThreeTextWitch = GameObject.Find("Upgrades/Currency/Currency3/Count").GetComponent<TextMeshProUGUI>();
        sporeOneCounter = GameObject.Find("SporeCounter/Spore1").GetComponent<TextMeshProUGUI>();
        sporeTwoCounter = GameObject.Find("SporeCounter/Spore2").GetComponent<TextMeshProUGUI>();
        button2 = GameObject.Find("CauldronUpgrade/UpgradeView/Centerer/PathHeight/SkillTree1 (1)/Skill1").GetComponent<Image>();
        button3 = GameObject.Find("CauldronUpgrade/UpgradeView/Centerer/PathHeight/SkillTree1 (1)/Skill2").GetComponent<Image>();
        button4 = GameObject.Find("CauldronUpgrade/UpgradeView/Centerer/PathHeight/SkillTree1 (1)/Skill3").GetComponent<Image>();
        sporeThreeCounter = GameObject.Find("SporeCounter/Spore3").GetComponent<TextMeshProUGUI>();
        sporeFourCounter = GameObject.Find("SporeCounter/Spore4").GetComponent<TextMeshProUGUI>();
        textObject = GameObject.Find("PopUpInstruction/PopUp").GetComponent<TextMeshProUGUI>();
        gameplayManager = GameObject.FindWithTag("GameplayManager").GetComponent<GameplayManager>();
        spellSlider = GameObject.Find("SporeCounter/SpellSlider").GetComponent<Slider>();
        faultyTree = GameObject.Find("PopUpScreens/Upgrades/UpgradeView/Centerer/PathHeight/SkillTree1 (1)");
    }

    void Update(){
        if(spellSlider.transform.GetChild(0).GetComponent<Image>().enabled){
            if(spellSlider.value < spellSlider.maxValue){
                spellSlider.value += 1 * Time.deltaTime;
                if(spellSlider.value > 20){
                    Image image = spellSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>();
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 0.843f);
                }
            }
        }
    }

    public void SpawnSpell(){
        if(spellSlider.value == 40){
            spellSlider.value = 0;
        }
        else{
            spellSlider.value -= 20;
        }
        Graphic image = spellSlider.transform.GetChild(1).GetChild(0).GetComponent<Graphic>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
    }

    public float SpellSliderValue(){
        return spellSlider.value;
    }

    public void LongRangeSpell(GameObject button)
    {
        if (gameplayManager.GetSpore1() >= 3000)
        {
            button.GetComponent<Image>().color = new Color32(0, 255, 249, 255);
            gameplayManager.SetSpore1(gameplayManager.GetSpore1() - 3000);
            UpdateSporeCounter();
            sporeOneTextWitch.text = gameplayManager.GetSpore1().ToString();
            spellSlider.transform.GetChild(0).GetComponent<Image>().enabled = true;
        }
    }

    public void SpellPlus(GameObject button)
    {
        if (gameplayManager.GetSpore3() >= 3000)
        {
            button.GetComponent<Image>().color = new Color32(0, 255, 249, 255);
            gameplayManager.SetSpore3(gameplayManager.GetSpore3() - 3000);
            UpdateSporeCounter();
            sporeThreeTextWitch.text = gameplayManager.GetSpore3().ToString();
            spellSlider.maxValue = 40f;
        }
    }

    public void BroomPlus(GameObject button) //Add var to make scurrying longer, figure out who changes it
    {
        if (gameplayManager.GetSpore2() >= 2500)
        {
            button.GetComponent<Image>().color = new Color32(0, 255, 249, 255);
            gameplayManager.SetSpore2(gameplayManager.GetSpore2() - 2500);
            UpdateSporeCounter();
            sporeTwoTextWitch.text = gameplayManager.GetSpore2().ToString();
            gameplayManager.UpgradeBroom();
        }
    }

    public void TreeKnockdown(GameObject button) //Add var to multiply to tree time
    {
        if (gameplayManager.GetSpore1() >= 6000)
        {
            button.GetComponent<Image>().color = new Color32(0, 255, 249, 255);
            gameplayManager.SetSpore1(gameplayManager.GetSpore1() - 6000);
            UpdateSporeCounter();
            sporeOneTextWitch.text = gameplayManager.GetSpore1().ToString();
            gameplayManager.UpgradeKnockDown();
        }
    }

    public void StrongerKnockdown(GameObject button) //^ but change the var
    {
        if (gameplayManager.GetSpore3() >= 6000)
        {
            button.GetComponent<Image>().color = new Color32(0, 255, 249, 255);
            gameplayManager.SetSpore3(gameplayManager.GetSpore3() - 6000);
            UpdateSporeCounter();
            sporeThreeTextWitch.text = gameplayManager.GetSpore1().ToString();
            gameplayManager.UpgradeKnockDown();
        }
    }

    public void Level2Check()
    {
        if (gameplayManager.GetSpore1() >= 50)
        {
            button2.sprite = setImage2;
            gameplayManager.SetSpore1(gameplayManager.GetSpore1() - 50);
            gameplayManager.SetSpore2(200);
            sporeOneText.text = gameplayManager.GetSpore1().ToString();
            gameplayManager.IncreaseLevel();
            UpdateSporeCounter();
            GetComponent<CircularMenu>().buttons[1].enabled = true;
        }
    }

    public void Level3Check()
    {
        if (gameplayManager.GetSpore2() >= 50)
        {
            button3.sprite = setImage3;
            gameplayManager.SetSpore2(gameplayManager.GetSpore2() - 50);
            gameplayManager.SetSpore3(200);
            sporeTwoText.text = gameplayManager.GetSpore2().ToString();
            gameplayManager.IncreaseLevel();
            UpdateSporeCounter();
            GetComponent<CircularMenu>().buttons[2].enabled = true;
        }
    }

    public void Level4Check()
    {
        if (gameplayManager.GetSpore3() >= 50)
        {
            Debug.Log("Inky Cap Check done, meaning it technically could be used");
            button4.sprite = setImage4;
            gameplayManager.SetSpore3(gameplayManager.GetSpore2() - 50);
            gameplayManager.SetSpore4(200);
            sporeTwoText.text = gameplayManager.GetSpore3().ToString();
            gameplayManager.IncreaseLevel();
            UpdateSporeCounter();
            GetComponent<CircularMenu>().buttons[3].enabled = true;
        }
    }

    public void UpdateSporeCounter()
    {
        sporeOneCounter.text = "Spore 1: " + gameplayManager.GetSpore1().ToString();
        sporeTwoCounter.text = "Spore 2: " + gameplayManager.GetSpore2().ToString();
        sporeThreeCounter.text = "Spore 3: " + gameplayManager.GetSpore3().ToString();
        sporeFourCounter.text = "Spore 4: " + gameplayManager.GetSpore4().ToString();
    }

    public void EnableCauldronUpgrade()
    {
        cauldronUpgrade.GetComponent<Canvas>().enabled = true;
        sporeOneText.text = gameplayManager.GetSpore1().ToString();
        sporeTwoText.text = gameplayManager.GetSpore2().ToString();
        sporeThreeText.text = gameplayManager.GetSpore3().ToString();
    }

    public void DisableCauldronUpgrade()
    {
        cauldronUpgrade.GetComponent<Canvas>().enabled = false;
    }

    public void EnableWitchUpgrade()
    {
        witchUpgrade.GetComponent<Canvas>().enabled = true;
        faultyTree.SetActive(true);
        sporeOneTextWitch.text = gameplayManager.GetSpore1().ToString();
        sporeTwoTextWitch.text = gameplayManager.GetSpore2().ToString();
        sporeThreeTextWitch.text = gameplayManager.GetSpore3().ToString();
    }

    public void DisableWitchUpgrade()
    {
        witchUpgrade.GetComponent<Canvas>().enabled = false;
        faultyTree.SetActive(false);
    }

    public void EnableTextPopUpUpgrade(string popUp)
    {
        textPopUp.GetComponent<Canvas>().enabled = true;
        textObject.text = popUp;
    }

    public void DisableTextPopUpUpgrade()
    {
        textPopUp.GetComponent<Canvas>().enabled = false;
        textObject.text = "";
    }

    public void EnableSporeCounterUpgrade()
    {
        sporeCounter.GetComponent<Canvas>().enabled = true;
    }

    public void DisableSporeCounterUpUpgrade()
    {
        sporeCounter.GetComponent<Canvas>().enabled = false;
    }
}
