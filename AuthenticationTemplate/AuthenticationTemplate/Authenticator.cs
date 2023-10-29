using System;
using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace AuthenticationTemplate
{
    public abstract class Authenticator
    {
        //authenticate n'est pas abstraite car dans toutes les implémentations, on garde la même logique métier
        public bool authenticate(String username, String password)
        {
            return (getLogin(username) != null && getLogin(password) != null);
        }

        public abstract String getLogin(String username);
        public abstract String getPassword(String username);
    }

    public class FileAuthenticator : Authenticator
    {
        private string _filePath;
        public FileAuthenticator(string filePath)
        {
            _filePath = filePath;
        }

        public override string getLogin(string username)
        {
            // Trouve exactement le nom de l'username avant un ; => uniquement chez les usernames
            string regex = "\\b" + username + "\\b(?=;)";

            var Match = Regex.Match(File.ReadAllText(this._filePath), regex);
            
            if(Match.Success) 
            { return Match.Value; }
            return "";
        }

        public override string getPassword(string username)
        {
            // On cherche l'username. On récupère ensuite les caractères après le ; => le password
            string regex = "(?<=" + username + ";).+";
            
            return Regex.Match(File.ReadAllText(this._filePath), regex).Value;
        }
    }

    public class DBAuthenticator : Authenticator
    {
        private string _url;
        SQLiteConnection _connection;

        #region Ouverture de session + manipulation bas niveau
        public DBAuthenticator(string url)
        {
            _url = url;
            _connection = connection();
        }

        private SQLiteConnection connection()
        {
            try
            {
                var connection = new SQLiteConnection(this._url);

                connection.Open();
                initDB(connection);
                
                return connection;
            }
            catch(Exception) 
            {
                SQLiteConnection.CreateFile(this._url);
                return connection();
            }
        }

        private void initDB(SQLiteConnection connection) 
        {
            try
            {
                //Create table user(username, password) and 4 users
                var init = connection.CreateCommand();

                init.CommandText = "CREATE TABLE user(username VARCHAR(20), password VARCHAR(20));\r\nINSERT INTO user VALUES(\"user1\", \"pass1\");\r\nINSERT INTO user VALUES(\"user2\", \"pass2\");\r\nINSERT INTO user VALUES(\"user3\", \"pass3\");\r\nINSERT INTO user VALUES(\"user4\", \"pass4\");";
                var tmp = init.ExecuteNonQuery();
            }
            catch(Exception) {}
        }

        public string executeQuery(string query)
        {
            string dataReturn = "";

            var command = _connection.CreateCommand();
            command.CommandText = query;

            try
            {
                var data = command.ExecuteReader();
                while (data.Read())
                {
                    dataReturn += data.GetString(0);
                }

                return dataReturn;
            }
            catch (Exception) 
            {
                //On a surement reçu aucun résultat
                return "";
            }
        }
        #endregion

        public override string getLogin(string username)
        {
            return executeQuery("SELECT username FROM user WHERE username = '" + username + "';");
        }

        public override string getPassword(string username)
        {
            return executeQuery("SELECT password FROM user WHERE username = '" + username + "';");
        }
    }
}