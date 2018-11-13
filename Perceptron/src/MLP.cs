using System;
using Perceptron.src.math;


namespace Perceptron.src
{
    public class MLP
    {
        double m_LearningRate;

        public int m_MaxNumOfIterations;
        public Matrix m_DataMatrix;
        public double[] m_Target;

        public Matrix m_Hidden;
        public Matrix[] m_Weights;
        public double[] m_Output;

        public MLP(
            int maxIter,
            double learningRate,
            Matrix dataMatrix,
            double[] targetVector)
        {
            m_MaxNumOfIterations = maxIter;
            m_LearningRate = learningRate;
            m_DataMatrix = new Matrix(dataMatrix);
            m_Hidden = new Matrix(dataMatrix);
            m_Target = new double[targetVector.Length];
            System.Array.Copy(targetVector, m_Target, targetVector.Length);

            m_Weights = new Matrix[2];
            for (int i = 0; i < m_Weights.Length; i++)
            {
                m_Weights[i] = new Matrix(dataMatrix.m_NumberOfRows, dataMatrix.m_NumberOfColumns);
            }

            Random rand = new Random();

            for (int i = 0; i < m_Weights.Length; i++)
            {
                for (int row = 0; row < m_Weights[i].m_NumberOfRows; row++)
                {
                    for (int col = 0; col < m_Weights[i].m_NumberOfColumns; col++)
                    {
                        m_Weights[i][row + col * m_Weights[i].m_NumberOfRows] = rand.NextDouble() / 4;
                    }
                }
            }
        }
        
        public void Learn(Functions.ActivationFunction activation)
        {
            //xi*wi
            Matrix result = new Matrix(m_DataMatrix.m_NumberOfRows,
                m_DataMatrix.m_NumberOfColumns, m_DataMatrix.m_Data);

            int iterations = 0;
            double output = 0.0;
            double gerror = 1.0;
            double[] errors = new double[m_DataMatrix.m_NumberOfColumns];

            while (gerror != 0)
            {
                if (iterations >= m_MaxNumOfIterations)
                {
                    System.Console.WriteLine($"Number of iterations: {iterations}");
                    return;
                }

                gerror = 0.0;
                for (int row = 0; row < m_DataMatrix.m_NumberOfRows; row++)
                {
                    for (int col = 0; col < m_DataMatrix.m_NumberOfColumns; col++)
                    {
                        m_Hidden[row + col * m_Hidden.m_NumberOfRows] +=
                            m_DataMatrix[row + col * m_DataMatrix.m_NumberOfRows]
                            * m_Weights[0][row + col * m_Weights[0].m_NumberOfRows];

                        m_Hidden[row + col * m_Hidden.m_NumberOfRows] =
                            activation(m_Hidden[row + col * m_Hidden.m_NumberOfRows]);
                    }

                    output = 0.0;
                    for (int col = 0; col < m_DataMatrix.m_NumberOfColumns; col++)
                    {
                        output +=
                            m_Hidden[row + col * m_DataMatrix.m_NumberOfRows] *
                            m_Weights[1][row + col * m_Weights[1].m_NumberOfRows];
                    }
                    output = activation(output);

                    errors = new double[m_DataMatrix.m_NumberOfColumns];
                    
                    double lErr = m_Target[row] - output;
                    gerror += Math.Abs(lErr);

                    for (int col = 0; col < m_DataMatrix.m_NumberOfColumns; col++)
                        errors[col] = lErr * m_Weights[1][row + col * m_Weights[1].m_NumberOfRows];

                    for (int r = 0; r < m_DataMatrix.m_NumberOfColumns; r++)
                    {
                        for (int col = 0; col < m_DataMatrix.m_NumberOfColumns; col++)
                        {
                            m_Weights[0][r + col * m_Weights[0].m_NumberOfRows] +=
                                m_LearningRate *
                                errors[col] *
                                m_DataMatrix[row + r * m_DataMatrix.m_NumberOfRows];

                        }
                    }

                    for (int col = 0; col < m_DataMatrix.m_NumberOfColumns; col++)
                    {
                        m_Weights[1][row + col * m_Weights[1].m_NumberOfRows] +=
                            0.1 *
                            lErr *
                            m_Hidden[row + col * m_Hidden.m_NumberOfRows];
                    }
                }

                ++iterations;
            }
            System.Console.WriteLine($"Number of iterations: {iterations}");
        }



