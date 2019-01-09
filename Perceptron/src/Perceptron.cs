using System;
using Perceptron.src.math;


namespace Perceptron.src
{
    public class Perceptron
    {
        public int m_NumberOfIterations;
        public int m_MaxNumOfIterations;
        public double m_LearningRate;
        public double[] m_Outputs;
        public double[] m_TargetVector;
        //NOTE: changing public Matrix m_Weights;
        public double[] m_Weights;
        public double[] m_OldWights;
        
        public Matrix m_DataMatrix;

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
            m_Weights = new double[2];
            
            Random rand = new Random();
            for (int index = 0; index < m_Weights.Length; index++)
            {
                m_Weights[index] = 
                    rand.NextDouble() / 4;
            }
            m_OldWights = new double[m_Weights.Length];
            Array.Copy(m_Weights, m_OldWights, m_Weights.Length);
        }

        public void Learn(Functions.ActivationFunction activation)
        {
            //xi*wi
            Matrix result = new Matrix(m_DataMatrix.m_NumberOfRows,
                m_DataMatrix.m_NumberOfColumns, m_DataMatrix.m_Data);
            
            m_NumberOfIterations = 0;
            double output = 0.0;
            double gerror = 1.0;

            while (gerror != 0)
            {
                if (m_NumberOfIterations >= m_MaxNumOfIterations)
                {
                    System.Console.WriteLine($"Number of iterations: {m_NumberOfIterations}");
                    return;
                }

                gerror = 0.0;
                if (m_DataMatrix.m_NumberOfColumns == m_Weights.Length)
                {
                    for (int row = 0; row < m_DataMatrix.m_NumberOfRows; row++)
                    {
                        output = 0.0;

                        for (int col = 0; col < (m_DataMatrix.m_NumberOfColumns); col++)
                        {
                            output +=
                                m_DataMatrix[row + col * m_DataMatrix.m_NumberOfRows]
                                * m_Weights[col];
                        }
                        output = activation(output);
                        double error = m_TargetVector[row] - output;

                        if (error != 0)
                        {
                            gerror += error;

                            for (int index = 0; 
                                index < m_Weights.Length; 
                                index++)
                            {
                                m_Weights[index] +=
                                    m_LearningRate * error
                                    * m_DataMatrix.m_Data[row + index * m_DataMatrix.m_NumberOfRows];
                                ;
                            }
                        }

                    }
                }
                else
                {
                    //
                }
                ++m_NumberOfIterations;
            }
            System.Console.WriteLine($"Number of iterations: {m_NumberOfIterations}");
        }
        
        public void Test(
            double learningRate, 
            Functions.ActivationFunction activation)
        {
            m_LearningRate = learningRate;
            Array.Copy(m_Weights, m_OldWights, m_Weights.Length);
            Learn(activation);

            double output = 0.0;
            m_Outputs = new double[m_DataMatrix.m_NumberOfRows];
            for (int row = 0; row < m_DataMatrix.m_NumberOfRows; row++)
            {
                output = 0.0;
                for (int col = 0; col < (m_DataMatrix.m_NumberOfColumns); col++)
                {
                    output += 
                        m_DataMatrix[row + 
                            col * m_DataMatrix.m_NumberOfRows]
                        * m_Weights[col];
                }
                output = activation(output);
                m_Outputs[row] = output;
            }

        }

    }
}
