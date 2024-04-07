using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public CardSkill cardSkill;
    public TMP_Text skillname;
    public TMP_Text descriptionText;
    public Image skillImage;
    private void Start()
    {
        skillname.text = cardSkill.nameOfSkills;
        descriptionText.text = cardSkill.describeSkills;
        skillImage.sprite = cardSkill.skillPictures;
    }

}
