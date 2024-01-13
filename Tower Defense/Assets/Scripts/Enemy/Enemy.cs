using UnityEngine;

public enum DamageType
{
    Physical,
    Piercing,
    Magical
}

[RequireComponent(typeof(TDPatrolController))]
public class Enemy : SpaceShip
{
    [Header("")]
    [SerializeField] private SpriteRenderer m_visual;
    [SerializeField] private Animator m_animator;
    [SerializeField] private int m_physArmor;
    [SerializeField] private int m_piercArmor;
    [SerializeField] private int m_magicArmor;
    [SerializeField] private int m_damage = 1;
    [SerializeField] private int m_gold = 1;
    public override void UseAsset(EnemyAsset asset)
    {
        if (asset.collorIsRandom)
            m_visual.color = asset.colorsRandomArray[Random.Range(0, asset.colorsRandomArray.Length)];
        else
            m_visual.color = asset.color;

        if (asset.scaleIsRandom)
        {
            float size = Random.Range(asset.scaleRandomRange.x, asset.scaleRandomRange.y);
            m_visual.transform.localScale = new Vector2(size, size);
        }
        else
            m_visual.transform.localScale = asset.spriteScale;

        if (asset.animationIsRandom)
            m_animator.runtimeAnimatorController = asset.animationsRandomArray[Random.Range(0, asset.animationsRandomArray.Length)];
        else
            m_animator.runtimeAnimatorController = asset.animation;

        m_physArmor = asset.physArmor;
        m_piercArmor = asset.piercArmor;
        m_magicArmor = asset.magicArmor;
        m_damage = asset.damage;
        m_gold = asset.gold;

        base.UseAsset(asset);
    }
    public void ApplyDamage(int damage, DamageType type)
    {
        int dmg = 0;
        dmg += type == DamageType.Physical ? Mathf.Max(1, damage - m_physArmor) : 0;
        dmg += type == DamageType.Piercing ? Mathf.Max(1, damage - m_piercArmor) : 0;
        dmg += type == DamageType.Magical ? Mathf.Max(1, damage - m_magicArmor) : 0;
        ApplyDamage(dmg);
    }
    public void DamagePlayer()
    {
        Player.Instance.TakeDamage(m_damage);
    }
    public void GivePlayerGold()
    {
        Player.Instance.ChangeGold(m_gold);
    }
}
