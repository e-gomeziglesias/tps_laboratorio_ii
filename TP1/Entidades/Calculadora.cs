using System;

namespace Entidades
{
    public static class Calculadora
    {
        #region Métodos
        /// <summary>
        /// Permite validar un operador ingresado (+ - * /).
        /// </summary>
        /// <param name="operador"></param>
        /// <returns>El operador, si es válido. Sino, retorna '+'.</returns>
        private static char ValidarOperador(char operador)
        {
            char retorno = '+';

            if(operador == '+' || operador == '-' || operador == '/' || operador == '*')
            {
                retorno = operador;
            }

            return retorno;
        }

        /// <summary>
        /// Permite realizar una operación entre dos números.
        /// </summary>
        /// <param name="num1">Primer operando.</param>
        /// <param name="num2">Segundo operando.</param>
        /// <param name="operador">Operador del cálculo.</param>
        /// <returns>El resultado de la operación. En caso de error, devuelve el mínimo valor de double.</returns>
        public static double Operar(Operando num1, Operando num2, char operador)
        {
            double retorno;
            double resultado = double.MinValue; //usé el mismo criterio, en caso de operando null, que para la sobrecarga de operadores (ver sobrecarga de operadores en Operando.cs)
            if((num1 is not null) && (num2 is not null)) //Valida que los operandos estén instanciados
            switch (ValidarOperador(operador))
            {
                case '+':
                    resultado = num1 + num2;
                    break;
                case '-':
                    resultado = num1 - num2;
                    break;
                case '/':
                    resultado = num1 / num2;
                    break;
                case '*':
                    resultado = num1 * num2;
                    break;
            }
            retorno = resultado;

            return retorno;
        }
       
        #endregion
    }
}