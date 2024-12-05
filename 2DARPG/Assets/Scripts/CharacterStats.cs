using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Major stats")]
    public Stat strength;
    public Stat agility;
    public Stat intelligence;
    public Stat vitality;

    [Header("Offensive stats")]
    public Stat damage;
    public Stat critChance;
    public Stat critPower;

    [Header("Defensive stats")]
    public Stat maxHealth;
    public Stat armor;
    public Stat evasion;



    [SerializeField] private int currentHealth;

    protected virtual void Start()
    {
        critPower.SetDefaultValue(150);
        currentHealth = maxHealth.GetValue();
    }

    public virtual void DoDamage(CharacterStats _targetStats)
    {
        if (TargetCanAvoidAttack(_targetStats))
            return;

        int totalDamage = damage.GetValue() + strength.GetValue();
        Debug.Log("≥ı º…À∫¶" + totalDamage);

        if (CanCrit())
        {
            totalDamage = CalcalateCriticalDamage(totalDamage);
            Debug.Log("◊Ó÷’…À∫¶" + totalDamage);
        }

        totalDamage -= _targetStats.armor.GetValue();
        totalDamage = CheckTargetArmor(_targetStats, totalDamage);
    }


    public virtual void TakeDamage(int _damage)
    {
        currentHealth -= _damage;

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

    private bool CanCrit()
    {
        int totalCriticalChance = critChance.GetValue() + agility.GetValue();

        if(Random.Range(0, 100) <= totalCriticalChance)
        {
            return true;
        }

        return false;
    }

    private int CalcalateCriticalDamage(int _damage)
    {
        float totalCritPower = (critPower.GetValue() + strength.GetValue()) * .01f;
        Debug.Log("±©ª˜…À∫¶±∂ ˝" + totalCritPower);

        float critDamage = _damage * totalCritPower;
        Debug.Log("◊‹…À∫¶" + critDamage);

        return Mathf.RoundToInt(critDamage);

    }
}
