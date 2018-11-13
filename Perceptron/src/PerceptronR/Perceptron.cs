using Perceptron.src.math;
using Perceptron.src.PerceptronR;
using System;

namespace PerceptronRosenblad.src.PerceptronR
{
    public class Perceptron
    {
        public int m_MaxNumOfIterations;
        public double[] m_TargetVector;
        
        public Neuron[] m_Neurons;

        public Perceptron(
            int maxIter,
            double learningRate,
            double[] input,
            double[] targetVector,
            Functions.ActivationFunction activation)
        {
            m_MaxNumOfIterations = maxIter;

            m_TargetVector = new double[targetVector.Length];
            System.Array.Copy(targetVector, m_TargetVector, targetVector.Length);

            double[] weights = new double[input.Length];
            System.Random rand = new Random();
            for (int el = 0; el < input.Length; el++)
            {
                weights[el] = rand.NextDouble() / 4;
            }

            m_Neurons = new Neuron[input.Length];
            for (int el = 0; el < input.Length; el++)
            {
                m_Neurons[el] = new Neuron(learningRate, input[el], weights[el], activation);
            }
            
        }

        public void Learn()
        {
            //xi*wi
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
                for (int neur = 0, target = 0; 
                     neur < (m_Neurons.Length - 1); 
                     neur += 2, target++)
                {
                    output = m_Neurons[neur].Output + m_Neurons[neur + 1].Output;
                    double error = m_TargetVector[target] - output;


                    if (error != 0)
                    {
                        m_Neurons[neur].Error = error;
                        m_Neurons[neur + 1].Error = error;

                        m_Neurons[neur].Learn();
                        m_Neurons[neur + 1].Learn();

                        gerror += error;
                    }

                }

                ++iterations;
            }
            System.Console.WriteLine($"Number of iterations: {iterations}");
        }

        public void Test()
        {
            Learn();
            double output = 0.0;
            for (int neur = 0, target = 0;
                     neur < (m_Neurons.Length - 1);
                     neur += 2, target++)
            {
                output = m_Neurons[neur].Output + m_Neurons[neur + 1].Output;
                System.Console.WriteLine(output);
            }

        }
    }

}