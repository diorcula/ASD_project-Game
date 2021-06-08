using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ASD_Game.ActionHandling.DTO;
using ASD_Game.Items;
using ASD_Game.Items.Services;
using ASD_Game.World;
using ASD_Game.World.Models.HazardousTiles;
using ASD_Game.World.Models.Interfaces;
using ASD_Game.World.Models.TerrainTiles;
using Moq;
using NUnit.Framework;

namespace ASD_Game.Tests.WorldTests
{
    [ExcludeFromCodeCoverage]  
    [TestFixture]
    public class NoiseMapGeneratorTest
    {
        //Declaration and initialisation of constant variables
 
        //Declaration of variables
        private NoiseMapGenerator _sut;
        private int _coordinateX;
        private int _coordinateY;
        //Declaration of mocks
        private Mock<IFastNoise> _mockedNoise;
        private IFastNoise _mockedNoiseObject;
        private Mock<IItemService> _mockedItemService;
        private IItemService _mockedItemServiceobject;

        [SetUp]
        public void Setup()
        {
            //Initialisation of variables
            
            //Initialisation of mocks
            _mockedNoise = new Mock<IFastNoise>();
            _mockedNoiseObject = _mockedNoise.Object;
            _mockedItemService = new Mock<IItemService>();
            _mockedItemServiceobject = _mockedItemService.Object;
            
            _sut = new NoiseMapGenerator(0, _mockedItemServiceobject, null);
            _sut.SetNoise(_mockedNoiseObject);
            
            _mockedItemService
                .Setup(mock => mock.GenerateItemFromNoise(It.IsAny<float>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(null as Item);
        }

        [Test]
        public void Test_Function_GetWaterTileFromNoise() 
        {
            //Arrange ---------
            _coordinateX = 2;
            _coordinateY = 3;
            float _noise = (float) -0.81;

            //Act ---------
            var result = _sut.GetTileFromNoise(_noise, _coordinateX, _coordinateY);
            
            //Assert ---------
            Assert.IsInstanceOf<WaterTile>(result);
        }
        
        [Test]
        public void Test_Function_GetDirtTileFromNoise() 
        {
            //Arrange ---------
            _coordinateX = 2;
            _coordinateY = 3;
            float _noise = (float) -0.41;

            //Act ---------
            var result = _sut.GetTileFromNoise(_noise, _coordinateX, _coordinateY);
            
            //Assert ---------
            Assert.IsInstanceOf<DirtTile>(result);
        }
        
        [Test]
        public void Test_Function_GetGrassTileFromNoise() 
        {
            //Arrange ---------
            _coordinateX = 2;
            _coordinateY = 3;
            float _noise = (float) 0.1;

            //Act ---------
            var result = _sut.GetTileFromNoise(_noise, _coordinateX, _coordinateY);
            
            //Assert ---------
            Assert.IsInstanceOf<GrassTile>(result);
        }
        
        [Test]
        public void Test_Function_GetSpikeTileFromNoise() 
        {
            //Arrange ---------
            _coordinateX = 2;
            _coordinateY = 3;
            float _noise = (float) 0.24;

            //Act ---------
            var result = _sut.GetTileFromNoise(_noise, _coordinateX, _coordinateY);
            
            //Assert ---------
            Assert.IsInstanceOf<SpikeTile>(result);
        }
        
        [Test]
        public void Test_Function_GetStreetTileFromNoise() 
        {
            //Arrange ---------
            _coordinateX = 2;
            _coordinateY = 3;
            float _noise = (float) 0.44;

            //Act ---------
            var result = _sut.GetTileFromNoise(_noise, _coordinateX, _coordinateY);
            
            //Assert ---------
            Assert.IsInstanceOf<StreetTile>(result);
        }
        
        
        [Test]
        public void Test_Function_GetGasTileFromNoise() 
        {
            //Arrange ---------
            _coordinateX = 2;
            _coordinateY = 3;
            float _noise = (float) 1;

            //Act ---------
            var result = _sut.GetTileFromNoise(_noise, _coordinateX, _coordinateY);
            
            //Assert ---------
            Assert.IsInstanceOf<GasTile>(result);
        }
                
        [Test]
        public void Test_Function_GetTileFromNoiseOutlier() 
        {
            //Arrange ---------
            _coordinateX = 2;
            _coordinateY = 3;
            float _noise = (float) 0;

            //Act ---------
            var result = _sut.GetTileFromNoise(_noise, _coordinateX, _coordinateY);
            
            //Assert ---------
            Assert.IsInstanceOf<GrassTile>(result);
        }
                
        [Test]
        public void Test_Function_GenerateCorrectMapSizeChunk() 
        {
            //Arrange ---------
            int chunkrowsize = 3;
            _mockedNoise.Setup(noise => noise.GetNoise(_coordinateX, _coordinateY)).Returns(0).Verifiable();

            //Act ---------
            var result = _sut.GenerateChunk(_coordinateX, _coordinateY, chunkrowsize);
            
            //Assert ---------
            Assert.AreEqual(chunkrowsize * chunkrowsize,result.Map.Length);
        }
        
        [Test]
        public void Test_Function_GenerateCorrectXCoordinatesChunk() 
        {
            //Arrange ---------
            int chunkrowsize = 3;
            _mockedNoise.Setup(noise => noise.GetNoise(_coordinateX, _coordinateY)).Returns(0).Verifiable();
            
            //Act ---------
            var result = _sut.GenerateChunk(_coordinateX, _coordinateY, chunkrowsize);
            
            //Assert ---------
            Assert.AreEqual(_coordinateX, result.X);
        }
        
        [Test]
        public void Test_Function_GenerateCorrectYCoordinatesChunk() 
        {
            //Arrange ---------
            int chunkrowsize = 3;
            _mockedNoise.Setup(noise => noise.GetNoise(_coordinateX, _coordinateY)).Returns(0).Verifiable();
            
            //Act ---------
            var result = _sut.GenerateChunk(_coordinateX, _coordinateY, chunkrowsize);
            
            //Assert ---------
            Assert.AreEqual(_coordinateY, result.Y);
        }
        
        [Test]
        public void Test_Function_GenerateCorrectChunkSizeCoordinatesChunk() 
        {
            //Arrange ---------
            int chunkrowsize = 3;
            _mockedNoise.Setup(noise => noise.GetNoise(_coordinateX, _coordinateY)).Returns(0).Verifiable();
            
            //Act ---------
            var result = _sut.GenerateChunk(_coordinateX, _coordinateY, chunkrowsize);
            
            //Assert ---------
            Assert.AreEqual(chunkrowsize, result.RowSize);
        }

        [Test]
        public void Test_Function_DoesThing() 
        {
            //Arrange ---------
            
            //Act ---------
            
            //Assert ---------
            Assert.That(true);
        }
        
        
        
    }
}