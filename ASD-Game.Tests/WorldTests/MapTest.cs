using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ASD_project.DatabaseHandler.Services;
using ASD_project.World;
using ASD_project.World.Models;
using ASD_project.World.Models.Characters;
using ASD_project.World.Models.Interfaces;
using ASD_project.World.Models.TerrainTiles;
using NUnit.Framework;
using Moq;
using Range = Moq.Range;

namespace ASD_Game.Tests.WorldTests
{
    
    [ExcludeFromCodeCoverage]  
    [TestFixture]
    public class MapTest
    {
        //Declaration and initialisation of constant variables
 
        //Declaration of variables
        private int _chunkSize;
        private List<Character> _characterList;
        private Player _character1;
        private Player _character2;
        private IList<Chunk> _chunks;
        private Map _sut;
        
        //Declaration of mocks
        private INoiseMapGenerator _noiseMapGeneratorMockObject;
        private IDatabaseService<Chunk> _databaseServiceMockObject;
        private Mock<INoiseMapGenerator> _noiseMapGeneratorMock;
        private Mock<IDatabaseService<Chunk>> _databaseServiceMock;


        [SetUp]
        public void Setup()
        {
            //Initialisation of variables
            _chunkSize = 2;
            
            var map1 = new ITile[] {new GrassTile(1,1), new GrassTile(1,2), new GrassTile(1,3), new GrassTile(1,4)};
            var map2 = new ITile[] {new StreetTile(1,1), new StreetTile(1,2), new StreetTile(1,3), new StreetTile(1,4)};
            var map3 = new ITile[] {new WaterTile(1,1), new WaterTile(1,2), new WaterTile(1,3), new WaterTile(1,4)};
            var map4 = new ITile[] {new DirtTile(1,1), new DirtTile(1,2), new DirtTile(1,3), new DirtTile(1,4)};
            var map5 = new ITile[] {new DirtTile(1,1), new DirtTile(1,2), new DirtTile(1,3), new DirtTile(1,4)};
            var chunk1 = new Chunk(0, 0, map1, _chunkSize, 0);
            var chunk2 = new Chunk(-1, 0, map2, _chunkSize, 0);
            var chunk3 = new Chunk(0, -1, map3, _chunkSize, 0);
            var chunk4 = new Chunk(-1, -1, map4, _chunkSize, 0);
            _chunks = new List<Chunk>() {chunk1, chunk2, chunk3, chunk4} ;

            //Initialisation of mocks
            _noiseMapGeneratorMock = new Mock<INoiseMapGenerator>();
            _noiseMapGeneratorMock.Setup(noiseMapGenerator => noiseMapGenerator.GenerateChunk(0,0, 2)).Returns(chunk1).Verifiable();
            _noiseMapGeneratorMock.Setup(noiseMapGenerator => noiseMapGenerator.GenerateChunk(-1,0, 2)).Returns(chunk2).Verifiable();
            _noiseMapGeneratorMock.Setup(noiseMapGenerator => noiseMapGenerator.GenerateChunk(0,-1, 2)).Returns(chunk3).Verifiable();
            _noiseMapGeneratorMock.Setup(noiseMapGenerator => noiseMapGenerator.GenerateChunk(-1,-1, 2)).Returns(chunk4).Verifiable();
            _noiseMapGeneratorMock.Setup(noiseMapGenerator => noiseMapGenerator.GenerateChunk(It.IsAny<int>(),It.IsAny<int>(), 2))
                .Returns((int x, int y, int size) => new Chunk(x, y, map5, _chunkSize, 0)).Verifiable();
            _noiseMapGeneratorMockObject = _noiseMapGeneratorMock.Object;
            
            _databaseServiceMock = new Mock<IDatabaseService<Chunk>>();
            _databaseServiceMock.Setup(databaseService => databaseService.CreateAsync(It.IsAny<Chunk>())).Verifiable();
            _databaseServiceMock.Setup(databaseService => databaseService.GetAllAsync()).ReturnsAsync(_chunks);
            _databaseServiceMock.Setup(databaseService => databaseService.DeleteAllAsync()).Verifiable();
            _databaseServiceMockObject = _databaseServiceMock.Object;

            _character1 = new Player("naam1", 0, 0, CharacterSymbol.FRIENDLY_PLAYER, "a");
            _character2 = new Player("naam2", 0, 0, CharacterSymbol.FRIENDLY_PLAYER, "b");
            
            _characterList = new List<Character>();
            _characterList.Add(_character1);
            _characterList.Add(_character2);
            
            _sut = new Map(_noiseMapGeneratorMockObject, _chunkSize, _databaseServiceMockObject, _chunks);
        }
        
