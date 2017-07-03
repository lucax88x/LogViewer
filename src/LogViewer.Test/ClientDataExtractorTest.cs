using System.Diagnostics;
using System.IO;
using System.Reflection;
using FluentAssertions;
using LogViewer.Web.Api;
using Xunit;

namespace LogViewer.Test
{
    public class ClientDataExtractorTest
    {
        private readonly ClientDataExtractor _sut;

        public ClientDataExtractorTest()
        {
            _sut = new ClientDataExtractor();
        }

        [Fact]
        public void should_give_empty_result_with_inexistent_file()
        {
            //ACT
            var result = _sut.Extract("inexistent file");

            //ASSERT
            result.Should().BeEmpty();
        }

        [Fact]
        public void should_skip_hashes()
        {
            //ACT
            var result = _sut.Extract(Map("logs/IISLog_1.log"));

            //ASSERT
            result.Should().HaveCount(1);
        }

        [Fact]
        public void should_parse_correctly_also_almost_empty_lines()
        {
            //ACT
            var result = _sut.Extract(Map("logs/IISLog_2.log"));

            //ASSERT
            result.Should().HaveCount(1);
        }

        [Fact]
        public void should_parse_correctly_and_give_ordered_clientdata()
        {
            //ACT
            var result = _sut.Extract(Map("logs/IISLog_3.log"));

            //ASSERT
            result.Should().NotBeNullOrEmpty();
            result[0].ClientIp.Should().Be("83.150.38.202");
            result[0].NumberOfCalls.Should().Be(3);
            result[1].ClientIp.Should().Be("198.20.69.74");
            result[1].NumberOfCalls.Should().Be(2);
        }

        [Fact]
        public void should_parse_in_decent_time()
        {
            //PREPARE
            var sw = new Stopwatch();
            sw.Start();

            //ACT
            _sut.Extract(Map("logs/IISLog.log"));

            //ASSERT
            sw.Stop();
            sw.ElapsedMilliseconds.Should().BeLessThan(100);
        }

        private string Map(string relativePath)
        {
            var location = typeof(ClientDataExtractorTest).GetTypeInfo().Assembly.Location;
            var dirPath = Path.GetDirectoryName(location);
            return Path.Combine(dirPath, relativePath);
        }
    }
}
