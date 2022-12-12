﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Genero
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AArteagaPruebaTecnicaEntities context = new DL_EF.AArteagaPruebaTecnicaEntities())
                {
                    var query = context.GeneroGetAll().ToList();
                    result.Objects = new List<object>(); //Se inicializa la lista
                    if (query != null)
                    {
                        foreach (var objeto in query)
                        {
                            ML.Genero genero = new ML.Genero(); //tiene que ir inicializado dentro  del foreach para que los guarde en un nuevo obj rol

                            genero.IdGenero = objeto.IdGenero;
                            genero.Nombre = objeto.Nombre;

                            result.Objects.Add(genero);
                        }
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al cargar la información" + result.Ex;
                throw;
            }


            return result;

        }
    }
}
