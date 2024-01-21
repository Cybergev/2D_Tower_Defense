using UnityEngine;
using UnityEngine.UI;

public class Tower : BuildSite
{
    private class TurretTarget
    {
        public Transform targetTransform;
        public Rigidbody2D targetRigit;
    }
    [SerializeField] private TowerAsset currentTowerAsset;
    [SerializeField] private UpgradeAsset currentUpgradeAsset;
    [SerializeField] private Turret[] turrets;
    [SerializeField] private Image image;
    public TowerAsset CurrentTowerAsset => currentTowerAsset;
    public UpgradeAsset CurrentUpgradeAsset => currentUpgradeAsset;
    public Turret[] Turrets => turrets;
    public Image Image => image;


    private TurretTarget turretTarget = new TurretTarget();

    private float radius => currentTowerAsset.TowerRadius * (currentUpgradeAsset != null && currentUpgradeAsset.Type == UpgradeAsset.UpgradeType.Tower ? currentUpgradeAsset.FireRadiusModifier : 1);

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
        if (currentTowerAsset == null)
            return;
        foreach (var turret in turrets)
        {
            turret.AssignLoadut(currentTowerAsset.TuerretProperties);
            turret.AssignUpgrade(currentUpgradeAsset);
        }
        image.sprite = image ? currentTowerAsset.TowerSprite : null;
    }
    public void AssignTowerAsset(TowerAsset asset)
    {
        if (asset == null)
            return;
        currentTowerAsset = asset;
        TowerIni();
    }
    public void AssignUpgradeAsset(UpgradeAsset asset)
    {
        if (asset == null || asset.Type != UpgradeAsset.UpgradeType.Tower || asset.TowerUpgradeTarget != currentTowerAsset.Type)
            return;
        currentUpgradeAsset = asset;
        TowerIni();
    }
    public void AssignAssets(TowerAsset towerAsset, UpgradeAsset upgradeAsset)
    {
        if (!towerAsset || !upgradeAsset)
            return;
        AssignTowerAsset(towerAsset);
        AssignUpgradeAsset(upgradeAsset);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
