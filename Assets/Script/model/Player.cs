using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {

    private Animator animator;

    private Rigidbody2D rigidbody2d;

    public void setRigidbody2d(Rigidbody2D rigidbody2d) {
        this.rigidbody2d = rigidbody2d;
    }

    public Rigidbody2D getRigidbody2d() {
        return this.rigidbody2d;
    }

    public void setAnimator(Animator animator) {
        this.animator = animator;
    }

    public Animator getAnimator() {
        return this.animator;
    }

}
