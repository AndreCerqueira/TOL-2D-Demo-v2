using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissaoNpc
{
    // ID da Missão (posição na lista)
    public int id;

    // Npc que pediu a missao
    public string sender;

    // Nivel preciso para fazer a Missão
    public int nivel;

    // Status da missao se ta feita ou vai ser feita pode ser: (pendente / aceitada / concluida / completada / bloqueada)
    public string status;

    // falas do npc na missao
    public string[] fala;

    // premios da missao
    public int dinheiro;
    public int xp;

    // missao para o player
    public MissaoPlayer quest;
}
