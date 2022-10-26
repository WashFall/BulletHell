public abstract class Attacks
{
    public string name;

    public float baseAttackSpeed;
    public float baseAttackRange;
    public float baseDamage;
    public float baseProjectileSize;
    public float baseProjectileAmount;

    public float attackSpeed;
    public float attackRange;
    public float damage;
    public float projectileSize;
    public float projectileAmount;

    public float attackLevel = 0;

    public virtual void AttackLevelUp()
    {
        attackLevel++;
    }

    public virtual string GetUpgradeText(float level)
    {
        return "";
    }
}
