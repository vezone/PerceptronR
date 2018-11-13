using System;
using Perceptron.src.math;


namespace Perceptron.src
{
    public class MLP
    {
        public int m_NumberOfIterations;
        public int m_MaxNumOfIterations;
        public double m_LearningRate;

        public double[] m_Target;
        public double[] m_Output;
        public Matrix m_DataMatrix;
        public Matrix m_Hidden;
        public Matrix[] m_OldWights;
        public Matrix[] m_Weights;

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

            m_OldWights = new Matrix[m_Weights.Length];
            for (int col = 0; col < m_Weights.Length; col++)
            {
                m_OldWights[col] = new Matrix(m_Weights[col]);
                m_OldWights[col].Assign(m_Weights[col]);
            }

        }
        
        public void Learn(Functions.ActivationFunction activation)
        {
            //xi*wi
            Matrix result = new Matrix(m_DataMatrix.m_NumberOfRows,
                m_DataMatrix.m_NumberOfColumns, m_DataMatrix.m_Data);

            m_NumberOfIterations = 0;
            double output = 0.0;
            double gerror = 1.0;
            double[] errors = new double[m_DataMatrix.m_NumberOfColumns];

            while (gerror != 0)
            {
                if (m_NumberOfIterations >= m_MaxNumOfIterations)
                {
                    System.Console.WriteLine($"Number of iterations: {m_NumberOfIterations}");
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
                            m_LearningRate *
                            lErr *
                            m_Hidden[row + col * m_Hidden.m_NumberOfRows];
                    }
                }

                ++m_NumberOfIterations;
            }
            System.Console.WriteLine($"Number of iterations: {m_NumberOfIterations}");
        }
        
        public void Test(double learningRate, Functions.ActivationFunction activation)
        {
            m_LearningRate = learningRate;

            for (int col = 0; col < m_Weights.Length; col++)
            {
                m_Weights[col].Assign(m_OldWights[col]);
            }

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
    
}
