using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
   public Slider slider;

   public Gradient gradient;
   public Image bar;

[Header("ScriptableObjects")]
    public PlayerData playerData;
   
    private void Start()
    {
        Fill(playerData.currentHealth / playerData.maxHealth);
    }

public void Fill(float amountNormalized)
    {
        bar.fillAmount = amountNormalized;
        // Change health bar's color according to the gradient
        bar.color = gradient.Evaluate(amountNormalized);
    }
}
