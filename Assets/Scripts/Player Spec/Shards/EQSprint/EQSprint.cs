using UnityEngine;

public class EQSprint : EQBase {
    [Header ( "EQ Sprint" )]
    public  ParticleSystem  speedlines;
    public  TeflonMovement  movementCore;

    public  float           sprintSTR;
    private float           pastMax = -1;

    public override void MainInit ( ItemPort port ) {
        if ( port == null ) return;
        base.MainInit ( port );
        if ( enabled ) {
            movementCore = port.hullLink.GetComponent<TeflonPMove> ();
        }
    }

    public override void OnStartFire () {
        pastMax = movementCore.mxv;
        speedlines.Play ();
    }

    public override void OnStopFire () {
        if ( pastMax != -1 ) {
            movementCore.mxv = pastMax;
        }
        if ( speedlines != null ) {
            speedlines.Stop ();
        }
    }

    public override void Fire () {
        if ( movementCore.mxv != sprintSTR ) {
            pastMax = movementCore.mxv;
            movementCore.mxv = sprintSTR;
        }
        movementCore.SetV ( 1 );
        base.Fire ();
    }
}
