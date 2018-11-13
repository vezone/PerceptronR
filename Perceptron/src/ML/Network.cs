using Perceptron.src.math;


namespace Perceptron.src.ML
{
    public class Network
    {
        Layer[] m_LayersChain;
        double[] m_Target;

        public Network(int layersNumber, double learningRate)
        {
            m_LayersChain = new Layer[layersNumber];
            double[] input =
                new double[] {
                    0.0, 0.0 ,
                    0.0, 1.0 ,
                    1.0, 0.0 ,
                    1.0, 1.0
                };
            m_Target = new double[] { 0.0, 1.0, 1.0, 1.0 };
            //_Input = new double[] { 0.0,0.0, 0.0,1.0, 1.0,0.0, 1.0,1.0};

            for (int i = 0; i < m_LayersChain.Length; i++)
            {
                if (i == 0)
                {
                    m_LayersChain[i] = new Layer(
                        i, learningRate, 
                        input);
                }
                else if (i == (m_LayersChain.Length - 1))
                {
                    m_LayersChain[i] = new Layer(
                        i, learningRate, 
                        m_LayersChain[i - 1].Outputs, neurNum:4);
                }
                else
                {
                    m_LayersChain[i] = new Layer(
                        i, learningRate, 
                        m_LayersChain[i - 1].Outputs);
                }
            }

            for (int err = 0; err < m_LayersChain[m_LayersChain.Length - 1].Length; err++)
            {
                m_LayersChain[m_LayersChain.Length - 1].Errors[err] =
                    m_Target[err] - m_LayersChain[m_LayersChain.Length - 1].Neurons[0].Output;
            }

            for (int i = 0; i < m_LayersChain.Length; i++)
            {
                System.Console.WriteLine(m_LayersChain[i].ToString());
            }
        }

        public void CalculateError()
        {
            for (int err = 0; err < m_LayersChain[m_LayersChain.Length - 1].Length; err++)
            {
                m_LayersChain[m_LayersChain.Length - 1].Errors[err] =
                    m_Target[err] - m_LayersChain[m_LayersChain.Length - 1].Neurons[0].Output;
            }

            for (int l = (m_LayersChain.Length - 2); l > 0; l--)
            {
                //Backpropogation
                for (int neur = 0; neur < m_LayersChain[l].Length; neur++)
                {
                    for (int wi = 0; wi < m_LayersChain[l].Neurons[neur].Weights.Length; wi++)
                    {
                        //todo something
                        m_LayersChain[l - 1].Neurons[wi].Error +=
                            m_LayersChain[l].Neurons[neur].Error *
                            m_LayersChain[l].Neurons[neur].Weights[wi];
                    }
                }
            }
        }

        public void Learn(int maxIter, int period)
        {
            int iterations = 0;
            m_LayersChain[m_LayersChain.Length - 1].Neurons[0].Error = 1;
            while (m_LayersChain[m_LayersChain.Length - 1].GError != 0 &&
                   iterations < maxIter)
            {
                CalculateError();

                for (int l = 0; l < m_LayersChain.Length; l++)
                {
                    for (int neur = 0; neur < m_LayersChain[l].Length; neur++)
                    {
                        m_LayersChain[l].Neurons[neur].Learn();
                        m_LayersChain[l].Neurons[neur].Error = 0.0;
                        if (iterations % period == 0)
                        {
                            System.Console.WriteLine("neur output: " +
                            m_LayersChain[l].Neurons[neur].Output);
                            System.Console.WriteLine("neur error: {0}",
                                m_LayersChain[l].Neurons[neur].Error);
                            System.Console.WriteLine("neur weights1: {0}, weights2: {1}",
                                m_LayersChain[l].Neurons[neur].Weights[0],
                                m_LayersChain[l].Neurons[neur].Weights[1]);
                        }
                    }
                }
                ++iterations;

                if (iterations % period == 0)
                {
                    System.Console.WriteLine($"iteration: {iterations}");
                    for (int n = 0; n < m_LayersChain[m_LayersChain.Length - 1].Length; n++)
                    {
                        System.Console.WriteLine(
                           $"error: {m_LayersChain[m_LayersChain.Length - 1].Neurons[n].Error}");
                    }
                }

                if (iterations > maxIter)
                {
                    System.Console.WriteLine("Error: " + 
                        m_LayersChain[m_LayersChain.Length - 1].Neurons[0].Error);
                    for (int n = 0; n < m_LayersChain[m_LayersChain.Length - 1].Length; n++)
                    {
                        System.Console.WriteLine(
                           $"error: {m_LayersChain[m_LayersChain.Length - 1].Neurons[n].Error}");
                    }
                }
            }

            for (int n = 0; n < m_LayersChain[m_LayersChain.Length - 1].Length; n++)
            {
                System.Console.WriteLine("Output: " +
                        m_LayersChain[m_LayersChain.Length - 1].Neurons[n].Output);
            }

        }
        
    }
}
