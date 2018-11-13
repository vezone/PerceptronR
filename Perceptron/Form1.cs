using System;
using System.Windows.Forms;
using Perceptron.src;
using Perceptron.src.math;


namespace Perceptron
{
    public partial class Form1 : Form
    {
        src.Perceptron per1;
        src.Perceptron per2;
        MLP mlp;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            //learning rate
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Functions functions = new Functions();
            double learningRate = (double)trackBar1.Value / 10;
            textBox5.Text = learningRate.ToString();

            if (radioButton1.Checked)
            {
                #region Logical AND
                textBox1.Text = "AND\r\n";
                textBox2.Text = "AND\r\n";
                textBox3.Text = "AND\r\n";
                
                if (per1 == null)
                {
                    per1 = new src.Perceptron(
                        120,
                        learningRate,
                        new Matrix(4, 2, new double[]
                           {
                                0.0, 0.0, 1.0, 1.0,
                                0.0, 1.0, 0.0, 1.0
                           }),
                        new double[] { 0.0, 0.0, 0.0, 1.0 });
                }
                per1.Test(learningRate, functions.ThresholdFunction);
                textBox4.Text = per1.m_NumberOfIterations.ToString(); 
                for (int row = 0; row < per1.m_DataMatrix.m_NumberOfRows; row++)
                {
                    for (int col = 0; col < per1.m_DataMatrix.m_NumberOfColumns; col++)
                    {
                        textBox3.Text +=
                            per1.m_DataMatrix[row + col * per1.m_DataMatrix.m_NumberOfRows] +
                        ((row != per1.m_DataMatrix.m_NumberOfRows - 1)
                        &&
                        (col == per1.m_DataMatrix.m_NumberOfColumns - 1) ? "\r\n" : "");
                    }
                }
                for (int w = 0; w < per1.m_Weights.m_Data.Length; w++)
                {
                    textBox1.Text += per1.m_Weights[w] + 
                        ((w != per1.m_Weights.m_Data.Length-1) ? "\r\n":"");
                }
                for (int o = 0; o < per1.m_Outputs.Length; o++)
                {
                    textBox2.Text += per1.m_Outputs[o] +
                        ((o != per1.m_Outputs.Length - 1) ? "\r\n" : "");
                }
                #endregion
            }
            else if (radioButton2.Checked)
            {
                #region Logical OR
                textBox1.Text = "OR\r\n";
                textBox2.Text = "OR\r\n";
                textBox3.Text = "OR\r\n";

                if (per2 == null)
                {
                    per2 = new src.Perceptron(
                        120,
                        learningRate,
                        new Matrix(4, 2, new double[]
                           {
                               0.0, 0.0, 1.0, 1.0,
                               0.0, 1.0, 0.0, 1.0
                           }),
                        new double[] { 0.0, 1.0, 1.0, 1.0 });
                }
                per2.Test(learningRate, functions.ThresholdFunction);
                textBox4.Text = per2.m_NumberOfIterations.ToString();
                for (int row = 0; row < per2.m_DataMatrix.m_NumberOfRows; row++)
                {
                    for (int col = 0; col < per2.m_DataMatrix.m_NumberOfColumns; col++)
                    {
                        textBox3.Text += 
                            per2.m_DataMatrix[row + col * per2.m_DataMatrix.m_NumberOfRows] +
                        ((row != per2.m_DataMatrix.m_NumberOfRows - 1)
                        && 
                        (col == per2.m_DataMatrix.m_NumberOfColumns-1) ? "\r\n" : "");
                    }
                }
                for (int w = 0; w < per2.m_Weights.m_Data.Length; w++)
                {
                    textBox1.Text += per2.m_Weights[w] +
                        ((w != per2.m_Weights.m_Data.Length - 1) ? "\r\n" : "");
                }
                for (int o = 0; o < per2.m_Outputs.Length; o++)
                {
                    textBox2.Text += per2.m_Outputs[o] +
                        ((o != per2.m_Outputs.Length - 1) ? "\r\n" : "");
                }
                #endregion
            }
            else
            {
                #region Logical XOR
                textBox1.Text = "XOR\r\n";
                textBox2.Text = "XOR\r\n";
                textBox3.Text = "XOR\r\n";

                if (mlp == null)
                {
                    mlp = new MLP(
                        120,
                        learningRate,
                        new Matrix(4, 2, new double[]
                        {
                            0.0, 0.0, 1.0, 1.0,
                            0.0, 1.0, 0.0, 1.0
                        }),
                        new double[] { 0.0, 1.0, 1.0, 0.0 });
                }
                mlp.Test(learningRate, functions.ThresholdFunction);
                textBox4.Text = mlp.m_NumberOfIterations.ToString();
                for (int row = 0; row < mlp.m_DataMatrix.m_NumberOfRows; row++)
                {
                    for (int col = 0; col < mlp.m_DataMatrix.m_NumberOfColumns; col++)
                    {
                        textBox3.Text +=
                            mlp.m_DataMatrix[row + col * mlp.m_DataMatrix.m_NumberOfRows] +
                        ((row != mlp.m_DataMatrix.m_NumberOfRows - 1)
                        &&
                        (col == mlp.m_DataMatrix.m_NumberOfColumns - 1) ? "\r\n" : "");
                    }
                }
                for (int m = 0; m < mlp.m_Weights.Length; m++)
                {
                    textBox1.Text += "\r\nMatrix\r\n";
                    for (int w = 0; w < mlp.m_Weights.Length; w++)
                    {
                        textBox1.Text += mlp.m_Weights[m].ToString() +
                            ((w != mlp.m_Weights.Length - 1) ? "\r\n" : "");
                    }
                }
                for (int o = 0; o < mlp.m_Output.Length; o++)
                {
                    textBox2.Text += mlp.m_Output[o] +
                        ((o != mlp.m_Output.Length - 1) ? "\r\n" : "");
                }
                #endregion
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
