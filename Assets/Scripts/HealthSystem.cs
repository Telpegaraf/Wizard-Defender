using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private TextMeshProUGUI life;
    [SerializeField] private Image fullHealth;
    [SerializeField] private Image currentHealth;
    [SerializeField] private Health playerHealth;

    private void Awake()
    {
        fullHealth.fillAmount = playerHealth.CurrentHealth/30;
        life.text = playerHealth.CurrentHealth.ToString();
    }

    private void Update()
    {
        currentHealth.fillAmount = playerHealth.CurrentHealth/30;
        life.text = playerHealth.CurrentHealth.ToString();
    }


}
