using UnityEngine;

public class Tower : MonoBehaviour
{
    private class TurretTarget
    {
        public Transform targetTransform;
        public Rigidbody2D targetRigit;
    }
    [SerializeField] private TowerAsset towerAsset;
    [SerializeField] private UpgradeAsset upgrade;
    private Turret[] turrets;
    private TurretTarget turretTarget = new TurretTarget();

    private float radius => towerAsset.TowerRadius * (upgrade != null && upgrade.Type == UpgradeAsset.UpgradeType.Tower ? upgrade.FireRadiusModifier : 1);

    private void Start()
    {
        TowerIni();
    }

    private void Update()
    {
        ActionFire();
    }
    private void ActionFire()
    {
        Vector3 Predction = GetPrediction();
        if (Predction != Vector3.zero)
            foreach (var turret in turrets)
            {
                turret.transform.up = Predction;
                turret.Fire(turretTarget.targetTransform);
            }
        else
            turretTarget = FindNewTarget(radius);
    }
    private Vector3 GetPrediction()
    {
        if (turretTarget.targetTransform)
            return (turretTarget.targetTransform.position - transform.position).magnitude <= radius ? turretTarget.targetTransform.position - transform.position : Vector3.zero;
        else
            return Vector3.zero;
    }
    private TurretTarget FindNewTarget(float v_radius)
    {
        float minDist = radius;
        TurretTarget turretTarget = new TurretTarget();
        if (Destructible.AllDestructibles != null)
            foreach (var v in Destructible.AllDestructibles)
            {
                float dist = (v.transform.position - transform.position).magnitude;
                if (dist < radius && dist < minDist)
                {
                    minDist = dist;

                    turretTarget.targetTransform = v.transform;
                    turretTarget.targetRigit = v.GetComponent<Rigidbody2D>();
                }
                else
                {
                    continue;
                }
            }
        return turretTarget;
    }
    private void TowerIni()
    {
        if (towerAsset == null)
            return;
        if (upgrade == null || upgrade.Type != UpgradeAsset.UpgradeType.Tower || upgrade.TowerUpgradeTarget != towerAsset.Type)
        {
            foreach (var item in ItemController.Instance.Items)
            {
                UpgradeAsset asset = item as UpgradeAsset;
                if (asset != null && asset.TowerUpgradeTarget == towerAsset.Type)
                    upgrade = asset;
            }
        }
        turrets ??= GetComponentsInChildren<Turret>();
        foreach (var turret in turrets)
        {
            turret.AssignLoadut(towerAsset.TuerretProperties);
            turret.AssignUpgrade(upgrade);
        }

    }
    public void AssignTowerAsset(TowerAsset asset)
    {
        if (asset == null)
            return;
        towerAsset = asset;
    }
    public void AssignUpgradeAsset(UpgradeAsset asset)
    {
        if (asset == null || asset.Type != UpgradeAsset.UpgradeType.Tower || asset.TowerUpgradeTarget != towerAsset.Type)
            return;
        upgrade = asset;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
