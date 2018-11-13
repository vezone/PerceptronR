using System;

using Perceptron.src.math;


namespace Perceptron.src.ML
{
    public class Layer
    {
        int m_LayerID;
        double[] m_Errors;
        double[] m_Weigths;

        public Layer(
            int layerID,
            double learningRate,
            double[] input,
            int neurNum = 0)
        {
            m_LayerID = layerID;
            
            System.Random rand = new System.Random();
            int neuronsNum = (neurNum == 0) ? input.Length : neurNum;
            Neurons = new Neuron[neuronsNum];
            for (int i = 0; i < (neuronsNum - 1); i+=2)
            {
                //0.0, 0.0 ,
                //0.0, 1.0 ,
                //1.0, 0.0 ,
                //1.0, 1.0

                double[] weigths = new double[] {
                    rand.NextDouble()/4+0.1,
                    rand.NextDouble()/4+0.1
                };

                double[] inp = new double[] {
                    input[i], input[i+1]
                };

                Neurons[i] = new Neuron(learningRate, inp, weigths);
                Neurons[i+1] = new Neuron(learningRate, inp, weigths);
            }

        }

        public int Length => Neurons.Length;

        public double[] Outputs
        {
            get
            {
                double[] outputs = new double[Neurons.Length];
                for (int i = 0; i < Neurons.Length; i++)
                {
                    outputs[i] = Neurons[i].Output;
                }
                return outputs;
            }
        }

        public Neuron[] Neurons { get; }

        public double[] Errors
        {
            get
            {
                if (m_Errors == null)
                {
                    m_Errors = new double[Neurons.Length];
                    for (int neur = 0; neur < Neurons.Length; neur++)
                    {
                        m_Errors[neur] = Neurons[neur].Error;
                    }
                }
                return m_Errors;
            }
        }

        public double GError
        {
            get
            {
                double gError = 0.0;
                for (int err = 0; err < Errors.Length; err++)
                {
                    gError += Errors[err];
                }
                return gError;
            }
        }
        
        public override string ToString()
        {
            string result = "";

            for (int i = 0; i < Neurons.Length; i++)
            {
                result += $"Neuron[{i}]: "+ Neurons[i].Output + "\r\n";
            }

            return result;
        }

    }
}
