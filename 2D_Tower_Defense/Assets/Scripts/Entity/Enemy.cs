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
    private EnemyAsset m_currentAsset;

    public override void UseAsset(EnemyAsset asset)
    {
        base.UseAsset(asset);
        if (!asset)
            return;
        m_currentAsset = asset;
        m_visual.color = asset.colorIsRandom ? asset.ColorsRandomArray[Random.Range(0, asset.ColorsRandomArray.Length)] : asset.Color;
        if (asset.spriteScaleIsRandom)
        {
            float size = Random.Range(asset.ScaleRandomRange.x, asset.ScaleRandomRange.y);
            m_visual.transform.localScale = new Vector2(size, size);
        }
        else
            m_visual.transform.localScale = asset.SpriteScale;
        m_animator.runtimeAnimatorController = asset.animationIsRandom ? asset.AnimationsRandomArray[Random.Range(0, asset.AnimationsRandomArray.Length)] : asset.Animation;
    }
    public void ApplyDamage(int damage, DamageType type)
    {
        int dmg = 0;
        dmg += type == DamageType.Physical ? Mathf.Max(0, damage - m_currentAsset.PhysArmor) : 0;
        dmg += type == DamageType.Piercing ? Mathf.Max(0, damage - m_currentAsset.PiercArmor) : 0;
        dmg += type == DamageType.Magical ? Mathf.Max(0, damage - m_currentAsset.MagicArmor) : 0;
        ApplyDamage(dmg);
    }
    public void DamagePlayer()
    {
        Player.Instance.TakeDamage(m_currentAsset.Gold);
        SoundController.Instance.Play(m_currentAsset.SoundWin);
    }
    public void GivePlayerGold()
    {
        Player.Instance.ChangeGold(m_currentAsset.Gold);
        SoundController.Instance.Play(m_currentAsset.SoundDie);
    }
}
