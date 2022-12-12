﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Autor
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AArteagaPruebaTecnicaEntities context = new DL_EF.AArteagaPruebaTecnicaEntities())
                {
                    var query = context.AutorGetAll().ToList();
                    result.Objects = new List<object>(); //Se inicializa la lista
                    if (query != null)
                    {
                        foreach (var objeto in query)
                        {
                            ML.Autor autor = new ML.Autor(); //tiene que ir inicializado dentro  del foreach para que los guarde en un nuevo obj rol

                            autor.IdAutor = objeto.IdAutor;
                            autor.Nombre = objeto.Nombre;

                            result.Objects.Add(autor);
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
