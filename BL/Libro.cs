using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace BL
{
    public class Libro
    {

        //METODOS CON SP

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConexion()))
                {
                    string querySP = "LibroGetAll";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = querySP;
                        cmd.CommandType = CommandType.StoredProcedure;

                        context.Open();

                        DataTable libroTable = new DataTable();

                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

                        sqlDataAdapter.Fill(libroTable);

                        if (libroTable.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in libroTable.Rows)
                            {
                                ML.Libro libro = new ML.Libro();

                                libro.IdLibro = int.Parse(row[0].ToString());
                                libro.Nombre = row[1].ToString();

                                libro.Autor = new ML.Autor();
                                libro.Autor.IdAutor = int.Parse(row[2].ToString());

                                libro.NumeroPaginas = int.Parse(row[3].ToString());
                                libro.FechaPublicacion = row[4].ToString();

                                libro.Editorial = new ML.Editorial();
                                libro.Editorial.IdEditorial = int.Parse(row[5].ToString());

                                libro.Edicion = row[6].ToString();

                                libro.Genero = new ML.Genero();
                                libro.Genero.IdGenero = int.Parse(row[5].ToString());



                                result.Objects.Add(libro);

                            }

                        }

                    }

                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error en la consulta del Libro" + result.Ex;

                throw;
            }

            return result;
        }

        public static ML.Result Add(ML.Libro libro)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConexion()))
                {
                    string query = "LibroAdd";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context; //conexion
                        cmd.CommandText = query; //query
                        cmd.CommandType = CommandType.StoredProcedure;

                        context.Open();

                        SqlParameter[] collection = new SqlParameter[7];

                        collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[0].Value = libro.Nombre;

                        collection[1] = new SqlParameter("IdAutor", SqlDbType.Int);
                        collection[1].Value = libro.Autor.IdAutor;

                        collection[2] = new SqlParameter("NumeroPaginas", SqlDbType.Int);
                        collection[2].Value = libro.NumeroPaginas;

                        collection[3] = new SqlParameter("FechaPublicacion", SqlDbType.VarChar);
                        collection[3].Value = libro.FechaPublicacion;

                        collection[4] = new SqlParameter("IdEditorial", SqlDbType.Int);
                        collection[4].Value = libro.Editorial.IdEditorial;

                        collection[5] = new SqlParameter("Edicion", SqlDbType.VarChar);
                        collection[5].Value = libro.Edicion;

                        collection[6] = new SqlParameter("IdGenero", SqlDbType.Int);
                        collection[6].Value = libro.Genero.IdGenero;




                        cmd.Parameters.AddRange(collection);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected >= 1)
                        {
                            result.Message = "Se agrego el libro correctamente";

                        }
                    }

                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al insertar el libro" + result.Ex;
                Console.ReadKey();

            }
            return result;

        }

        public static ML.Result Upd(ML.Libro libro)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConexion()))
                {
                    string query = "LibroUpdate";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        context.Open();

                        SqlParameter[] collection = new SqlParameter[8];

                        collection[0] = new SqlParameter("IdLibro", SqlDbType.Int);
                        collection[0].Value = libro.IdLibro;

                        collection[1] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[1].Value = libro.Nombre;

                        collection[2] = new SqlParameter("IdAutor", SqlDbType.Int);
                        collection[2].Value = libro.Autor.IdAutor;

                        collection[3] = new SqlParameter("NumeroPaginas", SqlDbType.Int);
                        collection[3].Value = libro.NumeroPaginas;

                        collection[4] = new SqlParameter("FechaPublicacion", SqlDbType.VarChar);
                        collection[4].Value = libro.FechaPublicacion;

                        collection[5] = new SqlParameter("IdEditorial", SqlDbType.Int);
                        collection[5].Value = libro.Editorial.IdEditorial;

                        collection[6] = new SqlParameter("Edicion", SqlDbType.VarChar);
                        collection[6].Value = libro.Edicion;

                        collection[7] = new SqlParameter("IdGenero", SqlDbType.Int);
                        collection[7].Value = libro.Genero.IdGenero;

                        cmd.Parameters.AddRange(collection);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected >= 1)
                        {
                            result.Message = "Se Modifico el Libro correctamente";

                        }
                    }

                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al modificar el Libro" + result.Ex;
                Console.ReadKey();

            }
            return result;

        }

        public static ML.Result GetById(int IdLibro)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConexion()))
                {
                    string querySP = "LibroGetById";


                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = querySP;
                        cmd.CommandType = CommandType.StoredProcedure;

                        context.Open();

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdLibro", SqlDbType.Int);
                        collection[0].Value = IdLibro;

                        cmd.Parameters.AddRange(collection);

                        DataTable libroTable = new DataTable();

                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

                        sqlDataAdapter.Fill(libroTable);

                        if (libroTable.Rows.Count > 0)
                        {
                            DataRow row = libroTable.Rows[0];

                            ML.Libro libro = new ML.Libro();


                            libro.IdLibro = int.Parse(row[0].ToString());
                            libro.Nombre = row[1].ToString();

                            libro.Autor = new ML.Autor();
                            libro.Autor.IdAutor = int.Parse(row[2].ToString());

                            libro.NumeroPaginas = int.Parse(row[3].ToString());
                            libro.FechaPublicacion = row[4].ToString();

                            libro.Editorial = new ML.Editorial();
                            libro.Editorial.IdEditorial = int.Parse(row[5].ToString());

                            libro.Edicion = row[6].ToString();

                            libro.Genero = new ML.Genero();
                            libro.Genero.IdGenero = int.Parse(row[5].ToString());

                            result.Object = libro;

                        }

                    }

                }
                result.Correct = true;


            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error en la consulta" + result.Ex;

                throw;
            }

            return result;
        }

        public static ML.Result Delete(ML.Libro libro)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConexion()))
                {
                    string query = "LibroDelete";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        context.Open();

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdLibro", SqlDbType.TinyInt);
                        collection[0].Value = libro.IdLibro;

                        cmd.Parameters.AddRange(collection);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected >= 1)
                        {
                            result.Message = "Se elimino el Libro correctamente";

                        }
                    }

                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al eliminar el Libro" + result.Ex;
                Console.ReadKey();

            }
            return result;

        }

        //Metodos con Entity Framework

        public static ML.Result AddEF(ML.Libro libro)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.AArteagaPruebaTecnicaEntities context = new DL_EF.AArteagaPruebaTecnicaEntities())
                {
                    int query = context.LibroAdd(libro.Nombre, libro.Autor.IdAutor, libro.NumeroPaginas, libro.FechaPublicacion, libro.Editorial.IdEditorial, libro.Edicion, libro.Genero.IdGenero);

                    if (query > 0)
                    {
                        result.Message = "El libro se agrego con exito";
                    }
                    else
                    {
                        result.Message = "Ocurrio un error al agregar";
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error" + result.Ex;
            }
            return result;
        }

        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.AArteagaPruebaTecnicaEntities context = new DL_EF.AArteagaPruebaTecnicaEntities())
                {
                    //var query = context.LibroGetAll().ToList();
                    var query = context.LibroGetAll().ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var row in query)
                        {
                            ML.Libro libro = new ML.Libro();

                            libro.IdLibro = row.IdLibro;
                            libro.Nombre = row.Nombre;
                            libro.Autor = new ML.Autor();
                            libro.Autor.IdAutor = row.IdAutor.Value;
                            libro.Autor.Nombre = row.AutorNombre;
                            libro.NumeroPaginas = (int)row.NumeroPaginas;
                            libro.FechaPublicacion = row.FechaPublicacion;
                            libro.Editorial = new ML.Editorial();
                            libro.Editorial.IdEditorial = row.IdEditorial.Value;
                            libro.Editorial.Nombre = row.EditorialNombre;
                            libro.Edicion = row.Edicion;
                            libro.Genero = new ML.Genero();
                            libro.Genero.IdGenero = row.IdGenero.Value;
                            libro.Genero.Nombre = row.GeneroNombre;

                            result.Objects.Add(libro);
                        }


                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar la informacion" + result.Ex;
            }
            return result;
        }

        public static ML.Result GetByIdEF(int IdLibro)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.AArteagaPruebaTecnicaEntities context = new DL_EF.AArteagaPruebaTecnicaEntities())
                {
                    var query = context.LibroGetById(IdLibro).FirstOrDefault();

                    if (query != null)
                    {
                        //result.ObjectS = new List<object>();


                        ML.Libro libro = new ML.Libro();

                        libro.IdLibro = query.IdLibro;
                        libro.Nombre = query.Nombre;
                        libro.Autor = new ML.Autor();
                        libro.Autor.IdAutor = query.IdAutor.Value;
                        libro.Autor.Nombre = query.AutorNombre;
                        libro.NumeroPaginas = (int)query.NumeroPaginas;
                        libro.FechaPublicacion = query.FechaPublicacion;
                        libro.Editorial = new ML.Editorial();
                        libro.Editorial.IdEditorial = query.IdEditorial.Value;
                        libro.Editorial.Nombre = query.EditorialNombre;
                        libro.Edicion = query.Edicion;
                        libro.Genero = new ML.Genero();
                        libro.Genero.IdGenero = query.IdGenero.Value;
                        libro.Genero.Nombre = query.GeneroNombre;

                        result.Object = libro;
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar la lista" + result.Ex;
            }
            return result;
        }

        public static ML.Result UpdateEF(ML.Libro libro)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AArteagaPruebaTecnicaEntities contex = new DL_EF.AArteagaPruebaTecnicaEntities())
                {
                    var query = contex.LibroUpdate(libro.IdLibro,libro.Nombre, libro.Autor.IdAutor, libro.NumeroPaginas, libro.FechaPublicacion, libro.Editorial.IdEditorial, libro.Edicion, libro.Genero.IdGenero);


                    if (query > 0)
                    {
                        result.Message = "El Libro se modifico correctamente";
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
            }//(Algún error en el programa)
            return result;




        }

        public static ML.Result DeleteEF(ML.Libro libro)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.AArteagaPruebaTecnicaEntities contex = new DL_EF.AArteagaPruebaTecnicaEntities())
                {
                    int query = contex.LibroDelete(libro.IdLibro);

                    if (query >= 1)
                    {
                        result.Message = "El Libro se elimino correctamente";
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
            }//(Algún error en el programa)
            return result;

        }


    }
}
