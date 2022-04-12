using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Operando
    {
        #region Atributos
        private double numero;
        #endregion

        #region Constructores
        /// <summary>
        /// Constructor por defecto de Operando
        /// </summary>
        public Operando()
        {
            this.numero = 0;
        }
        
        /// <summary>
        /// Sobrecarga del constructor de Operando.
        /// </summary>
        /// <param name="numero">Numero double que inicializa el atributo numero</param>
        public Operando(double numero)
        {
            this.numero = numero;
        }

        /// <summary>
        /// Sobrecarga del constructor de Operando.
        /// </summary>
        /// <param name="strNumero">Carga el numero (en formato cadena) en la variable</param>
        public Operando(string strNumero)
        {
            this.Numero = strNumero;
        }
        #endregion

        #region Propiedades

        /// <summary>
        /// Setea el número, previa validación
        /// </summary>
        private string Numero
        {
            set
            {
                this.numero = this.ValidarOperando(value);
            }
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Valida que la cadena sea un número
        /// </summary>
        /// <param name="strNumero">Cadena a validar</param>
        /// <returns>Si es un numero, lo retorna. Sino, retorna 0</returns>
        private double ValidarOperando(string strNumero)
        {
            double retorno = 0;

            double.TryParse(strNumero, out retorno);

            return retorno;
        }

        /// <summary>
        /// Valida que una cadena sea un numero binario
        /// </summary>
        /// <param name="binario">Cadena a validar</param>
        /// <returns>True si es  binario, falso si no lo es.</returns>
        private bool EsBinario(string binario)
        {
            bool retorno = false;
            foreach (char item in binario)
            {
                if(item == '0' || item == '1')
                {
                    retorno = true;
                }
            }

            return retorno;
        }

        /// <summary>
        /// Convierte un numero entero positivo, en binario
        /// </summary>
        /// <param name="numero">Numero (double) a convertir.</param>
        /// <returns>El numero binario en formato de cadena.</returns>
        public string DecimalBinario(double numero)
        {
            string cadena = "";
            string retorno = "Valor inválido";

            if (numero > 0 && numero % 1 == 0) //valido que sea positivo y entero (no incluyo el cero porque el cero no es positivo)
                {
                    while(numero > 0)
                    {
                        if (numero % 2 == 0)
                        {
                            cadena = "0" + cadena; //si el resto es 0 => 0
                        }
                        else
                        {
                            cadena = "1" + cadena; //si el resto es distinto de 0 => 1
                        }
                        
                        numero /= 2;
                        numero = Math.Truncate(numero);
                }
                    retorno = cadena;
                }
            /*}*/
            return retorno;
        }
        
        /// <summary>
        /// Convierte un numero entero positivo, en binario
        /// </summary>
        /// <param name="numero">Numero (cadena) a convertir.</param>
        /// <returns>El numero en binario como cadena si se pudo convertir. Sino, devuelve valor inválido.</returns>
        public string DecimalBinario(string numero)
        {
            string retorno = "Valor invalido";
            double dNumero = 0;
            if(double.TryParse(numero, out dNumero)) //si puedo parsear la cadena, lo convierto a binario (reutilizando código)
            {
                retorno = this.DecimalBinario(dNumero);
            }
            return retorno;
        }

        /// <summary>
        /// Permite convertir un numero binario en decimal.
        /// </summary>
        /// <param name="binario">Numero binario en formato de cadena.</param>
        /// <returns>El numero decimal (en formato cadena), si se pudo convertir. Sino devuelve "Valor invalido".</returns>
        public string BinarioDecimal(string binario)
        {
            string retorno = "Valor inválido";
            int pesoPosicion = 1;
            double numeroDecimal = 0;
            if(this.EsBinario(binario))
            {
                for(int i = binario.Length-1; i>=0; i--)
                {
                    if(binario[i] == '1') //sumo los valores de los pesos de cada posición que sea igual a 1, comenzando desde la más baja (2^0)
                    {
                        numeroDecimal += pesoPosicion;
                    }
                    pesoPosicion *= 2; //multiplico el peso por 2 (en el fondo, me voy moviendo por las potencias 2^0, 2^1, 2^2...)
                }
                retorno = numeroDecimal.ToString(); //retorno el numero pero en formato cadena
            }
            return retorno;
        }
        #endregion

        #region Sobrecarga de Operadores
            /*OBSERVACIÓN: 
            En caso de error por algún operando null, opté por devolver 
            siempre (no sólo en la división por cero) el mínimo valor de double para que fuera bien 
            explícito el error, si ocurriera.*/



        /// <summary>
        /// Permite restar dos objetos Operando.
        /// </summary>
        /// <param name="n1">Primer operando.</param>
        /// <param name="n2">Segundo operando.</param>
        /// <returns>El resultado de la resta.</returns>
        public static double operator - (Operando n1, Operando n2)
        {
            double retorno = double.MinValue;
            if ((n1 is not null) && (n2 is not null))
            {
                retorno = n1.numero - n2.numero;
            }
            return retorno;
        }

        /// <summary>
        /// Permite realizar el producto de dos objetos Operando.
        /// </summary>
        /// <param name="n1">Primer operando.</param>
        /// <param name="n2">Segundo operando.</param>
        /// <returns>El resultado del producto.</returns>
        public static double operator * (Operando n1, Operando n2)
        {
            double retorno = double.MinValue;
            if ((n1 is not null) && (n2 is not null))
            {
                retorno = n1.numero * n2.numero;
            }
            return retorno;
        }

        /// <summary>
        /// Permite realizar la división entre dos objetos Operando
        /// </summary>
        /// <param name="n1">Primer operando.</param>
        /// <param name="n2">Segundo operando.</param>
        /// <returns>El resultado de la división. Si el divisor es cero devuelve el mínimo valor de double.</returns>
        public static double operator / (Operando n1, Operando n2)
        {
            double retorno = double.MinValue;
            if ((n1 is not null) && (n2 is not null) && (n2.numero != 0))
            {
                retorno = n1.numero / n2.numero;
            }
            return retorno;
        }

        /// <summary>
        /// Permite realizar la suma de dos objetos Operando.
        /// </summary>
        /// <param name="n1">Primer operando.</param>
        /// <param name="n2">Segundo operando.</param>
        /// <returns>El resultado de la suma.</returns>
        public static double operator + (Operando n1, Operando n2)
        {
            double retorno = double.MinValue;
            if ((n1 is not null) && (n2 is not null))
            {
                retorno = n1.numero + n2.numero;
            }
            return retorno;
        }
        #endregion
    }
}
