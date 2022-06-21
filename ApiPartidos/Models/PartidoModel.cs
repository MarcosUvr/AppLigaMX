﻿using System.Data.SqlClient;

namespace ApiPartidos.Models
{
    public class PartidoModel
    {
        //Variables
        string ConnectionString = "Server=tcp:sqlserverappligamx.database.windows.net,1433;Initial Catalog=sqlappligamx;Persist Security Info=False;User ID=andres;Password=Marcosguapo.123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        //Propiedades
        public int ID { get; set; }
        public string Teams { get; set; }
        public string Picture { get; set; }
        public string Hour { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        //public PartidoModel()
        //{
        //    Teams = String.Empty;
        //    Picture = String.Empty;
        //    Hour = String.Empty;
        //}

        //public PartidoModel(string connectionString)
        //{
        //    ConnectionString = connectionString;
        //    Teams = String.Empty;
        //    Picture = String.Empty;
        //    Hour = String.Empty;
        //}

        //Métodos
        public ApiResponse GetAll()
        {
            List<PartidoModel> list = new List<PartidoModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string tsql = "SELECT * FROM Partido ";
                    using (SqlCommand cmd = new SqlCommand(tsql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new PartidoModel
                                {
                                    ID = (int)reader["IDPartido"],
                                    Teams = reader["Teams"].ToString(),
                                    Picture = reader["Picture"].ToString(),
                                    Hour = reader["Hour"].ToString()    
                                });
                            }
                        }
                    }
                }
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "Los partidos fueron obtenidos correctamente",
                    Result = list
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"Se generó un error al obtener los partidos: {ex.Message}",
                    Result = null
                };
            }

        }

        public ApiResponse Get(int id)
        {
            // Memoria
            //return Products.Find(p => p.ID == id);

            // MySQL
            PartidoModel model = new PartidoModel();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    string tsql = "SELECT * FROM Partido WHERE IDPartido = @ID";
                    using (SqlCommand cmd = new SqlCommand(tsql, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                model = new PartidoModel()
                                {
                                    ID = int.Parse(reader["IDPartido"].ToString()),
                                    Teams = reader["Teams"].ToString(),
                                    Picture = reader["Picture"].ToString(),
                                    Hour = reader["Hour"].ToString()
                                };
                            }
                        }
                    }
                }
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "Los partidos fueron obtenidos correctamente",
                    Result = model
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"Se generó un error al obtener los partidos: {ex.Message}",
                    Result = null
                };
            }
        }

        public ApiResponse Post(PartidoModel model)
        {
            // Memoria
            /*model.ID = Products.Count + 1;
            Products.Add(model);
            return model.ID;*/

            // MySQL
            try
            {
                object newID;
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    string tsql = "INSERT INTO Partido (Teams, Hour, Picture, Latitud, Longitud) " +
                        "VALUES  (@Teams, @Hour, @Picture, @Latitud, @Longitud);"; 
                    using (SqlCommand cmd = new SqlCommand(tsql, con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@Teams", model.Teams);
                        cmd.Parameters.AddWithValue("@Hour", model.Hour);
                        cmd.Parameters.AddWithValue("@Picture", model.Picture);
                        cmd.Parameters.AddWithValue("@Latitud", model.Latitud);
                        cmd.Parameters.AddWithValue("@Longitud", model.Longitud);

                        cmd.ExecuteScalar();
                        // cmd.ExecuteNonQuery();
                        //if (newID != null && newID.ToString().Length > 0)
                        //{
                        //    cmd.Parameters.AddWithValue("@ID", newID);
                        //    return int.Parse(newID.ToString());
                        //}
                    }
                }
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "Los partidos fueron obtenidos correctamente",
                    Result = model
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"Se generó un error al obtener los partidos: {ex.Message}",
                    Result = null
                };
            }
        }

        public int Update(PartidoModel model)
        {
            // Memoria
            /*foreach(ProductModel item in Products)
            {
                if (item.ID == model.ID)
                {
                    item.Price = model.Price;
                    item.Picture = model.Picture;
                    item.Description = model.Description;
                    item.Designer = model.Designer;
                    item.Category = model.Category;
                    item.Model = model.Model;
                    break;
                }
            }*/

            // MySQL
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    string tsql = "UPDATE Partido SET Teams = @Teams, Picture = @Picture, Hour = @Hour " +
                                  "WHERE IDPartido = @ID";
                    using (SqlCommand cmd = new SqlCommand(tsql, con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@Teams", model.Teams);
                        cmd.Parameters.AddWithValue("@Picture", model.Picture);
                        cmd.Parameters.AddWithValue("@Hour", model.Hour);
                        cmd.Parameters.AddWithValue("@ID", model.ID);
                        cmd.ExecuteNonQuery();
                    }
                }
                return model.ID;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Delete(int id)
        {
            // Memoria
            //Products.Remove(Get(id));

            // MySQL
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    string tsql = "DELETE * FROM Partido WHERE IDPartido = @ID";
                    using (SqlCommand cmd = new SqlCommand(tsql, con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                return id;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
