
public interface IPlayerMovimentService {
    void movimentacao(PlayerController playerController);
    void verificaLadoQuePersonagemEstaAndando(PlayerController playerController);
    void fazAnimacoes(PlayerController playerController, int idAnimation);
    void ataque(PlayerController playerController);
}
