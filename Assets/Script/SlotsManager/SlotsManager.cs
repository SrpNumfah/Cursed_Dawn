using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotsManager : MonoBehaviour
{
    public GameObject[] SlotSkillPbject;
    public Button[] slots;

    public Sprite[] SkillSprites;

    [System.Serializable]
    public class DisplayItemSlots
    {
        public List<Image> slotSprites = new List<Image>();
    }

    public DisplayItemSlots[] DisplayItemSlot;

    public Image DisplayResultImage;

    public List<int> StartList = new List<int>();
    public List<int> ResultIndexList = new List<int>();

    int ItemCount = 3;

    private void Start()
    {
        for (int i = 0; i < ItemCount * slots.Length; i++)
        {
            StartList.Add(i);
        }

        for (int i = 0; i < slots.Length ; i++)
        {
            for (int j = 0; j < ItemCount; j++)
            {
                slots[i].interactable = false;

                int randomIndex = Random.Range(0, StartList.Count);
                if (i == 0 && j == 1 || i == 1 && j == 0 || i == 2 && j == 2)
                {
                    ResultIndexList.Add(StartList[randomIndex]);
                }
                DisplayItemSlot[i].slotSprites[j].sprite = SkillSprites[StartList[randomIndex]];

                if (j == 0)
                {
                    DisplayItemSlot[i].slotSprites[ItemCount].sprite = SkillSprites[StartList[randomIndex]];
                }
                StartList.RemoveAt(randomIndex);
            }

            

        }
        

        for (int i = 0; i < slots.Length; i++)
        {
            StartCoroutine(StartSlot(i));
        }

    }

    int[] answer = { 2, 3, 1 };
    IEnumerator StartSlot(int slotIndex)
    {

        for (int i = 0; i < (ItemCount * (6 + slotIndex * 4) + answer[slotIndex]) * 2 ; i++)
        {
            SlotSkillPbject[slotIndex].transform.localPosition -= new Vector3(0, 50f, 0);
            if (SlotSkillPbject[slotIndex].transform.localPosition.y < 50f)
            {
                SlotSkillPbject[slotIndex].transform.localPosition += new Vector3(0, 300f, 0);
            }
            yield return new WaitForSeconds(0.02f);
        }

        for (int i = 0; i < ItemCount; i++)
        {
            slots[i].interactable = true;
        }
    }
    public void ClickBtn(int index)
    {
        DisplayResultImage.sprite = SkillSprites[ResultIndexList[index]];
        Debug.Log(DisplayResultImage);
    }

  
}
