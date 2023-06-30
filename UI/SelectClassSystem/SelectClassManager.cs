using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SelectClassManager : MonoBehaviour
{
    [SerializeField] private TMP_Text playText;
    [SerializeField] private Button playButton;
    [SerializeField] private Image playButtonImage;
    [SerializeField] private UIClassSetting[] classSetting;
    private void AcitePlayButton()
    {
        playButton.interactable = true;
        playButtonImage.color = new Color32(176, 149, 125, 255);
        playText.alpha = 255;
    }

    public void ChooseHero(int index)
    {
        classSetting[index].TurnOnClassSetting();
        AcitePlayButton();
        for (int i = 0; i < classSetting.Length; i++)
        {
            if(i != index)
            {
                classSetting[i].GetExplainPanel().SetActive(false);
                classSetting[i].GetOutlineObjcet().SetActive(false);
                classSetting[i].GetButtonExplain().SetActive(false);
            }
        }
    }

    public void OpenPanel(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void ClosePanel(GameObject obj)
    {
        obj.SetActive(false);
    }
}
