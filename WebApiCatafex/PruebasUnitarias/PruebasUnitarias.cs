using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        /// <summary>
        /// metodo de prueba para el verificar  la utenticacion de un catador 
        /// cuyos valores son validos en la autenticacion
        /// </summary>
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
        /// <summary>
        ///  Realizar una autenticación con un correo no registrado en el sistema.
        /// </summary>
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
        /// <summary>
        /// metodo test validar realizar una autenticación con una contraseña incorrecta.
        /// </summary>
        [TestMethod]
        public void autenticarCatadorContraseñaFallida()
        {
            Catador catador = new Catador()
            {
                correo = "jhonatha3n@catafex.com",
                contrasena = "1234567"
            };
            var response = apiAutenticar.validarCamposCatador(catador);
            Assert.AreEqual(System.Net.HttpStatusCode.BadGateway, response.StatusCode);
        }
        /// <summary>
        /// metodo test para Registrar correctamente los datos de un café para una cata.
        /// </summary>
        [TestMethod]
        public void registrarCataCorrecta()
        {
            Cata cata = new Cata("CATA-01", 1, 7, 9, 4, 6, 2, 8, 5, 7, 8, "Excelente Cafe");
            var response = apiCata.registrarCata(cata);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }

        /// <summary>
        /// metodo test para Registrar los datos de una cata con valores negativos.
        /// </summary>
        [TestMethod]
        public void registrarCataFallida()
        {
            Cata cata = new Cata("CATA-01", -1, 7, 9, 4, 6, 2, 8, 5, 11, 8, "Excelente Cafe");
            var response = apiCata.registrarCata(cata);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// metodo test para Consultar catas pendientes, igual a las catas en la base de datos
        /// </summary>
        [TestMethod]
        public void obtenerCatasPendientes()
        {
            var response = apiCata.consultarCatacion("CATADOR001");
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }

        /// <summary>
        /// metodo test para Consultar catas pendientes, sin catas pendientes, todas ya han sido realizadas
        /// </summary>
        [TestMethod]
        public void sinCatasPendientes()
        {
            var response = apiCata.consultarCatacion("CATADOR002");
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadGateway);
        }

        /// <summary>
        /// metodo test para  registrar un catador valido
        /// </summary>
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

        /// <summary>
        /// metodo test para llenar un catador sin llenar el campo de nombre 
        /// </summary>
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

        /// <summary>
        /// registrar un catador con la cedula ya existente
        /// </summary>
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

        /// <summary>
        /// metodo test para registar un catador con una cedula invalida (que no posea solo numeros)
        /// </summary>
        [TestMethod]
        public void registrarCatadorCedulaInvalida()
        {
            Catador catador = new Catador()
            {
                cedula = "8243F",
                correo = "jhonathan2@catafex.com",
                codigo = "CATADOR002",
                nivelExp = "EXPERIMENTADO",
                contrasena = "1234"
            };
            var response = apiRegistrar.insertarCatador(catador);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadGateway);
        }

        /// <summary>
        /// metodo para probar la actualizacion de datos de un catador 
        /// solo se pueden actualizar el nombre, correo y contraseña, pero se mandan 
        /// los demas datos para probar el metodo(los demas deben coinidir)
        /// </summary>
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
        /// <summary>
        /// metodo test para actualizar en elk cual no se le manda un nombre 
        /// 
        /// </summary>
        [TestMethod]
        public void actualizarCatadorFallido()
        {
            Catador catador = new Catador()
            {
                nombre = "",
                cedula = "1234",
                correo = "jhonathan@catafex.com",
                codigo = "CATADOR001",
                nivelExp = "EXPERIMENTADO",
                contrasena = "0752"
            };
            var response = apiRegistrar.actualizarCatador(catador);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadGateway);
        }

        /// <summary>
        /// metodo test para eliminar un catador con su cedula 
        /// </summary>
        [TestMethod]
        public void eliminarCatadorExitoso()
        {
            var response = apiRegistrar.eliminarCatador("1234");
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }
        /// <summary>
        /// metodo test para eliminar catador con una cedula no existente
        /// </summary>
        [TestMethod]
        public void eliminarCatadorNoRegistrado()
        {
            var response = apiRegistrar.eliminarCatador("9999");
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadGateway);
        }
        /// <summary>
        /// metodo test para eliminar ctador con una cedula  invalida
        /// </summary>
        [TestMethod]
        public void eliminarCatadorcedulaInvalida()
        {
            var response = apiRegistrar.eliminarCatador("123o4p");
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadGateway);
        }

        /// <summary>
        /// metodo test para eliminar un catador con la cedula vacia
        /// </summary>
        [TestMethod]
        public void eliminarCatadorCedulaVacia()
        {
            var response = apiRegistrar.eliminarCatador("");
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadGateway);
        }
        /// <summary>
        /// metodo test para realizar una autenticación dejando los campos en blanco.
        /// </summary>
        [TestMethod]
        public void AutenticarCatadorDatosBlanco()
        {
            ApiAutenticarController autenticar = new ApiAutenticarController();
            Catador catador = new Catador() { correo = "", contrasena = "" };
            Assert.IsNull(autenticar.ValidarCatador(catador.correo, catador.contrasena));
        }

    }
}
