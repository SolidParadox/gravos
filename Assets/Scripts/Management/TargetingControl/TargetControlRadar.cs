using UnityEngine;

public class TargetControlRadar : TargetControlCore {
    public  Radar           radar;
    private Collider2D      delta;

    private void Start () {
        delta = null;
    }

    public override void ModifyLock ( bool alpha ) {
        base.ModifyLock ( alpha );
        delta = null;
    }

    void Update () {
        if ( delta == null || !radar.collectedColliders.Contains ( delta ) ) {
            if ( radar.collectedCount != 0 ) {
                delta = radar.collectedColliders [ 0 ];
                ModifyTarget ( delta.transform );
            }
        }
    }
}