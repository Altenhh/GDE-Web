using System;
using System.Configuration;
using RethinkDb.Driver;
using RethinkDb.Driver.Ast;
using RethinkDb.Driver.Net;
using static GDE.Web.Data.Database.Configuration;

namespace GDE.Web.Data.Database
{
    public class QueryFixture : IDisposable
    {
        public string DatabaseName = ConfigurationManager.AppSettings["databaseName"];
        
        public readonly RethinkDB RethinkInstance = RethinkDB.R;
        public Db Database => RethinkInstance.Db(DatabaseName);

        public Connection Connection;

        public QueryFixture()
        {
            ensureConnection();

            try
            {
                createDatabase();
            }
            catch
            {
                // we already have a database, so let's not do anything to it.
            }
        }

        public void Dispose()
        {
            Connection?.Close(false);
            Connection?.Dispose();
        }
        
        public Table CreateTable(string tableName)
        {
            Database.TableCreate(tableName).Run(Connection);
            return Database.Table(tableName);
        }

        public Table ClearTable(string tableName)
        {
            DropTable(tableName);
            return CreateTable(tableName);
        }
        
        public void DropDatabase()
        {
            RethinkInstance.DbDrop(DatabaseName).Run(Connection);
        }
        
        public void DropTable(string tableName)
        {
            Database.TableDrop(tableName).Run(Connection);
        }
        
        /// <summary>DO NOT USE THIS UNLESS IF IT'S ABSOLUTELY NECESSARY.</summary>
        public void Reset()
        {
            Database.Table("Projects").Delete().Run(Connection);
        }
        
        public void RunCommand(ReqlExpr expr) => expr.Run(Connection);

        private void createDatabase()
        {
            RethinkInstance.DbCreate(DatabaseName).Run(Connection);
            Database.Wait_().Run(Connection);
        }

        private void ensureConnection()
        {
            if (Connection == null || !Connection.Open)
                Connection = defaultConnectionBuilder().Connect();
        }

        private Connection.Builder defaultConnectionBuilder() =>
            RethinkInstance.Connection()
               .Hostname(ServerName)
               .Port(ServerPort);
    }
}