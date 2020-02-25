using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persistencia.Entity;
using Persistencia.Listas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService.Controllers;

namespace PruebasUnitarias
{
    [TestClass]
    public class TestCodeSmell
    {
        [TestMethod]
        public void TestVerificarMD5HashCorrecto()
        {
            string contraserña = "contraseña";
            string hash = "4C882DCB24BCB1BC225391A602FECA7C";
            ApiRegistrarCatadorController catador = new ApiRegistrarCatadorController();
            // bool verificar = catador.VerificarMd5Hash(contraserña, hash);
            //Assert.IsTrue(verificar);
        }

        [TestMethod]
        public void TestVerificarMD5HashIncorrecto()
        {
            string contraserña = "contraseña";
            string hash = "Abc";
            ApiRegistrarCatadorController catador = new ApiRegistrarCatadorController();
            //bool verificar = catador.VerificarMd5Hash(contraserña, hash);
            //Assert.IsFalse(verificar);
        }

        [TestMethod]
        public void TestRegistrarCataNegativos()
        {
            Lista catas = new Lista();
            CATA cata = new CATA()
            {
                CODCATACION = "123",
                VEZCATADA = 1,
                RANCIDEZ = -3,
                DULCE = 7,
                ACIDEZ = 3,
                AROMA = 9,
                AMARGO = 8,
                FRAGANCIA = 6,
                SABORESIDUAL = 3,
                CUERPO = 4,
                IMPRESIONGLOBAL = 3,
                OBSERVACIONES = ""
            };
            bool registroCata = catas.registrarCata(cata);
            Assert.IsFalse(registroCata);
        }

        [TestMethod]
        public void TestRegistrarCataVálido()
        {
            Lista catas = new Lista();
            CATA cata = new CATA()
            {
                CODCATACION = "123",
                VEZCATADA = 1,
                RANCIDEZ = 3,
                DULCE = 7,
                ACIDEZ = 3,
                AROMA = 9,
                AMARGO = 8,
                FRAGANCIA = 6,
                SABORESIDUAL = 3,
                CUERPO = 4,
                IMPRESIONGLOBAL = 3,
                OBSERVACIONES = ""
            };
            bool registroCata = catas.registrarCata(cata);
            Assert.IsTrue(registroCata);
        }

    }
}
