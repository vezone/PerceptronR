using Perceptron.src.math;
namespace Perceptron.src.PerceptronR
{
    public class Neuron
    {
        Functions.ActivationFunction d_Activation;

        public Neuron(
            double learningRate,
            double input, double weight,
            Functions.ActivationFunction activation)
        {
            LearningRate = learningRate;
            Input = input;
            Weight = weight;
            d_Activation = activation;
        }

        public double Output
        {
            get
            {
                double output = 0.0;
                output += Input * Weight;
                return d_Activation(output);
            }
        }

        public double Input { get; set; }
        public double Weight { get; set; }
        public double LearningRate { get; set; }
        public double Error { get; set; }

        public void Learn()
        {
            Weight += LearningRate * Error;
        }

    }
}