        public void Test(Functions.ActivationFunction activation)
        {
            Learn(activation);
            double output = 0.0;
            m_Output = new double[m_DataMatrix.m_NumberOfRows];

            for (int row = 0; row < m_DataMatrix.m_NumberOfRows; row++)
            {
                for (int col = 0; col < m_DataMatrix.m_NumberOfColumns; col++)
                {
                    m_Hidden[row + col * m_Hidden.m_NumberOfRows] +=
                        m_DataMatrix[row + col * m_DataMatrix.m_NumberOfRows]
                        * m_Weights[0][row + col * m_Weights[0].m_NumberOfRows];

                    m_Hidden[row + col * m_Hidden.m_NumberOfRows] =
                        activation(m_Hidden[row + col * m_Hidden.m_NumberOfRows]);
                }

                output = 0.0;
                for (int col = 0; col < m_DataMatrix.m_NumberOfColumns; col++)
                {
                    output +=
                        m_Hidden[row + col * m_DataMatrix.m_NumberOfRows] *
                        m_Weights[1][row + col * m_Weights[1].m_NumberOfRows];
                }
                output = activation(output);
                m_Output[row] = output;
            }
        }
    }

    public class Persaceptron
    {

        double[] enters;
        double[] hidden;
        double outer;
        double[][] wEH;
        double[] wHO;
        double[][] patterns = {
            new double[]{0, 0}, 
            new double[]{1, 0}, 
            new double[]{0, 1}, 
            new double[]{1, 1}
        };
        double[] answers = { 0, 1, 1, 0 };

        Persaceptron()
        {
            enters = new double[patterns[0].Length];
            hidden = new double[2];
            wEH = new double[enters.Length][];
            wHO = new double[hidden.Length];

            initWeights();
            study();
            for (int p = 0; p < patterns.Length; p++)
            {
                for (int i = 0; i < enters.Length; i++)
                    enters[i] = patterns[p][i];

                countOuter();
            }

        }

        public void initWeights()
        {
        }

        public void countOuter()
        {
            for (int i = 0; i < hidden.Length; i++)
            {
                hidden[i] = 0;
                for (int j = 0; j < enters.Length; j++)
                {
                    hidden[i] += enters[j] * wEH[j][i];
                }
                if (hidden[i] > 0.5) hidden[i] = 1;
                else hidden[i] = 0;
            }
            outer = 0;
            for (int i = 0; i < hidden.Length; i++)
            {
                outer += hidden[i] * wHO[i];
            }
            if (outer > 0.5) outer = 1;
            else outer = 0;
        }

        public void study()
        {
            double[] err = new double[hidden.Length];
            double gError = 0;
            do
            {
                gError = 0;
                for (int p = 0; p < patterns.Length; p++)
                {
                    for (int i = 0; i < enters.Length; i++)
                        enters[i] = patterns[p][i];

                    countOuter();

                    double lErr = answers[p] - outer;
                    gError += Math.Abs(lErr);

                    for (int i = 0; i < hidden.Length; i++)
                        err[i] = lErr * wHO[i];

                    for (int i = 0; i < enters.Length; i++)
                    {
                        for (int j = 0; j < hidden.Length; j++)
                        {
                            wEH[i][j] += 0.1 * err[j] * enters[i];

                        }
                    }
                    for (int i = 0; i < hidden.Length; i++)
                        wHO[i] += 0.1 * lErr * hidden[i];
                }
            } while (gError != 0);
        }
    }

}
