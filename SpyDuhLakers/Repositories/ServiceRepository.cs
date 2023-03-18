using Microsoft.Data.SqlClient;
using SpyDuhLakers.Models;

namespace SpyDuhLakers.Repositories
{
    public class ServiceRepository : BaseRepository, IServiceRepository
    {
        public ServiceRepository(IConfiguration configuration) : base(configuration) { }
        public List<Service> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select
                                        id
                                        ,[name]
                                        ,userId
                                        From [Services]";

                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Service> services = new List<Service>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("id");

                        int idValue = reader.GetInt32(idColumnPosition);
                        int nameColumnPosition = reader.GetOrdinal("Name");
                        string nameValue = reader.GetString(nameColumnPosition);

                        Service service = new Service()
                        {
                            Id = idValue,
                            Name = nameValue
                        };
                        services.Add(service);
                    }
                    reader.Close();

                    return services;

                }
            }
        }


        public Service GetById(int Id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id
                                               ,[name]
                                               ,userId 
                                                FROM [Services] 
                                                WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", Id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Service service = null;
                    if (reader.Read())
                    {
                        service = new Service()
                        {
                            Id = Id,
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                        };
                    }
                    reader.Close();
                    return service;
                }

            }
        }


        public void Insert(Service service)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Insert INTO Services(Name) 
                                        OUTPUT INSERTED.Id 
                                        VALUES (@name)";
                    cmd.Parameters.AddWithValue("@name", service.Name);
                    int id = (int)cmd.ExecuteScalar();

                }
                conn.Close();

            }
        }

    }
}