        [Test]
        public void Test_MapConstructor_DoesntThrowException() 
        {
            //Arrange ---------
            //Act ---------
            //Assert ---------
            Assert.DoesNotThrow(() =>
            {
                var map = new Map(_noiseMapGeneratorMockObject,21, _databaseServiceMockObject, _chunks);
            });
        }
        
        [Test]
        public void Test_GetMapAroundCharacter_DoesntThrowException() 
        {
            //Arrange ---------
            //Act ---------
            //Assert ---------
            Assert.DoesNotThrow(() =>
            {
                _sut.GetCharArrayMapAroundCharacter(_character1, 1, _characterList);
            });
        }
        
        [Test]
        public void Test_GetMapAroundCharacter_DisplaysRightSize() 
        {
            //Arrange ---------
            //Act ---------
            _sut.GetCharArrayMapAroundCharacter(_character1,2, _characterList);
            //Assert ---------
            // _consolePrinterMock.Verify(consolePrinterMock => consolePrinterMock.PrintText(It.IsAny<string>()), Times.Exactly(25));

        }
        
        [Test]
        public void Test_GetMapAroundCharacter_DoesntLoadTooBigArea() 
        {
            //Arrange ---------
            var viewDistance = 2;
            var maxLoadingLimit = (int)(Math.Pow(viewDistance, 4)  * _chunkSize / _chunkSize * 4);
            //Act ---------
            _sut.GetCharArrayMapAroundCharacter(_character1,viewDistance, _characterList);
            //Assert ---------
            _databaseServiceMock.Verify(databaseService => databaseService.CreateAsync(It.IsAny<Chunk>()), Times.Between(0, maxLoadingLimit, Range.Inclusive));
        }
        
        [Test]
        public void Test_DeleteMap_PassesCommandThrough() 
        {
            //Arrange ---------
            _sut.GetCharArrayMapAroundCharacter(_character1,1, _characterList);
            //Act ---------
            _sut.DeleteMap();
            //Assert ---------
            _databaseServiceMock.Verify( databaseService => databaseService.DeleteAllAsync(), Times.Once);
        }
        
        [Test]
        public void Test_MapConstructor_ThrowsWhenGivenNegativeChunkSize() 
        {
            //Arrange ---------
            //Act ---------
            //Assert ---------
            Assert.Throws<InvalidOperationException>(() =>
            {
                // var map = new Map(_noiseMapGeneratorMockObject,-21, _consolePrinterMockObject, _databaseServiceMockObject, 0, _chunks);
            });
        }
        
        [Test]
        public void Test_GetMapAroundCharacter_ThrowsWhenGivenNegativeDisplaySize() 
        {
            //Arrange ---------
            //Act ---------
            //Assert ---------
            Assert.Throws<InvalidOperationException>(() =>
            {
                _sut.GetCharArrayMapAroundCharacter(_character1,-1, _characterList);
            });
        }
        
        [Test]
        public void Test_GetMapAroundCharacter_UsesChunksIfTheyAreFoundInDatabase() 
        {
            //Arrange ---------
            //Act ---------
            _sut.GetCharArrayMapAroundCharacter(_character1,2, _characterList);
            //Assert ---------
            // _consolePrinterMock.Verify( consolePrinter => consolePrinter.PrintText("  " + _chunks[0].Map[0].Symbol), Times.AtLeast(1));
            // _consolePrinterMock.Verify( consolePrinter => consolePrinter.PrintText("  " + _chunks[1].Map[0].Symbol), Times.AtLeast(1));
            // _consolePrinterMock.Verify( consolePrinter => consolePrinter.PrintText("  " + _chunks[2].Map[0].Symbol), Times.AtLeast(1));
            // _consolePrinterMock.Verify( consolePrinter => consolePrinter.PrintText("  " + _chunks[3].Map[0].Symbol), Times.AtLeast(1));
        }
    }
}