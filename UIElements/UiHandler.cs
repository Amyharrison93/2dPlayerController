using UnityEngine;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour
{
    [field: SerializeField] public HealthHandler healthHandler {get; private set;}
    [field: SerializeField] public ManaHandler manaHandler {get; private set;}
    [field: SerializeField] public StaminaHandler staminaHandler {get; private set;}
    [field: SerializeField] public Slider healthSlider {get; private set;}
    [field: SerializeField] public Slider manaSlider {get; private set;}
    [field: SerializeField] public Slider staminaSlider {get; private set;}
    [field: SerializeField] public Text deathCounterText {get; private set;}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthSlider.maxValue = healthHandler.MaxHealth;
        manaSlider.maxValue = manaHandler.MaxMana;
        staminaSlider.maxValue = staminaHandler.MaxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        UpdateMana();
        UpdateStamina();
    }
    private void UpdateHealth()
    {
        healthSlider.value = healthHandler.CurrentHealth;
    }
    private void UpdateMana()
    {
        manaSlider.value = manaHandler.CurrentMana;
    }
    private void UpdateStamina()
    {
        staminaSlider.value = staminaHandler.CurrentStamina;
    }
    public void UpdateDeathCounter()
    {
        deathCounterText.text = "Deaths: " + "1";
    }
}
