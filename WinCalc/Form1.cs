using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Calc;

namespace WinCalc
{
    public partial class Form1 : Form
    {
        private Calc.Calc calc = new Calc.Calc();

        public Form1()
        {
            InitializeComponent();
            this.Text = R.Calc;
            this.enterLabel.Text = R.EnterExpression;
            this.resultLabel.Text = R.Result;
            this.calculateButton.Text = R.Calculate;
        }

        private void calculate () {
            String expression = this.expressionTextBox.Text;
            try {
                Double result = this.calc.calculate(expression);
                this.resultTextBox.Text = result.ToString();
            } catch (EvaluationExcpetion) {
                MessageBox.Show(R.EvaluationError);
            }
        }

        private void calculateButton_Click (object sender, EventArgs e) {
            calculate();
        }

        private void expressionTextBox_KeyDown (object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter && this.expressionTextBox.Text != String.Empty) {
                calculate();
            }
        }
    }
}
