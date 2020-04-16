using UnityEngine;

public class PlayerController : MonoBehaviour {
    // Start is called before the first frame update

    private Player player;
    
    private Transform groundCheck;

    [SerializeField]
    private BoxCollider2D standing;

    private BoxCollider2D crounching;

    private int idAnimation;

    private float horizontal;

    private float vertical;

    [SerializeField]
    [Header("Velocidade do personagem")]
    private float speed;

    [Space(2)]
    [SerializeField]
    [Header("Força do pulo do personagem")]
    private float jumpForce;

    [Space(2)]
    [SerializeField]
    private bool grounded;

    private bool isLookLeft;

    private bool isAttacking;


    private void Awake() {
        player = this.GetComponent<Player>();
        groundCheck = this.transform.GetChild(0);
        player.setAnimator(this.GetComponent<Animator>());
        player.setRigidbody(this.GetComponent<Rigidbody2D>());
        defineTamanhoAndPosicaoCollider();

    }

    private void defineTamanhoAndPosicaoCollider() {
        standing = this.gameObject.AddComponent<BoxCollider2D>();
        standing.offset = new Vector2(-0.02426934f, -0.05258477f);
        standing.size = new Vector2(0.5231423f, 0.8548286f);
        crounching = this.gameObject.AddComponent<BoxCollider2D>();
        crounching.offset = new Vector2(0.04664016f, -0.1360077f);
        crounching.size = new Vector2(0.5148001f, 0.6879828f);
        standing.enabled = true;
        crounching.enabled = false;
    }

    void Start() {
        // Method intentionally left empty.

    }

    private void FixedUpdate() {
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);
        player.getRigidbody().velocity = new Vector2(horizontal * this.speed, player.getRigidbody().velocity.y);
    }

    // Update is called once per frame
    void Update() {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        movimentacao(horizontal, vertical);
        ataque(vertical);
        verificaLadoQuePersonagemEstaAndando(horizontal);
        fazAnimacoes();

    }

    private void verificaLadoQuePersonagemEstaAndando(float horizontal) {
        if (horizontal > 0 && isLookLeft && !this.isAttacking) {
            flipPlayer();
        } else if (horizontal < 0 && !isLookLeft && !this.isAttacking) {
            flipPlayer();
        }
    }

    private void ataque(float vertical) {
        if (Input.GetButtonDown("Fire1") && vertical >= 0 && !this.isAttacking) {
            player.getAnimator().SetTrigger("attack");
        }
        if(this.isAttacking && this.grounded) {
            horizontal = 0;
        }
    }

    private void fazAnimacoes() {
        player.getAnimator().SetInteger("idAnimation", idAnimation);
        player.getAnimator().SetBool("grounded", grounded);
    }

    private void movimentacao(float horizontal, float vertical) {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0) {
            this.idAnimation = 1;
        } else {
            this.idAnimation = 0;
        }

        if (vertical < 0) {
            this.idAnimation = 2;
            this.horizontal = 0;
            ativaAndDesativaPlayerColisorCasoPlayerEstejaAbaixadoOuEmPe(true, false);
        } else {
            ativaAndDesativaPlayerColisorCasoPlayerEstejaAbaixadoOuEmPe(false, true);
        }

        if ((Input.GetButtonDown("Jump") || this.vertical > 0) && grounded) {
            player.getRigidbody().AddForce(new Vector2(0, jumpForce));
            this.vertical = 0;
        }
    }

    private void ativaAndDesativaPlayerColisorCasoPlayerEstejaAbaixadoOuEmPe(bool isCrounching, bool isStanding) {
        this.crounching.enabled = isCrounching;
        this.standing.enabled = isStanding;
    }

    private void flipPlayer() {
        isLookLeft = !isLookLeft;
        float playerScaleX = transform.localScale.x;
        playerScaleX *= -1;
        transform.localScale = new Vector3(playerScaleX, transform.localScale.y, transform.localScale.z);
    }

    public void playerIsAttacking(int isAttacking) {
        if(isAttacking == 0) {
            this.isAttacking = false;
        } else {
            this.isAttacking = true;
        }
    }
}
