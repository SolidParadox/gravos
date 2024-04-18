using UnityEngine;

public class ProtoPlayerBridge : Zetha {
    public  NoticeMenu      notice;
    private TeflonPMove     movement;

    public  PowerCell       shieldCell;
    public  float           STRShield;

    public  float   stunTime;
    private float   deltaTime;

    public  bool    invulnerability;
    public  LabelNotification   ntf;

    public override void Start () {
        movement = GetComponent<TeflonPMove> ();
        base.Start ();
    }

    private void Update () {
        if ( deltaTime > 0 && currentHealth > 0 ) {
            deltaTime -= Time.deltaTime;
            if ( deltaTime <= 0 ) {
                deltaTime = 0;
                movement.SetLock (false);
            }
        }    
    }

    public override void DeltaIncoming ( int id, float delta ) {
        if ( CheckID ( id ) ) {
            
            delta *= invulnerability ? 0 : hitboxes [ id ].beta;

            if ( delta != 0 ) {
                ntf.AddMessage ( new DataLinkNTF ( "Hit for " + delta + " damage", 2 ) );
            }
            
            delta -= shieldCell.VariDrain ( delta * STRShield ) / STRShield;

            currentHealth -= delta;
            if ( stunTime != -1 ) {
                movement.SetLock ( true );
                deltaTime = stunTime;
            }

            if ( currentHealth <= 0.01f ) {
                Detach ( "GAME OVER" );
            }
        }
    }

    public  void    Detach  ( string messig ) {
        notice.SendNotice ( messig );
        movement.SetLock ( true );
        for ( int i = 1; i < movement.transform.childCount; i++ ) {
            movement.transform.GetChild ( i ).gameObject.SetActive ( false );
        }
    }

    public void Reattach () {

        movement.SetLock ( false );

        for ( int i = 1; i < movement.transform.childCount; i++ ) {
            movement.transform.GetChild ( i ).gameObject.SetActive ( true );
        }
    }
}
