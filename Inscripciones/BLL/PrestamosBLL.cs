using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inscripciones.Models;
using Inscripciones.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Inscripciones.BLL
{
    public class PrestamosBLL
    {
        public static bool Guardar(Prestamos prestamo)
        {
            prestamo.Balance -= prestamo.Monto;

            if (!Existe(prestamo.PrestamoId))
                return Insertar(prestamo);
            else
                return Modificar(prestamo);
        }

        private static bool Insertar(Prestamos prestamo)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Prestamos.Add(prestamo);
                paso = contexto.SaveChanges() > 0;

                if (paso)
                {
                    AumentarBalancePersona(prestamo);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        private static void AumentarBalancePersona(Prestamos prestamo)
        {
                var persona = PersonasBLL.Buscar(prestamo.PersonaId);
                persona.Balance += prestamo.Balance;
                PersonasBLL.Guardar(persona);
        }

        private static bool Modificar(Prestamos prestamo)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var monto = prestamo.Monto;

                var anteriorPrestamo = PrestamosBLL.Buscar(prestamo.PrestamoId);
                prestamo.Monto += anteriorPrestamo.Monto;

                contexto.Entry(prestamo).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;

                if (paso)
                {
                    RestarBalancePersona(prestamo.PersonaId,monto);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        private static void RestarBalancePersona(int personaId, decimal monto)
        {
                var persona = PersonasBLL.Buscar(personaId);
                persona.Balance -= monto;
                PersonasBLL.Guardar(persona);
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var prestamo = contexto.Prestamos.Find(id);

                if (prestamo != null)
                {
                    Personas persona = PersonasBLL.Buscar(prestamo.PersonaId);
                    persona.Balance -= prestamo.Balance;
                    PersonasBLL.Guardar(persona);

                    contexto.Prestamos.Remove(prestamo);
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static Prestamos Buscar(int id)
        {
            Prestamos prestamo;
            Contexto contexto = new Contexto();

            try
            {
                prestamo = contexto.Prestamos.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return prestamo;
        }

        public static bool Existe(int id)
        {
            bool encontrado = false;
            Contexto contexto = new Contexto();

            try
            {
                encontrado = contexto.Prestamos.Any(p => p.PrestamoId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }

        public static List<Prestamos> GetList(Expression<Func<Prestamos, bool>> criterio)
        {
            List<Prestamos> lista = new List<Prestamos>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Prestamos.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }
    }
}
