using Perceptron.src.math;


namespace Perceptron.src.ML
{
    public class Neuron
    {
        private double[] m_Input; // (0.0 1.0)
        private double[] m_Weights;
        private double m_LearningRate;
        
        Functions functions = new Functions();

        public Neuron(
            double learningRate,
            double[] input, double[] weights)
        {
            m_LearningRate = learningRate;

            m_Input = new double[2];
            System.Array.Copy(input, m_Input, 2);
            m_Weights = new double[2];
            System.Array.Copy(weights, m_Weights, 2);
        }

        public double Output
        {
            get
            {
                double output = 0.0;
                for (int row = 0; row < 2; row++)
                {
                    output += m_Input[row] * m_Weights[row];
                }
                return output;
            }
        }

        public double[] Weights
        {
            get
            {
                return m_Weights;
            }
            set
            {
                m_Weights = value;
            }
        }

        public double Error { get; set; }

        public void Learn()
        {
            for (int w = 0; w < Weights.Length; w++)
            {
                Weights[w] += m_LearningRate*Error * m_Input[w];//* d_Activation(Output);
            }
        }

    }
}
