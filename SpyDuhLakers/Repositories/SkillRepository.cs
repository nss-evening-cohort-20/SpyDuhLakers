using Microsoft.Data.SqlClient;
using SpyDuhLakers.Models;

namespace SpyDuhLakers.Repositories
{
    public class SkillRepository : BaseRepository, ISkillRepository
    {
        public SkillRepository(IConfiguration configuration) : base(configuration) { }

        public List<Skill> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT \r\nid\r\n,[name]\r\n,userId\r\nfROM Skills";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Skill> skills = new List<Skill>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");

                        int idValue = reader.GetInt32(idColumnPosition);

                        int nameColumnPosition = reader.GetOrdinal("Name");
                        string nameValue = reader.GetString(nameColumnPosition);

                        Skill Skill = new Skill()
                        {
                            Id = idValue,
                            Name = reader.GetString(nameColumnPosition),
                        };

                        skills.Add(Skill);
                    }

                    reader.Close();
                    return skills;
                }
            }
        }

    }
}