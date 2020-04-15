using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour {
    // Start is called before the first frame update

    public Player player;

    private int idAnimation;

    private bool grounded = true;

    public float horizontal;

    void Start() {
        // Method intentionally left empty.
        player = this.GetComponent<Player>();
        player.setAnimator(this.GetComponent<Animator>());
        
    }

    // Update is called once per frame
    void Update() {
        // Method intentionally left empty.
        movimentacao();
        fazAnimacoes();

    }

    private void fazAnimacoes() {
        player.getAnimator().SetInteger("idAnimation", idAnimation);
        player.getAnimator().SetBool("grounded", grounded);
    }

    private void movimentacao() {
        horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0) {
            this.idAnimation = 1;
        } else {
            this.idAnimation = 0;
        }

        if (vertical < 0) {
            this.idAnimation = 2;
        }
    }
}
