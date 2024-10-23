using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image heartPrefab;
    public Sprite FullHeart;
    public Sprite EmptyHeart;

    private List<Image> hearts = new List<Image>();
    // Start is called before the first frame update
    public void SetMaxHealth(int maxHealth)
    {
        foreach (Image heart in hearts)
        {
            Destroy(heart.gameObject);
        }
        hearts.Clear();

        for(int i = 0; i < maxHealth; i++)
        {
            Image newHeart=Instantiate(heartPrefab,transform);
            newHeart.sprite=FullHeart;
            hearts.Add(newHeart);
        }
    }

    public void UpdateHealth(int currentHealth)
    {
        for (int i = 0; i < hearts.Count; i++) 
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = FullHeart;
            }
            else
            {
                hearts[i].sprite=EmptyHeart;
            }
            
        }
    }
}
