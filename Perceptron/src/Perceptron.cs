using System;
using Perceptron.src.math;


namespace Perceptron.src
{
    public class Perceptron
    {
        double m_LearningRate;

        public int m_MaxNumOfIterations;
        public Matrix m_DataMatrix;
        public double[] m_TargetVector;
        public Matrix m_Weights;
        public double[] m_Outputs;

        public Perceptron(
            int maxIter,
            double learningRate,
            Matrix dataMatrix,
            double[] targetVector)
        {
            m_MaxNumOfIterations = maxIter;
            m_LearningRate = learningRate;
            m_DataMatrix = new Matrix(dataMatrix);
            m_TargetVector = new double[targetVector.Length];
            System.Array.Copy(targetVector, m_TargetVector, targetVector.Length);
            m_Weights = new Matrix(dataMatrix.m_NumberOfRows, dataMatrix.m_NumberOfColumns);

            Random rand = new Random();

            for (int row = 0; row < m_Weights.m_NumberOfRows; row++)
            {
                for (int col = 0; col < m_Weights.m_NumberOfColumns; col++)
                {
                    m_Weights[row + col* m_Weights.m_NumberOfRows] = rand.NextDouble() / 4;
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
                    output = 0.0;
                    for (int col = 0; col < (m_DataMatrix.m_NumberOfColumns); col++)
                    {
                        output += 
                            m_DataMatrix[row + col * m_DataMatrix.m_NumberOfRows]
                            * m_Weights[row + col*m_Weights.m_NumberOfRows];
                    }

                    output = activation(output);
                    double error = m_TargetVector[row] - output;

                    if (error != 0)
                    {
                        gerror += error;

                        for (int el = 0; el < m_Weights.m_NumberOfColumns; el++)
                        {
                            m_Weights[row + el * m_Weights.m_NumberOfRows] +=
                                m_LearningRate * error * 
                                m_Weights[row + el * m_Weights.m_NumberOfRows];
                        }
                    }

                }
                
                ++iterations;
            }
            System.Console.WriteLine($"Number of iterations: {iterations}");
        }
        
        public void Test(Functions.ActivationFunction activation)
        {
            Learn(activation);
            m_Outputs = new double[m_DataMatrix.m_NumberOfRows];
            double output = 0.0;
            for (int pat = 0; pat < m_DataMatrix.m_NumberOfRows; pat++)
            {
                for (int el = 0; el < (m_DataMatrix.m_NumberOfColumns); el++)
                {
                    output += m_DataMatrix[pat + el * m_DataMatrix.m_NumberOfRows]
                        * m_Weights[pat + el * m_Weights.m_NumberOfRows];
                }
                output = activation(output);
                m_Outputs[pat] = output;
            }

        }

    }
}
