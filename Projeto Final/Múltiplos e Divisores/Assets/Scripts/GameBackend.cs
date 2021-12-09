using UnityEngine;

namespace GameBackend
{
    public class Carta
    {
        private int valor;
        private char tipo; 

        public Carta(int valor, char tipo)
        {
            this.setValor(valor);
            this.setTipo(tipo);
            Debug.Log("Construtor de Carta chamado: " + this.getValor() + this.getTipo());
        }

        public void setValor(int valor)
        {
            this.valor = valor;
        }

        public void setTipo(char tipo)
        {
            this.tipo = tipo;
        }

        public int getValor()
        {
            return this.valor;
        }

        public char getTipo()
        {
            return this.tipo;
        }
    }
}