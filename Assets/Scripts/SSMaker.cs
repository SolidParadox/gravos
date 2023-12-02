using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSMaker : MonoBehaviour {
  private void Update() {
    if ( Input.GetKeyDown(KeyCode.Q) ) {
      ScreenCapture.CaptureScreenshot( "SomeLevel.png" );
    }
  }
}
