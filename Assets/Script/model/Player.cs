using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {

    public Animator animator;

    public void setAnimator(Animator animator) {
        this.animator = animator;
    }

    public Animator getAnimator() {
        return this.animator;
    }

}
