using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebService.Controllers;
using Persistencia.Listas;
using Persistencia.Entity;

namespace PruebasUnitarias
{
    [TestClass]
    public class TestCodeSmell
    {

     

    
        [TestMethod]
        public void TestVerificarMD5HashCorrecto()
        {
            string contraser�a = "contrase�a";
            string hash = "4C882DCB24BCB1BC225391A602FECA7C";
            ApiRegistrarCatadorController catador = new ApiRegistrarCatadorController();
           // bool verificar = catador.VerificarMd5Hash(contraser�a, hash);
            //Assert.IsTrue(verificar);
        }

        [TestMethod]
        public void TestVerificarMD5HashIncorrecto()
        {
            string contraser�a = "contrase�a";
            string hash = "Abc";
            ApiRegistrarCatadorController catador = new ApiRegistrarCatadorController();
            //bool verificar = catador.VerificarMd5Hash(contraser�a, hash);
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
        public void TestRegistrarCataV�lido()
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
