﻿using UnityEngine;
using System;

public class ItemHandle : ZephyrUnit {
    [Header("Item Handle Base")]
    public  ItemPolarity    polarity;

    public  string      itemName;
    public  string      description;
    public  int         itemQuantity = 1;
    public  float       weight = 1;
    public  Sprite      icon;

    public  Transform   visuals;

    public string GetTagName () {
        return ItemPolarityChecker.TFP ( polarity );
    }

    public override void Autobind ( ZephyrUnit _unit ) {
        ItemPort host = null;
        if ( _unit != null ) {
            host = ( ItemPort ) _unit;

            transform.SetParent ( host.transform );
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        EQBase  eqb;
        if ( polarity == ItemPolarity.Equipment && TryGetComponent ( out eqb ) ) {
            eqb.MainInit ( host );
        }
        Thunder thd;
        if ( polarity == ItemPolarity.Weapon && TryGetComponent( out thd ) ) {
            thd.MainInit ( host );
        }

        base.Autobind ( _unit );
    }
}
