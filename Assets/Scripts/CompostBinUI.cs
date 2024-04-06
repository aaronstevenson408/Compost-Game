using UnityEngine;
using UnityEngine.UI;

public class CompostBinUI : MonoBehaviour
{
    public Image compostBinImage;
    public Sprite[] compostQualitySprites;

    public void UpdateCompostQuality(float compostQuality)
    {
        int spriteIndex = Mathf.RoundToInt(compostQuality * (compostQualitySprites.Length - 1));
        compostBinImage.sprite = compostQualitySprites[spriteIndex];
    }
}