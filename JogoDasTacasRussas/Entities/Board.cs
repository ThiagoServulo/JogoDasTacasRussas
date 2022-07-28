﻿using System.Drawing;
using System.Windows.Forms;
using JogoDasTacasRussas.Entities.Enums;

namespace JogoDasTacasRussas.Entities
{
    /** ************************************************************************
    * \brief Informações sobre o tabuleiro.
    * \details A classe Board armazena as informações referentes ao tabuleiro 
    * do jogo, ou seja, onde as peças serão colocadas.
    * \author Thiago Sérvulo Guimarães - thiago.servulo@sga.pucminas.br
    * \date 19/07/2022
    * \version v1.0
    ***************************************************************************/
    class Board
    {
        /// \brief Informações da jogada atual.
        public Move PlayerMove { get; private set; }

        /// \brief Lista contendo os campos iniciais do jogador X.
        public Field[] InitFieldsPlayerX { get; private set; }

        /// \brief Lista contendo os campos iniciais do jogador Y.
        public Field[] InitFieldsPlayerY { get; private set; }

        /// \brief Lista contendo os campos do tabuleiro comum para os jogadores.
        public Field[] FieldsBoard { get; private set; }

        /// \brief Lista contendo todos os campos do tabuleiro.
        public Field[][] AllFields { get; private set; }

        /// \brief Informações relativas ao jogador X.
        public Player PlayerX { get; private set; }

        /// \brief Informações relativas ao jogador Y.
        public Player PlayerY { get; private set; }

        /// \brief Informações relativas ao jogador atual.
        public Player CurrentPlayer { get; private set; }

        /// \brief Tela onde o tabuleiro será apresentado.
        public FormBoard Form { get; private set; }


        /** ************************************************************************
        * \brief Construtor da classe Board.
        * \param form Tela onde o tabuleiro será apresentado.
        ***************************************************************************/
        public Board(FormBoard form)
        {
            this.Form = form;

            // Inicialização do tabuleiro
            this.InitBoard();

            // Inicialização dos jogadores
            this.PlayerX = new Player(PlayerType.PlayerX);
            this.PlayerY = new Player(PlayerType.PlayerY);
            this.CurrentPlayer = this.PlayerX;
        }


        /** ************************************************************************
        * \brief Inicializa o tabuleiro.
        * \details Função responsável por inicializar o tabuleiro do jogo.
        ***************************************************************************/
        public void InitBoard()
        {
            // Inicialização da movimentação
            this.PlayerMove = new Move();

            // Inicialização das peças do Jogador X
            this.InitFieldsPlayerX = new Field[] { 
                new Field(this.Form.pictureBoxX1),  new Field(this.Form.pictureBoxX2),  new Field(this.Form.pictureBoxX3),
                new Field(this.Form.pictureBoxX4),  new Field(this.Form.pictureBoxX5),  new Field(this.Form.pictureBoxX6),
                new Field(this.Form.pictureBoxX7),  new Field(this.Form.pictureBoxX8),  new Field(this.Form.pictureBoxX9),
                new Field(this.Form.pictureBoxX10), new Field(this.Form.pictureBoxX11), new Field(this.Form.pictureBoxX12)};
            this.InitFields(this.InitFieldsPlayerX, Color.DarkRed, Color.IndianRed);

            // Inicialização das peças do Jogador Y
            this.InitFieldsPlayerY = new Field[] {
                new Field(this.Form.pictureBoxY1),  new Field(this.Form.pictureBoxY2),  new Field(this.Form.pictureBoxY3),
                new Field(this.Form.pictureBoxY4),  new Field(this.Form.pictureBoxY5),  new Field(this.Form.pictureBoxY6),
                new Field(this.Form.pictureBoxY7),  new Field(this.Form.pictureBoxY8),  new Field(this.Form.pictureBoxY9),
                new Field(this.Form.pictureBoxY10), new Field(this.Form.pictureBoxY11), new Field(this.Form.pictureBoxY12)};
            this.InitFields(this.InitFieldsPlayerY, Color.DarkBlue, Color.LightBlue);

            // Inicialização do tabuleiro
            this.FieldsBoard = new Field[] {
                new Field(this.Form.pictureBoxA1), new Field(this.Form.pictureBoxA2), new Field(this.Form.pictureBoxA3), 
                new Field(this.Form.pictureBoxA4), new Field(this.Form.pictureBoxB1), new Field(this.Form.pictureBoxB2), 
                new Field(this.Form.pictureBoxB3), new Field(this.Form.pictureBoxB4), new Field(this.Form.pictureBoxC1),
                new Field(this.Form.pictureBoxC2), new Field(this.Form.pictureBoxC3), new Field(this.Form.pictureBoxC4),
                new Field(this.Form.pictureBoxD1), new Field(this.Form.pictureBoxD2), new Field(this.Form.pictureBoxD3), 
                new Field(this.Form.pictureBoxD4)};
            this.InitFields(this.FieldsBoard, null, null);

            // Concatenação de todos os campos do tabuleiro
            this.AllFields = new Field[][] { this.FieldsBoard, this.InitFieldsPlayerY, this.InitFieldsPlayerX };
        }


