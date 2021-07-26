using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BibianController : NpcController
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
                sender = "Bibian",
                nivel = 1,
                xp = 5,
                dinheiro = 1
            };

            _missaoNpc.fala = new string[3];
            _missaoNpc.fala[0] = "Bom Dia! Novo por aqui?";
            _missaoNpc.fala[1] = "Tava a precisar de ajuda, tens tempo?";
            _missaoNpc.fala[2] = "";

            MissaoPlayer _missaoPlayer = new MissaoPlayer
            {
                idNpc = _missaoNpc.id,
                imagem = "Icons/Slime Verde",
                posImagem = new Vector2(0, -24.5f), 
                tipo = 1, 
                objetivo = 5,
                target = "Slime Verde", 
                npc = "Bibian",
                posNpc = new Vector2(0, -22.9f)
            };
            _missaoNpc.quest = _missaoPlayer;
            ListaMissoesNpc.Add(_missaoNpc);
        #endregion
        
        #region Missão 2
             _missaoNpc = new MissaoNpc
            {
                id = 1,
                sender = "Bibian",
                nivel = 2,
                xp = 10,
                dinheiro = 2
            };

            _missaoNpc.fala = new string[3];
            _missaoNpc.fala[0] = "Tens mais Tempo?";
            _missaoNpc.fala[1] = "Mais Slimes então porfavor.";
            _missaoNpc.fala[2] = "";

             _missaoPlayer = new MissaoPlayer
            {
                idNpc = _missaoNpc.id,
                imagem = "Icons/Slime Verde",
                posImagem = new Vector2(0, -24.5f), 
                tipo = 1, 
                objetivo = 10,
                target = "Slime Verde", 
                npc = "Bibian",
                posNpc = new Vector2(0, -22.9f)
            };
            _missaoNpc.quest = _missaoPlayer;
            ListaMissoesNpc.Add(_missaoNpc);
        #endregion

        #region Missão 2
             _missaoNpc = new MissaoNpc
            {
                id = 2,
                sender = "Bibian",
                nivel = 3,
                xp = 30,
                dinheiro = 15
            };

            _missaoNpc.fala = new string[3];
            _missaoNpc.fala[0] = "Bom dia!";
            _missaoNpc.fala[1] = "Consegues acabar com umas Slimes azuis agora?";
            _missaoNpc.fala[2] = "";

             _missaoPlayer = new MissaoPlayer
            {
                idNpc = _missaoNpc.id,
                imagem = "Icons/Slime Azul",
                posImagem = new Vector2(0, -24.5f), 
                tipo = 1, 
                objetivo = 10,
                target = "Slime Azul", 
                npc = "Bibian",
                posNpc = new Vector2(0, -22.9f)
            };
            _missaoNpc.quest = _missaoPlayer;
            ListaMissoesNpc.Add(_missaoNpc);
        #endregion

    }
}
