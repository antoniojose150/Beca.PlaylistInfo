using AutoMapper;
using Beca.PlaylistInfo.API.Controllers;
using Beca.PlaylistInfo.API.Entities;
using Beca.PlaylistInfo.API.Models;
using Beca.PlaylistInfo.API.Profiles;
using Beca.PlaylistInfo.API.Services;
using Castle.Core.Logging;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistInfo.Test
{
    public class CancionesControllerTest
    {
        private readonly ILogger<CancionesController> _logger;
        private readonly CancionesController _playlistController;
        private readonly Mapper _mapper;

        public CancionesControllerTest()
        {
            //Arrage
            var playlistMock = new Mock<IPlaylistRepository>();
            playlistMock
                .Setup(m => m.GetPlaylistsAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<Playlist>
                {new Playlist("linkin park"){Id= 1, Descripcion = "buena lkista"},
                new Playlist("skillet"){Id= 2,Descripcion = "buena play"},
                new Playlist("solence"){Id=3, Descripcion = "canerita"}
                });
            playlistMock
                .Setup(m => m.PlaylistExistAsync(It.IsAny<int>()))
                .ReturnsAsync(true);
            playlistMock
                .Setup(m => m.GetCancionesAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<Cancion>
                {
                    new Cancion("in the end"){Descripcion="best music"},
                    new Cancion("in the end"){Descripcion="best music"}
                });
            playlistMock
                .Setup(m => m.GetCancionAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(
                    new Cancion("in the end"){Descripcion="best music",PlaylistId=2,Playlist = new Playlist("skillet") { Id = 2, Descripcion = "buena play" } }
                 );
            playlistMock
                .Setup(m => m.AddCancionAsync(It.IsAny<int>(), It.IsAny<Cancion>()));
            playlistMock
                .Setup(m => m.SavesChangesAsync())
                .ReturnsAsync(
                    true
                 );
            playlistMock
                .Setup(m => m.DeleteCancion(It.IsAny<Cancion>()));
                


            var mapperConfi = new MapperConfiguration(
                cfg => cfg.AddProfile<CancionProfile>());
            var mapper = new Mapper(mapperConfi);
            _mapper = mapper;
            var logger = Mock.Of<ILogger<CancionesController>>();
            _logger = logger;
            _playlistController = new CancionesController(logger, playlistMock.Object, mapper);

        }

        [Fact]
        public async Task GetCanciones_Test()
        {
            //Arrage

            //Act

            var result = await _playlistController.GetCanciones(1);

            //Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<CancionDto>>>(result);
            var okObject = Assert.IsType<OkObjectResult>(actionResult.Result);
            var dtos = Assert.IsAssignableFrom<IEnumerable<CancionDto>>(okObject.Value);
            Assert.Equal(2, dtos.Count());


        }

        [Fact]
        public async Task GetCancion_Test()
        {
            //Arrage

            //Act

            var result = await _playlistController.GetCancion(2,1);

            //Assert
            var actionResult = Assert.IsType<ActionResult<CancionDto>>(result);
            var okObject = Assert.IsType<OkObjectResult>(actionResult.Result);
            var dtos = Assert.IsAssignableFrom<CancionDto>(okObject.Value);
            Assert.NotNull(dtos);   

        }

        [Fact]
        public async Task CreateCancion_Test()
        {
            //Arrage

            //Act
            CancionCreationDto cancion = new CancionCreationDto()
            {
                Nombre="ok",Descripcion = "prueba"
            } ;

            var result = await _playlistController.CreateCancion(2, cancion);

            //Assert
            var actionResult = Assert.IsType<ActionResult<CancionDto>>(result);
            var okObject = Assert.IsType<CreatedAtRouteResult>(actionResult.Result);

        }

        [Fact]
        public async Task UpgradeCancion_Test()
        {
            //Arrage

            //Act
            CancionUpdateDto cancion = new CancionUpdateDto()
            {
                Nombre = "ok",
                Descripcion = "prueba"
            };

            var result = await _playlistController.UpgrateCancion(2,2, cancion);

            //Assert
            var actionResult = Assert.IsType<NoContentResult>(result);
            

        }

        /*[Fact]
        public async Task PartialUpgradeCancion_Test()
        {
            //Arrage

            JsonPatchDocument<CancionUpdateDto> cancion = new JsonPatchDocument<CancionUpdateDto>();
            cancion.Replace(e => e.Nombre, "nuebo");
            cancion.Replace(e => e.Descripcion, null);


            //Act

            var result = await _playlistController.PartiallyUpgrateCancion(2, 2, cancion);

            //Assert
            var actionResult = Assert.IsType<NoContentResult>(result);


        }*/


        [Fact]
        public async Task DeleteCancion_Test()
        {
            //Arrage

            //Act

            var result = await _playlistController.DeleteCancion(2, 2);

            //Assert
            var actionResult = Assert.IsType<NoContentResult>(result);


        }
    }
}
