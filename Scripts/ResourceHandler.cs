using UnityEngine;

public class ResourceHandler : MonoBehaviour
{
    [field: SerializeField] public HealthHandler healthHandler;
    [field: SerializeField] public ManaHandler manaHandler;
    [field: SerializeField] public StaminaHandler staminaHandler;

    //spells restore stamina cost mana
    //actions restore mana cost stamina
    public void CastSpell(float spellCost)
    {
        manaHandler.CastSpell(spellCost);
        staminaHandler.RestoreStamina(spellCost);
    }
    public void PerformAction(float actionCost)
    {
        staminaHandler.PerformAction(actionCost);
        manaHandler.RestoreMana(actionCost);
    }
    public float GetStamina()
    {
        return staminaHandler.CurrentStamina;
    }
    public float GetMana()
    {
        return manaHandler.CurrentMana;
    }
    public void ResetResources()
    {
        manaHandler.RestoreMana(manaHandler.MaxMana);
        staminaHandler.RestoreStamina(staminaHandler.MaxStamina);
    }
}
