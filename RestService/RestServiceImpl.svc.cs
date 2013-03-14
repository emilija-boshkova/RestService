using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;

namespace RestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RestServiceImpl" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select RestServiceImpl.svc or RestServiceImpl.svc.cs at the Solution Explorer and start debugging.
    public class RestServiceImpl : IRestServiceImpl
    {
        //string cString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=\"|DataDirectory|\\Database\\TVSeries.mdf\";Integrated Security=True;Connect Timeout=30";
        string cString = ConfigurationManager.AppSettings["SQLSERVER_CONNECTION_STRING"];
        public string JSONData(string id)
        {
            return "You requested product" + id;
        }

        public List<Serie> getAllSeries()
        {
            List<Serie> list = new List<Serie>();
            SqlConnection connection = new SqlConnection(cString);
            string sqlString = "SELECT * FROM Series ORDER BY title ASC";
            SqlCommand cmd = new SqlCommand(sqlString, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Serie s = new Serie();
                    s.id = Int32.Parse(reader[0].ToString());
                    s.title = reader[1].ToString();
                    s.plot = reader[2].ToString();
                    s.genre = reader[3].ToString();
                    s.imdb = reader[4].ToString();
                    s.image = reader[5].ToString();
                    s.search = reader[6].ToString();

                    list.Add(s);
                }
            }
            catch (Exception e)
            {
                Serie s = new Serie();
                s.id = -1;
                s.title = e.ToString();

                list.Add(s);
                return list;
            }
            finally
            {
                connection.Close();
            }
            return list;
        }

        public List<Serie> SearchSerie(string title)
        {
            List<Serie> list = new List<Serie>();
            string temp = title.Replace("-", "%");
            string search = Regex.Replace(temp, "[^a-zA-Z0-9%]+", "", RegexOptions.Compiled);
            SqlConnection connection = new SqlConnection(cString);
            string sqlString = "SELECT * FROM Series WHERE search LIKE '%" + search + "%'";
            SqlCommand cmd = new SqlCommand(sqlString, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Serie s = new Serie();
                    s.id = Int32.Parse(reader[0].ToString());
                    s.title = reader[1].ToString();
                    s.plot = reader[2].ToString();
                    s.genre = reader[3].ToString();
                    s.imdb = reader[4].ToString();
                    s.image = reader[5].ToString();

                    list.Add(s);
                }
            }
            catch (Exception e)
            {
                Serie s = new Serie();
                s.id = -1;
                s.title = e.ToString();

                list.Add(s);
                return list;
            }
            finally
            {
                connection.Close();
            }
            return list;
        }
    }
}
