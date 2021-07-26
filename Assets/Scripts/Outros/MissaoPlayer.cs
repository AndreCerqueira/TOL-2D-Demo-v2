using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissaoPlayer
{

    // ID da Missão (posição na lista)
    public int id { get; set; }

    // ID da Missão no npc (posição na lista do npc)
    public int idNpc { get; set; }

    // Imagem da Missão Pos e size da imagem
    public string imagem { get; set; }
    public Vector2 posImagem { get; set; }

    // Pos da Missão
    public Vector2 posObj { get; set; }

    // Tipo de Missão Pode ser: { 1- Matar Mobs, 2- Apanhar Drops, 3- Falar com Npcs }
    public int tipo { get; set; }

    // Quem pediu pela missao a pos e o size
    public string npc { get; set; }
    public Vector2 posNpc { get; set; }

    // Objetivo da Missão é a quantidade de coisas a fazer: { Matar (num) Mobs, Apanhar (num) Drops, Falar com Npc (nada) }
    public int objetivo { get; set; }

    // Progresso da Missão é a quantidade de coisas feitas ate o momento: { Matar (num) Mobs, Apanhar (num) Drops }
    public int progresso { get; set; }
    
    // Alvo da missão { O Mob ou o Npc }
    public string target { get; set; }

}
