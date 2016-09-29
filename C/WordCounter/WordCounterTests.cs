namespace WordCounter
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class WordCounterTests
    {
        private StreamReader PrepareInput(string input)
        {
            return new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(input)));
        }

        [Test]
        public void CountMultipleLines()
        {
            var count = WordCounter.Count(PrepareInput(@"Test test test
tests test:tttt"), "test");
            count.Should().Be(4);
        }

        [Test]
        public void CountOneLiner()
        {
            var count = WordCounter.Count(PrepareInput("Test test test"), "test");
            count.Should().Be(3);
        }

        [Test]
        public void SrtExample()
        {
            var input = PrepareInput(@"
1
00:02:17,440 --> 00:02:20,375
Senator, we're making
our final approach into Coruscant.

2
00:02:20,476 --> 00:02:22,501
Very good, Lieutenant.");

            var count = WordCounter.Count(input, "approach");
            count.Should().Be(1);
        }

        [Test]
        public void CountAllWordsTest()
        {
            using (var sr = new StreamReader(new FileStream("Star.Wars.srt", FileMode.Open, FileAccess.Read)))
            {
                var count = WordCounter.AllWordsCount(sr);
                Console.WriteLine(count);
            }
        }

        [Test]
        public void TimeEstimateTest()
        {
            var words = new Dictionary<string, uint>();
            var sw = new Stopwatch();
            sw.Start();

            for (int t = 0; t < 100; t++)
            {
                using (var sr = new StreamReader(new FileStream("Star.Wars.srt", FileMode.Open, FileAccess.Read)))
                {
                    WordCounter.AnalyzeFile(sr, words);
                }
            }

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
        }

        
    }
}