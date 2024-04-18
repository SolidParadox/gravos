using UnityEngine;

public class ItemPort : ZephyrUnit {
    [Header("Item Port Base")]
    public  ItemPolarity    polarity;

    public  bool            bungholio = false;

    public  Transform       hullLink;
    public  Transform       batteryLink;

    public  Transform       specLink;

    public bool Compatible ( ItemPort other ) {
        if ( other.polarity == ItemPolarity.Item && other.mirror != null ) {
            return ItemPolarityChecker.CPC ( polarity, ( ( ItemHandle ) other.mirror ).polarity );
        }
        return ItemPolarityChecker.CPC ( polarity, other.polarity );
    }

    protected override void AutoloadCore () {
        mirror = transform.GetComponentInChildren<ItemHandle> (); ;
    }

    public override void Autobind ( ZephyrUnit _unit ) {
        base.Autobind ( _unit );

        if ( _unit != null ) {
            ItemHandle delta = _unit as ItemHandle;
            if ( bungholio && ( delta.polarity == ItemPolarity.Weapon || delta.polarity == ItemPolarity.Equipment ) ) {
                ZeusPTC ptc = specLink.GetComponent<ZeusPTC> ();
                ptc.RefreshTurretList ();
                if ( delta.polarity == ItemPolarity.Equipment ) {
                    ptc.AdditionalEquipmentTargetOverride ();
                }
            }
        }
    }

    public  ItemHandle  GetItem () {
        if ( mirror == null ) return null;
        return mirror as ItemHandle;
    }

    public void Swap ( ItemPort other ) {
        if ( Compatible ( other ) ) {
            ZephyrUnit  h1 = mirror, h2 = other.mirror;

            if ( h1 != null ) {
                h1.Autobreak ();
            }
            if ( h2 != null ) {
                h2.Autobreak ();
            }

            Autobind ( h2 );
            other.Autobind ( h1 );
        }   
    }
}