        /** ************************************************************************
        * \brief Inicializa os campos.
        * \details Função responsável por inicializar os campos do tabuleiro, 
        * adicionando as peças ou deixando-os vazios.
        * \param fields Lista contendo os campos que serão inicializados.
        * \param primaryColor Cor primária da possível peça adicionada.
        * \param secundaryColor Cor secundária da possível peça adicionada.
        ***************************************************************************/
        public void InitFields(Field[] fields, Color? primaryColor, Color? secundaryColor)
        {
            for (int i = 0; i < fields.Length; i++)
            {
                // Habilitar para que o campo possa ser selecionado
                fields[i].FieldPictureBox.Enabled = true;

                if (primaryColor == null || secundaryColor == null) // Se as cores forem nulas, o campo será limpado
                {
                    fields[i].DrawCircle(null);
                }
                else if (i < 3) // Adiciona o menor círculo existente
                {
                    fields[i].AddCircle(new Circle((Color)primaryColor, (Color)secundaryColor, CircleType.Type1));
                }
                else if (i < 6) // Adiciona o segundo menor círculo existente
                {
                    fields[i].AddCircle(new Circle((Color)primaryColor, (Color)secundaryColor, CircleType.Type2));
                }
                else if (i < 9) // Adiciona o segundo maior círculo existente
                {
                    fields[i].AddCircle(new Circle((Color)primaryColor, (Color)secundaryColor, CircleType.Type3));
                }
                else // Adiciona o maior círculo existente
                {
                    fields[i].AddCircle(new Circle((Color)primaryColor, (Color)secundaryColor, CircleType.Type4));
                }
            }
        }


        /** ************************************************************************
        * \brief Processa o envento de click sobre um \a pictureBox.
        * \details Função responsável por processar a interrupção do usuário ao 
        * clicar em um determinado \a pictureBox.
        * \param pictureBox \a pictureBox clicado pelo usuário.
        ***************************************************************************/
        public void Click(PictureBox pictureBox)
        {
            Field field = this.GetField(pictureBox);

            // Se a jogada estiver sido finalizada, será alternado para o próximo jogador
            if (this.PlayerMove.Play(field, this.CurrentPlayer) == PlayStatus.Finish)
            {
                Player playerWinner = this.CheckWinner();
                if (playerWinner != null)
                {
                    playerWinner.AddVictory();
                    this.InitBoard();
                    MessageBox.Show($"{PlayerX.Victories} - {PlayerY.Victories}");
                }
                this.ChangeCurrentPlayer();
            }
        }


        /** ************************************************************************
        * \brief Identifica um campo.
        * \details Função responsável por converter um \a pictureBox para um campo
        * conhecido.
        * \param pictureBox \a pictureBox clicado pelo usuário.
        * \return Valor do tipo Field, que apresenta as informações do campo 
        * referentes ao \a pictureBox informado.
        ***************************************************************************/
        public Field GetField(PictureBox pictureBox)
        {
            foreach(Field[] listFields in this.AllFields)
            {
                foreach (Field field in listFields)
                {
                    if (field.FieldPictureBox == pictureBox)
                    {
                        return field;
                    }
                }
            }
            return null;
        }


        /** ************************************************************************
        * \brief Troca o jogador atual.
        * \details Função responsável por realizar a troca do jogador atual.
        ***************************************************************************/
        public void ChangeCurrentPlayer()
        {
            this.CurrentPlayer = (this.CurrentPlayer.Type == PlayerType.PlayerX) ? this.PlayerY : this.PlayerX;
        }


        /** ************************************************************************
        * \brief Verifica se há um ganhador.
        * \details Função responsável por verificar se há um ganhador na partida.
        * \return Valor do tipo Player, indicando o ganhador da partida atual. Se
        * não houver ganhador é retornado \a null.
        ***************************************************************************/
        public Player CheckWinner()
        {
            // Checar colunas
            for(int i = 0; i <= 12; i+=4)
            {
                if (this.FieldsBoard[i].Equals(this.FieldsBoard[i + 1]) && 
                    this.FieldsBoard[i].Equals(this.FieldsBoard[i + 2]) && 
                    this.FieldsBoard[i].Equals(this.FieldsBoard[i + 3]))
                {
                    return this.CheckPlayer(this.FieldsBoard[i]);
                }
            }

            // Checar linhas
            for (int i = 0; i <= 4; i++)
            {
                if (this.FieldsBoard[i].Equals(this.FieldsBoard[i + 4]) &&
                    this.FieldsBoard[i].Equals(this.FieldsBoard[i + 8]) &&
                    this.FieldsBoard[i].Equals(this.FieldsBoard[i + 12]))
                {
                    return this.CheckPlayer(this.FieldsBoard[i]);
                }
            }

            // Checar diagonal principal
            if (this.FieldsBoard[0].Equals(this.FieldsBoard[5]) &&
                this.FieldsBoard[0].Equals(this.FieldsBoard[10]) &&
                this.FieldsBoard[0].Equals(this.FieldsBoard[15]))
            {
                return this.CheckPlayer(this.FieldsBoard[0]);
            }

            // Checar diagonal principal
            if (this.FieldsBoard[3].Equals(this.FieldsBoard[6]) &&
                this.FieldsBoard[3].Equals(this.FieldsBoard[9]) &&
                this.FieldsBoard[3].Equals(this.FieldsBoard[12]))
            {
                return this.CheckPlayer(this.FieldsBoard[3]);
            }

            // Retorna null se não houver ganhador
            return null;
        }


        /** ************************************************************************
        * \brief Verifica o jogador dono da peça.
        * \details Função responsável por checar de qual jogador é a peça que se
        * encontra no campo informado.
        * \param field Campo que será analizado.
        * \return Valor do tipo Player, indicando o jogador dono da peça presente
        * no campo.
        ***************************************************************************/
        public Player CheckPlayer(Field field)
        {
            // Pega a cor do círculo presnete no campo
            Color circleColor = field.GetLast().Color.Primary;

            // Ver o jogador dono desta peça
            if(circleColor == Color.DarkRed)
            {
                return this.PlayerX;
            }
            else if (circleColor == Color.DarkBlue)
            {
                return this.PlayerY;
            }

            // Retorna null se o jogador não for encontrado
            return null;
        }
    }
}
