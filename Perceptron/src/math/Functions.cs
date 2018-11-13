using System.Runtime.CompilerServices;


namespace Perceptron.src.math
{
    public class Functions
    {
        public delegate double ActivationFunction(double value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ThresholdFunction(double value)
        {
            return ((value > 0.5) ? 1.0 : 0.0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double LogisticFunction(double value)
        {
            return (1.0 / (1.0 + System.Math.Pow(System.Math.E, (-value))));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double DerivativeLogisticFunction(double value)
        {
            return LogisticFunction(value) * (1- LogisticFunction(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double SemiLinearSaturation(double value)
        {
            if (value <= 0)
                return 0;
            else if (value > 0 && value < 1)
                return value;
            else
                return 1;
        }
    }
}
