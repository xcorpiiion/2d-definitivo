using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {

    private Animator animator;

    private Rigidbody2D rigidbody;

    public void setRigidbody(Rigidbody2D rigidbody) {
        this.rigidbody = rigidbody;
    }

    public Rigidbody2D getRigidbody() {
        return this.rigidbody;
    }

    public void setAnimator(Animator animator) {
        this.animator = animator;
    }

    public Animator getAnimator() {
        return this.animator;
    }

}
