using BenchmarkDotNet.Running;
using static Benchmarks.ReadmeWriter;

namespace Benchmarks
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Tests>();
            WriteReadme();
        }
    }
}
