using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolownController : NpcController
{
    
    public override void Awake()
    {
        base.Awake();
        CreateListMissoesNpc();
    }

    private void CreateListMissoesNpc()
    {

        #region Missão 1
            MissaoNpc _missaoNpc = new MissaoNpc
            {
                id = 0,
                sender = "Solown",
                nivel = 1,
                xp = 0,
                dinheiro = 2
            };

            _missaoNpc.fala = new string[4];
            _missaoNpc.fala[0] = "Ola, Bem-Vindo!";
            _missaoNpc.fala[1] = "És novo por aqui não és?";
            _missaoNpc.fala[2] = "Este lugar pode ser bem perigoso, porque não vais treinar um bocado?";
            _missaoNpc.fala[3] = "";

            MissaoPlayer _missaoPlayer = new MissaoPlayer
            {
                idNpc = _missaoNpc.id,
                imagem = "Icons/Boneco",
                posImagem = new Vector2(0, -24.5f), 
                tipo = 1, 
                objetivo = 10,
                target = "Boneco", 
                npc = "Solown",
                posNpc = new Vector2(0, -22.9f)
            };
            _missaoNpc.quest = _missaoPlayer;
            ListaMissoesNpc.Add(_missaoNpc);
        #endregion

        #region Missão 2
             _missaoNpc = new MissaoNpc
            {
                id = 1,
                sender = "Solown",
                nivel = 1,
                xp = 10,
                dinheiro = 1
            };

            _missaoNpc.fala = new string[4];
            _missaoNpc.fala[0] = "Bom trabalho com o treino.";
            _missaoNpc.fala[1] = "Não estás interessado em algo mais desafiador?";
            _missaoNpc.fala[2] = "Logo á frente vais encontrar umas slimes, tenta lutar contra elas!";
            _missaoNpc.fala[3] = "";

             _missaoPlayer = new MissaoPlayer
            {
                idNpc = _missaoNpc.id,
                imagem = "Icons/Slime Verde",
                posImagem = new Vector2(0, -24.5f), 
                tipo = 1, 
                objetivo = 3,
                target = "Slime Verde", 
                npc = "Solown",
                posNpc = new Vector2(0, -22.9f)
            };
            _missaoNpc.quest = _missaoPlayer;
            ListaMissoesNpc.Add(_missaoNpc);
        #endregion

        #region Missão 3
             _missaoNpc = new MissaoNpc
            {
                id = 2,
                sender = "Solown",
                nivel = 2,
                xp = 20,
                dinheiro = 3
            };

            _missaoNpc.fala = new string[4];
            _missaoNpc.fala[0] = "Hey! Ainda bem que te encontro...";
            _missaoNpc.fala[1] = "Estas slimes não me deixam dormir á noite são muito barulhentas!";
            _missaoNpc.fala[2] = "Podes te livrar delas por mim?";
            _missaoNpc.fala[3] = "";

             _missaoPlayer = new MissaoPlayer
            {
                idNpc = _missaoNpc.id,
                imagem = "Icons/Slime Verde",
                posImagem = new Vector2(0, -24.5f), 
                tipo = 1, 
                objetivo = 7,
                target = "Slime Verde", 
                npc = "Solown",
                posNpc = new Vector2(0, -22.9f)
            };
            _missaoNpc.quest = _missaoPlayer;
            ListaMissoesNpc.Add(_missaoNpc);
        #endregion

        #region Missão 4
             _missaoNpc = new MissaoNpc
            {
                id = 3,
                sender = "Solown",
                nivel = 3,
                xp = 30,
                dinheiro = 7
            };

            _missaoNpc.fala = new string[4];
            _missaoNpc.fala[0] = "Hey!";
            _missaoNpc.fala[1] = "Tou a ver que já estás mais forte.";
            _missaoNpc.fala[2] = "Achas que consegues derrotar as slimes azuis agora?";
            _missaoNpc.fala[3] = "";

             _missaoPlayer = new MissaoPlayer
            {
                idNpc = _missaoNpc.id,
                imagem = "Icons/Slime Azul",
                posImagem = new Vector2(0, -24.5f), 
                tipo = 1, 
                objetivo = 5,
                target = "Slime Azul", 
                npc = "Solown",
                posNpc = new Vector2(0, -22.9f)
            };
            _missaoNpc.quest = _missaoPlayer;
            ListaMissoesNpc.Add(_missaoNpc);
        #endregion

        #region Missão 5
            _missaoNpc = new MissaoNpc
            {
                id = 4,
                sender = "Solown",
                nivel = 3,
                xp = 40,
                dinheiro = 10
            };

            _missaoNpc.fala = new string[3];
            _missaoNpc.fala[0] = "Ola!";
            _missaoNpc.fala[1] = "Apareceram mais slimes azuis recentemente.";
            _missaoNpc.fala[2] = "";

            _missaoPlayer = new MissaoPlayer
            {
                idNpc = _missaoNpc.id,
                imagem = "Icons/Slime Azul",
                posImagem = new Vector2(0, -24.5f), 
                tipo = 1, 
                objetivo = 7,
                target = "Slime Azul", 
                npc = "Solown",
                posNpc = new Vector2(0, -22.9f)
            };
            _missaoNpc.quest = _missaoPlayer;
            ListaMissoesNpc.Add(_missaoNpc);
        #endregion
    }
}
