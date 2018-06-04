using static BenchmarkDotNet.Running.BenchmarkRunner;
using static Benchmarks.ReadmeWriter;

namespace Benchmarks
{
    public static class Program
    {
        public static void Main(string[] args = default)
        {
            Run<Tests>();
            WriteReadme();
        }
    }
}
