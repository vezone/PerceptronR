
namespace Perceptron.src.math
{
    public class Vector
    {
        double[] m_Data;
        public Vector(int size)
        {
            m_Data = new double[size];
        }

        public Vector(double[] data)
        {
            m_Data = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                m_Data[i] = data[i];
            }
        }

        public Vector(Vector vector)
        {
            m_Data = new double[vector.Length];
            System.Array.Copy(vector.m_Data, m_Data, vector.Length);
        }

        public double this[int id]
        {
            get { return m_Data[id]; }
            set { m_Data[id] = value; }
        }

        public int Length => m_Data.Length;

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < m_Data.Length; i++)
            {
                result += m_Data[i] + " ";
            }
            return result; 
        }

    }
}
