using Microsoft.VisualStudio.TestTools.UnitTesting;
using Inscripciones.BLL;
using System;
using System.Collections.Generic;
using System.Text;
using Inscripciones.Models;

namespace Inscripciones.BLL.Tests
{
    [TestClass()]
    public class PrestamosBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            Personas prePersona = PersonasBLL.Buscar(1);

            Prestamos prestamo = new Prestamos();
            prestamo.PrestamoId = 0;
            prestamo.Fecha = DateTime.Now;
            prestamo.PersonaId = 1;
            prestamo.Concepto = "Primer prestamo";
            prestamo.Monto = 0.0m;
            prestamo.Balance = 2600.0m;

            PrestamosBLL.Guardar(prestamo);

            Personas postPersona = PersonasBLL.Buscar(1);

            prestamo = new Prestamos();
            prestamo.PrestamoId = 0;
            prestamo.Fecha = DateTime.Now;
            prestamo.PersonaId = 1;
            prestamo.Concepto = "Segundo prestamo";
            prestamo.Monto = 0.0m;
            prestamo.Balance = 2600.0m;

            PrestamosBLL.Guardar(prestamo);

            postPersona = PersonasBLL.Buscar(1);

            Assert.AreEqual(prePersona.Balance + 5200, postPersona.Balance);
        }

        [TestMethod()]
        public void EliminarTest()
        {
            Personas Prepersona = PersonasBLL.Buscar(1);

            PrestamosBLL.Eliminar(1);

            Personas PostPersona = PersonasBLL.Buscar(1);

            Assert.AreEqual(Prepersona.Balance - 2600, PostPersona.Balance);
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Prestamos prestamo = PrestamosBLL.Buscar(2);

            Assert.IsTrue(prestamo != null);
        }

        [TestMethod()]
        public void ExisteTest()
        {
            bool paso = PrestamosBLL.Existe(2);

            Assert.IsTrue(paso);
        }

        [TestMethod()]
        public void GetListTest()
        {
            List<Prestamos> listadoPrestamos = PrestamosBLL.GetList(p => true);

            Assert.IsTrue(listadoPrestamos != null);
        }
    }
}