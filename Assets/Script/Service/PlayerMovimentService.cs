using UnityEngine;

namespace PlayerMovimentService {
    public class PlayerMoviment : IPlayerMovimentService {
        public void movimentacao(PlayerController playerController) {
            if (playerController.GetHorizontal() != 0) {
                playerController.SetIdAnimation(1);
            } else {
                playerController.SetIdAnimation(0);
            }

            if (playerController.GetVertical() < 0) {
                playerController.SetIdAnimation(2);
                playerController.SetHorizontal(0);
                ativaAndDesativaPlayerColisorCasoPlayerEstejaAbaixadoOuEmPe(true, false, playerController);
            } else {
                ativaAndDesativaPlayerColisorCasoPlayerEstejaAbaixadoOuEmPe(false, true, playerController);
            }

            if ((Input.GetButtonDown("Jump") || playerController.GetVertical() > 0) && playerController.GetGrounded()) {
                playerController.GetPlayer().getRigidbody2d().AddForce(new Vector2(0, playerController.GetJumpForce()));
                playerController.SetVertical(0);
            }
        }

        public void verificaLadoQuePersonagemEstaAndando(PlayerController playerController) {
            if (playerController.GetHorizontal() > 0 && playerController.GetIsLookLeft() && !playerController.GetIsAttacking()) {
                flipPlayer(playerController);
            } else if (playerController.GetHorizontal() < 0 && !playerController.GetIsLookLeft() && !playerController.GetIsAttacking()) {
                flipPlayer(playerController);
            }
        }

        private void flipPlayer(PlayerController playerController) {
            playerController.SetIsLookLeft(!playerController.GetIsLookLeft());
            float playerScaleX = playerController.transform.localScale.x;
            playerScaleX *= -1;
            playerController.transform.localScale = new Vector3(playerScaleX, playerController.transform.localScale.y, playerController.transform.localScale.z);
        }

        public void fazAnimacoes(PlayerController playerController, int idAnimation) {
            playerController.GetPlayer().getAnimator().SetInteger("idAnimation", idAnimation);
            playerController.GetPlayer().getAnimator().SetBool("grounded", playerController.GetGrounded());
        }

        private void ativaAndDesativaPlayerColisorCasoPlayerEstejaAbaixadoOuEmPe(bool isCrounching, bool isStanding, PlayerController playerController) {
            playerController.GetCrounching().enabled = isCrounching;
            playerController.GetStanding().enabled = isStanding;
        }

        public void ataque(PlayerController playerController) {
            if (Input.GetButtonDown("Fire1") && playerController.GetVertical() >= 0 && !playerController.GetIsAttacking()) {
                playerController.GetPlayer().getAnimator().SetTrigger("attack");
            }
            if (playerController.GetIsAttacking() && playerController.GetGrounded()) {
                playerController.SetHorizontal(0);
            }
        }
    }
}
