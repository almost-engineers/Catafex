using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using WebService.Controllers;
using WebService.Models;



namespace PruebasUnitarias
{
    [TestClass]
    public class PruebasUnitarias
    {
        private ApiAutenticarController apiAutenticar;
        private ApiRegistrarCataController apiCata;
        private ApiRegistrarCatadorController apiRegistrar;

        public PruebasUnitarias()
        {
            this.apiAutenticar = new ApiAutenticarController();
            this.apiCata = new ApiRegistrarCataController();
            this.apiRegistrar = new ApiRegistrarCatadorController();
        }

        [TestMethod]
        public void autenticarCatadorExitosamente()
        {
            Catador catador = new Catador()
            {
                correo = "jhonathan@catafex.com",
                contrasena = "123456"
            };
            var response = apiAutenticar.validarCamposCatador(catador);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }
        [TestMethod]
        public void autenticarCatadorCorreoFallido()
        {
            Catador catador = new Catador()
            {
                correo = "jhonathan59@catafex.com",
                contrasena = "123456"
            };
            var response = apiAutenticar.validarCamposCatador(catador);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadGateway);
        }
        [TestMethod]
        public void autenticarCatadorContraseñaFallida()
        {
            Catador catador = new Catador()
            {
                correo = "jhonathan@catafex.com",
                contrasena = "1234567"
            };
            var response = apiAutenticar.validarCamposCatador(catador);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadGateway);
        }
        [TestMethod]
        public void registrarCataCorrecta()
        {
            Cata cata = new Cata("CATA-01", 1, 7, 9, 4, 6, 2, 8, 5, 7, 8, "Excelente Cafe");
            var response = apiCata.registrarCata(cata);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        public void registrarCataFallida()
        {
            Cata cata = new Cata("CATA-01", -1, 7, 9, 4, 6, 2, 8, 5, 11, 8, "Excelente Cafe");
            var response = apiCata.registrarCata(cata);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void obtenerCatasPendientes()
        {
            var response = apiCata.consultarCatacion("CATADOR001");
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }
        [TestMethod]
        public void sinCatasPendientes()
        {
            var response = apiCata.consultarCatacion("CATADOR002");
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadGateway);
        }

        [TestMethod]
        public void registrarCatadorExitoso()
        {
            Catador catador = new Catador()
            {
                nombre = "Jhonathan",
                cedula = "9999",
                correo = "jhonathan2@catafex.com",
                codigo = "CATADOR002",
                nivelExp = "EXPERIMENTADO",
                contrasena = "1234"
            };
            var response = apiRegistrar.insertarCatador(catador);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }
        [TestMethod]
        public void registrarCatadorSinNombre()
        {
            Catador catador = new Catador()
            {
                cedula = "9999",
                correo = "jhonathan2@catafex.com",
                codigo = "CATADOR002",
                nivelExp = "EXPERIMENTADO",
                contrasena = "1234"
            };
            var response = apiRegistrar.insertarCatador(catador);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.NotFound);
        }
        [TestMethod]
        public void registrarCatadorCedulaRepetida()
        {
            Catador catador = new Catador()
            {
                cedula = "1234",
                correo = "jhonathan2@catafex.com",
                codigo = "CATADOR002",
                nivelExp = "EXPERIMENTADO",
                contrasena = "1234"
            };
            var response = apiRegistrar.insertarCatador(catador);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadGateway);
        }

        [TestMethod]
        public void registrarCatadorCedulaInvalida()
        {
            Catador catador = new Catador()
            {
                cedula = "1234F",
                correo = "jhonathan2@catafex.com",
                codigo = "CATADOR002",
                nivelExp = "EXPERIMENTADO",
                contrasena = "1234"
            };
            var response = apiRegistrar.insertarCatador(catador);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadGateway);
        }

        [TestMethod]
        public void actualizarCatadorExitoso()
        {
            Catador catador = new Catador()
            {
                cedula = "1234",
                nombre = "jhonathan",
                correo = "jhonathan@catafex.com",
                codigo = "CATADOR001",
                nivelExp = "EXPERIMENTADO",
                contrasena = "12345"
            };
            var response = apiRegistrar.actualizarCatador(catador);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        public void actualizarCatadorFallido()
        { 
            Catador catador = new Catador()
            {
                cedula = "1234",
                correo = "jhonathan@catafex.com",
                codigo = "CATADOR001",
                nivelExp = "EXPERIMENTADO",
                contrasena = "12345"
            };
            var response = apiRegistrar.actualizarCatador(catador);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadGateway);
        }

        [TestMethod]
        public void eliminarCatadorExitoso()
        {
            var response = apiRegistrar.eliminarCatador("1234");
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }
    }
}
