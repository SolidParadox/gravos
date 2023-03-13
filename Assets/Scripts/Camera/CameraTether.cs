using UnityEngine;

public class CameraTether : MonoBehaviour {
    public  Transform   target;
    public  Vector3     offset;

    public  Rigidbody2D trackingRigidbody;
    public  float       velocityFactor;

    public  float       strength;
    public  float       rotationStrength;

    private Vector3     delta;

    void LateUpdate() {
        if ( trackingRigidbody != null ) {
            delta = ( Vector3 )target.position + ( Vector3 )trackingRigidbody.velocity * velocityFactor;
        } else {
            delta = target.position;
        }

        delta += offset;

        transform.position = Vector3.Lerp( transform.position - offset, delta, strength ) + offset;
        transform.rotation = Quaternion.Lerp ( transform.rotation, target.rotation, rotationStrength * Time.unscaledDeltaTime );
    }
}
