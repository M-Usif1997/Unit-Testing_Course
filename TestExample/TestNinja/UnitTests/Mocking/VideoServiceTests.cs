using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;


namespace UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {

        private Mock<IFileReader> _fileReader;
        private Mock<IVideoRepository> _videoRepository;
        private VideoService _videoService;
    

        [SetUp]
        public void SetUp()
        {
            _fileReader = new Mock<IFileReader>();
            _videoRepository = new Mock<IVideoRepository>();
            _videoService = new VideoService(_fileReader.Object, _videoRepository.Object);
            
        }

     

        [Test]
        public void ReadVedioTitle_EmptyFile_ReturnError()
        {
           

            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            //DP via Properties
            //videoService.FileReaderProp = new FakeFileReader();

           var result = _videoService.ReadVideoTitle();


            //State-Based Testing
            Assert.That(result, Does.Contain("error").IgnoreCase);


        }


        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnAnEmptyString()
        {

            _videoRepository.Setup(vr => vr.GetUnprocessedVideos()).Returns(new List<Video>());
        
           var result = _videoService.GetUnprocessedVideosAsCsv();

           Assert.That(result, Is.EqualTo(""));

        }


        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnAStringWithIdOfUnprocessedVideos()
        {

            _videoRepository.Setup(vr => vr.GetUnprocessedVideos()).Returns(new List<Video>()
            {
                new Video() {Id = 1},
                new Video() {Id = 2},
                new Video() {Id = 3}
            });

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));

        }
    }
}
