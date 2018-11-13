using System;

namespace Perceptron.src.math
{
    public class Matrix
    {
        public double[] m_Data;
        public int m_NumberOfRows;
        public int m_NumberOfColumns;

        public double this[int id]
        {
            get { return m_Data[id]; }
            set { m_Data[id] = value; }
        }

        public Matrix(string data)
        {
            int index = data.IndexOf('\r'),
                numberOfColumns = 1, numberOfRows = 1;
            for (int i = 0; i < data.Length; i++)
            {
                if (i < index && 
                    data[i] == ' ')
                {
                    ++numberOfColumns;
                }

                if (data[i] == '\r')
                {
                    ++numberOfRows;
                }
            }

            m_NumberOfRows = numberOfRows;
            m_NumberOfColumns = numberOfColumns;
            m_Data = new double[numberOfRows * numberOfColumns];

            data = data.Replace("\r\n", "|");
            string[] splitted = data.Split('|');
            for (int i = 0; i < splitted.Length; i++)
            {
                string[] arr = splitted[i].Split(' ');
                for (int j = 0; j < arr.Length; j++)
                {
                    m_Data[i + j*m_NumberOfRows] = Double.Parse(arr[j]); 
                }
            }
        }

        public Matrix(int numberOfRows, int numberOfColumns)
        {
            m_NumberOfRows = numberOfRows;
            m_NumberOfColumns = numberOfColumns;
            m_Data = new double[numberOfRows*numberOfColumns];
        }

        public Matrix(int numberOfRows, int numberOfColumns, double[] data)
        {
            m_NumberOfRows = numberOfRows;
            m_NumberOfColumns = numberOfColumns;
            m_Data = new double[numberOfRows * numberOfColumns];
            if (data.Length == m_Data.Length)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    m_Data[i] = data[i];
                }
            }
            else
            {
                System.Console.WriteLine("Rows*Columns != data.Length!");
            }
        }

        public Matrix(Matrix matrix)
        {
            m_NumberOfRows = matrix.m_NumberOfRows;
            m_NumberOfColumns = matrix.m_NumberOfColumns;
            m_Data = new double[matrix.m_NumberOfRows * matrix.m_NumberOfColumns];
            for (int i = 0; i < matrix.m_Data.Length; i++)
            {
                m_Data[i] = matrix.m_Data[i];
            }
        }

        public static Matrix operator- (Matrix mat1, Matrix mat2)
        {
            if (mat1.m_NumberOfRows != mat2.m_NumberOfRows &&
                mat1.m_NumberOfColumns != mat2.m_NumberOfColumns)
            {
                System.Console.WriteLine("You can't - matrices because mat1.columns != mat2.rows!");
                return new Matrix(1, 1);
            }

            Matrix result = new Matrix(mat1.m_NumberOfRows, mat1.m_NumberOfColumns);
            for (int el = 0; el < mat1.m_Data.Length; el++)
            {
                result[el] = mat1[el] - mat2[el];
            }

            return result;
        }

        public static Matrix operator- (Matrix mat1, double number)
        {
            Matrix result = new Matrix(mat1.m_NumberOfRows, mat1.m_NumberOfColumns);
            for (int el = 0; el < mat1.m_Data.Length; el++)
            {
                result[el] = mat1[el] - number;
            }

            return result;
        }

        public static Matrix operator+ (Matrix mat1, Matrix mat2)
        {
            if (mat1.m_NumberOfRows != mat2.m_NumberOfRows &&
                mat1.m_NumberOfColumns != mat2.m_NumberOfColumns)
            {
                System.Console.WriteLine("You can't + matrices because mat1.columns != mat2.rows!");
                return mat1;
            }

            Matrix result = new Matrix(mat1.m_NumberOfRows, mat1.m_NumberOfColumns);
            for (int el = 0; el < mat1.m_Data.Length; el++)
            {
                result[el] = mat1[el] + mat2[el];
            }

            return result;
        }

        public static Matrix operator+ (Matrix mat1, double number)
        {
            Matrix result = new Matrix(mat1.m_NumberOfRows, mat1.m_NumberOfColumns);
            for (int el = 0; el < mat1.m_Data.Length; el++)
            {
                result[el] = mat1[el] + number;
            }

            return result;
        }

        public static Matrix operator* (Matrix mat1, Matrix mat2)
        {
            if (mat1.m_NumberOfColumns != mat2.m_NumberOfRows)
            {
                System.Console.WriteLine("You can't * matrices because mat1.columns != mat2.rows!");
                return new Matrix(1, 1);
            }

            Matrix result = new Matrix(mat1.m_NumberOfRows, mat2.m_NumberOfColumns);
            double temp;
            for (int row = 0; row < mat1.m_NumberOfRows; row++)
            {
                for (int col = 0; col < mat2.m_NumberOfColumns; col++)
                {
                    temp = 0.0;
                    for (int e = 0; e < mat1.m_NumberOfColumns; e++)
                    {
                        temp +=
                            mat1.m_Data[row + e * mat1.m_NumberOfRows] *
                            mat2.m_Data[e + col * mat2.m_NumberOfRows];
                    }
                    result.m_Data[col + row * mat2.m_NumberOfColumns] = temp;
                }
            }

            return result;
        }

        public static Matrix operator* (Matrix mat1, double number)
        {
            Matrix result = new Matrix(mat1.m_NumberOfRows, mat1.m_NumberOfColumns);
            for (int el = 0; el < mat1.m_Data.Length; el++)
            {
                mat1.m_Data[el] *= number;
            }
            return result;
        }

        public static bool operator== (Matrix mat1, Matrix mat2)
        {
            if (mat1.m_NumberOfRows != mat2.m_NumberOfRows ||
                mat1.m_NumberOfColumns != mat2.m_NumberOfColumns)
            {
                return false;
            }

            for (int el = 0; el < mat1.m_Data.Length; el++)
            {
                if (mat1.m_Data[el] != mat2.m_Data[el])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator!= (Matrix mat1, Matrix mat2)
        {
            return !(mat1 == mat2);
        }

        public static bool operator== (Matrix mat1, double number)
        {
            for (int el = 0; el < mat1.m_Data.Length; el++)
            {
                if (mat1.m_Data[el] != number)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator!= (Matrix mat1, double number)
        {
            return !(mat1 == number);
        }
        
        public void Assign(double number)
        {
            for (int el = 0; el < m_Data.Length; el++)
            {
                m_Data[el] = number;
            }
        }

        public void Assign(Matrix mat2)
        {
            if (m_NumberOfColumns != mat2.m_NumberOfRows)
            {
                System.Console.WriteLine("You can't multiply matrices because mat1.columns != mat2.rows!");
            }

            for (int el = 0; el < m_Data.Length; el++)
            {
                m_Data[el] = mat2.m_Data[el];
            }
        }

        public void Activation(Functions.ActivationFunction activation)
        {
            for (int el = 0; el < m_Data.Length; el++)
            {
                m_Data[el] = activation(m_Data[el]);
            }
        }

        public double Summary()
        {
            double result = 0.0;
            for (int el = 0; el < m_Data.Length; el++)
            {
                result += m_Data[el];
            }

            return result;
        }

        public override string ToString()
        {
            string result = "";
            for (int row = 0; row < m_NumberOfRows; row++)
            {
                for (int col = 0; col < m_NumberOfColumns; col++)
                {
                    result += m_Data[col + row * m_NumberOfColumns] + "  ";
                }
                result += (row != m_NumberOfRows-1) ? "\r\n" : "";
            }
            return result;
        }

    }
}
