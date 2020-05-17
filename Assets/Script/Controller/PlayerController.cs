using PlayerMovimentService;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    // Start is called before the first frame update

    #region Atributos

    private Player player;

    private readonly IPlayerMovimentService playerMovimentService = new PlayerMoviment();

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

    #endregion

    #region Get and Set
    public void SetIdAnimation(int idAnimation) => this.idAnimation = idAnimation;

    public int GetIdAnimation => this.idAnimation;

    public void SetGroundCheck(Transform groundCheck) => this.groundCheck = groundCheck;

    public Transform GetGoundCheck() => this.groundCheck;

    public void SetPlayer(Player player) => this.player = player;

    public Player GetPlayer() => this.player;

    public void SetStanding(BoxCollider2D standing) => this.standing = standing;

    public BoxCollider2D GetStanding() => this.standing;

    public void SetCrounching(BoxCollider2D crounching) => this.crounching = crounching;

    public BoxCollider2D GetCrounching() => this.crounching;

    public void SetHorizontal(float horizontal) => this.horizontal = horizontal;

    public float GetHorizontal() => this.horizontal;

    public void SetVertical(float vertical) => this.vertical = vertical;

    public float GetVertical() => this.vertical;

    public void SetSpeed(float speed) => this.speed = speed;

    public float GetSpeed() => this.speed;

    public void SetJumpForce(float jumpForce) => this.jumpForce = jumpForce;

    public float GetJumpForce() => this.jumpForce;

    public void SetGrounded(bool grounded) => this.grounded = grounded;

    public bool GetGrounded() => this.grounded;

    public void SetIsLookLeft(bool isLookLeft) => this.isLookLeft = isLookLeft;

    public bool GetIsLookLeft() => this.isLookLeft;

    public void SetIsAttacking(bool isAttacking) => this.isAttacking = isAttacking;

    public bool GetIsAttacking() => this.isAttacking;

    #endregion

    private void Awake() {
        player = this.GetComponent<Player>();
        groundCheck = this.transform.GetChild(0);
        player.setAnimator(this.GetComponent<Animator>());
        player.setRigidbody2d(this.GetComponent<Rigidbody2D>());
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
        player.getRigidbody2d().velocity = new Vector2(horizontal * this.speed, player.getRigidbody2d().velocity.y);
    }

    // Update is called once per frame
    void Update() {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        playerMovimentService.movimentacao(this);
        playerMovimentService.ataque(this);
        playerMovimentService.verificaLadoQuePersonagemEstaAndando(this);
        playerMovimentService.fazAnimacoes(this, idAnimation);

    }

    public void EventoAnimacaoVerificaSePlayerIsAttaking(int isAttacking) {
        if(isAttacking == 0) {
            this.isAttacking = false;
        } else {
            this.isAttacking = true;
        }
    }
}
