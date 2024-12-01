using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Major stats")]
    public Stat strength;
    public Stat agility;
    public Stat intelligence;
    public Stat vitality;

    [Header("Defensive stats")]
    public Stat maxHealth;
    public Stat armor;
    public Stat evasion;


    public Stat damage;

    [SerializeField] private int currentHealth;

    protected virtual void Start()
    {
        currentHealth = maxHealth.GetValue();
    }

    public virtual void DoDamage(CharacterStats _targetStats)
    {
        if (TargetCanAvoidAttack(_targetStats))
            return;

        int totalDamage = damage.GetValue() + strength.GetValue();

        totalDamage -= _targetStats.armor.GetValue();
        totalDamage = CheckTargetArmor(_targetStats, totalDamage);
    }


    public virtual void TakeDamage(int _damage)
    {
        currentHealth -= _damage;

        Debug.Log(_damage);

        if (currentHealth <= 0)
            Die();
    }

    protected virtual void Die()
    {
        //throw new NotImplementedException();
    }
    private bool TargetCanAvoidAttack(CharacterStats _targetStats)
    {
        int totalEavasion = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();

        if (Random.Range(0, 100) < totalEavasion)
        {
            return true;
        }

        return false;
    }
    private int CheckTargetArmor(CharacterStats _targetStats, int totalDamage)
    {
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        _targetStats.TakeDamage(totalDamage);
        return totalDamage;
    }
}
