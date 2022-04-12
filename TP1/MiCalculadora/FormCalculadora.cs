using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiCalculadora
{
    public partial class FormCalculadora : Form
    {
        #region Constructor
        public FormCalculadora()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Evento de carga del formulario principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormCalculadora_Load(object sender, EventArgs e)
        {
            Limpiar();
        }

        /// <summary>
        /// Evento de presionar el botón Cerrar - Intenta cerrar el formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Evento de presionar el botón "Convertir A Binario" - Convierte el resultado a binario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConvertirABinario_Click(object sender, EventArgs e)
        {
            Operando opDecimal = new Operando();
            StringBuilder sbCadena = new StringBuilder();
            string strResultado = lblResultado.Text.ToString();
            sbCadena.AppendFormat(strResultado); //guardo el operando del conversor
            strResultado = opDecimal.DecimalBinario(lblResultado.Text);
            sbCadena.AppendFormat("(d) = {0}(b)", strResultado);
            sbCadena.Append(Environment.NewLine);
            
            lblResultado.Text = strResultado;
            lstOperaciones.Items.Add(sbCadena);
            
        }

        /// <summary>
        /// Evento de presionar el botón "Convertir A Decimal" - Convierte el resultado a decimal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConvertirADecimal_Click(object sender, EventArgs e)
        {
            Operando opBinario = new Operando();
            //lblResultado.Text = opBinario.BinarioDecimal(lblResultado.Text);

            StringBuilder sbCadena = new StringBuilder();
            string strResultado = lblResultado.Text.ToString();
            sbCadena.AppendFormat(strResultado); //guardo el operando del conversor
            strResultado = opBinario.BinarioDecimal(lblResultado.Text);
            sbCadena.AppendFormat("(b) = {0}(d)", strResultado);
            sbCadena.Append(Environment.NewLine);
            //tuve que repetir algo de código del stringBuilder en estos dos métodos para evitar hacer métodos nuevos.

            lblResultado.Text = strResultado;
            lstOperaciones.Items.Add(sbCadena);
        }

        /// <summary>
        /// Evento de presionar el botón "Limpiar" - Permite limpiar los textBox de los operandos, el comboBox del operador y el label de resultado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        /// <summary>
        /// Evento de presionar el botón "Operar" - Permite realizar el cálculo seleccionado entre los operandos. Por defecto, suma.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOperar_Click(object sender, EventArgs e)
        {
            //declaro variables y capturo datos del formulario, validándolos.
            
            //Estos operandos los uso sólo para poder validar mediante el tryparse
            double operando1;
            double operando2;
            
            string strOperando1 = txtNumero1.Text.ToString();
            string strOperando2 = txtNumero2.Text.ToString();
            string operador = this.cmbOperador.Text;

            //hago este cambio para que no se omita el punto en el ingreso de datos
            strOperando1 = strOperando1.Replace('.', ',');
            strOperando2 = strOperando2.Replace('.', ',');

            if ((double.TryParse(strOperando1, out operando1)) && (double.TryParse(strOperando2, out operando2)))
            { 
                StringBuilder operacionDetallada = new StringBuilder();

                double resultado;
                
                /*Necesité aplicar este redondeo ya que si tenía de operando "algo,9" en una resta
                 o una división, la respuesta me aparecía con muchos ceros, como si intentara redondear
                los ceros. No rompía pero no se veía bien, estéticamente. Con este parche se solucionó.*/
                resultado = Math.Round(Operar(strOperando1, strOperando2, operador), 10);

                this.lblResultado.Text = resultado.ToString();

                //hago esta modificación sólo para que se muestre bien la operación en el listbox.
                if (string.IsNullOrWhiteSpace(operador)) 
                {
                    /*Opté por no cambiar el valor del comboBox por '+' (dejarlo vacío) cuando
                     no se seleccionó la operación para que el usuario sepa que, 
                    efectivamente, no seleccionó nada*/
                    operador = "+";
                }

                operacionDetallada.AppendFormat("{0} {1} {2} = {3}", operando1, operador, operando2, resultado);
                operacionDetallada.Append(Environment.NewLine);
                this.lstOperaciones.Items.Add(operacionDetallada);
            }
            else
            {
                this.lblResultado.Text = "Operando inválido.";
            }
        }

        /// <summary>
        /// Evento de comienzo de cierre del formulario - Muestra un mensaje de confirmación para cerrar el formulario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormCalculadora_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult respuesta;
            respuesta = MessageBox.Show("¿Seguro de querer salir?", "Salir", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        #endregion

        #region Métodos
        /// <summary>
        /// Permite realizar una operación (+ - * /) entre dos operandos.
        /// </summary>
        /// <param name="numero1">Primer operando.</param>
        /// <param name="numero2">Segundo operando.</param>
        /// <param name="operador">Operador.</param>
        /// <returns>El resultado de la operación si fue exitoso, el mínimo valor de double si se produce un error.</returns>
        private double Operar(string numero1, string numero2, string operador)
        {
            //Para poder usar las funciones de Calculadora, necesito instanciar los operandos.
            Operando operando1 = new Operando(numero1);
            Operando operando2 = new Operando(numero2);
            
            return Calculadora.Operar(operando1, operando2, operador[0]);
        }

        /// <summary>
        /// Permite limpiar los textBox de los operandos, el comboBox del operador y el label de resultado
        /// </summary>
        private void Limpiar()
        {
            this.txtNumero1.Clear();
            this.txtNumero2.Clear();
            this.cmbOperador.SelectedIndex = 0;
            this.lblResultado.Text = "0";
        }
        #endregion
    }
}
