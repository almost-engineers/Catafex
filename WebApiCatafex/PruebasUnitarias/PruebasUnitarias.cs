using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Persistencia;
using Persistencia.Entity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using WebService.Controllers;
using WebService.Models;

namespace PruebasUnitarias
{
    [TestClass]
    public class PruebasUnitarias
    {
        private ApiAutenticarController apiAutenticar;
        public PruebasUnitarias()
        {
            this.apiAutenticar = new ApiAutenticarController();
        }

        [TestMethod]
        public void autenticarCatadorExitosamente()
        {
            Catador catador = new Catador()
            {
                correo = "jhonathan@catafex.com",
                contrasena = "123456"
            };
            Catador catadorRespuesta = new Catador()
            {
                codigo = "CATADOR001",
                nivelExp = "EXPERIMENTADO",
                cedula = "1234",
                correo = "jhonathan@catafex.com",
                nombre = "Jhonathan",
                contrasena = "e10adc3949ba59abbe56e057f20f883e"
            };
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(catadorRespuesta));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            Assert.AreEqual(response.TryGetContentValue<Catador>(out catadorRespuesta), apiAutenticar.validarCamposCatador(catador));
        }
    }
}
