using UnityEngine;

public class MenuCore : MonoBehaviour {
    protected MenuManager   manager;
    protected int           selfID = -1;
    protected Animator      animator;
    protected bool          status;

    public  virtual void    Handshake ( MenuManager _manager, int _selfID ) {
        manager = _manager;
        selfID = _selfID;
        Start ();
    }

    public virtual  void    Incoming ( bool a ) {
        status = a;
        animator.SetBool ( "Dispatch", a );
    }

    public virtual void Start () {
        animator = GetComponent<Animator> ();
        Backflow ( false );
    }

    public  virtual void Backflow ( bool a, int specPrio = -1 ) {
        if ( manager != null && selfID != -1 ) {
            manager.MenuRequest ( selfID, a, specPrio );
        }
    }

    public  virtual void Update  () {
        if ( Input.GetKey ( KeyCode.Escape ) ) {
            Backflow ( false );
        }
    }
}
