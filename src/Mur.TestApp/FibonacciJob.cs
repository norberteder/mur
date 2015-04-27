namespace Mur.TestApp
{
    public class FibonacciJob : JobBase
    {
        public override void Run()
        {
            var fibOf10 = GetFibonacci(10);
        }

        public int GetFibonacci(int n)
        {
            if ((n == 0) || (n == 1))
            {
                return n;
            }
            return GetFibonacci(n - 1) + GetFibonacci(n - 2);
        }
    }
}
