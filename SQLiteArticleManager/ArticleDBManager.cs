using System;
using System.Collections.Generic;
using SQLite;

namespace SQLiteArticleManager
{
    public class ArticleDbManager
    {
        public SQLiteConnection Connection { get; private set; }

        public bool InitialiseConnection(string dbpath, bool exists)
        {
            SQLiteConnectionString options;
            if (exists)
            {
                options = new SQLiteConnectionString(dbpath, false);
                Connection = new SQLiteConnection(options); //toadd trycatch
                Console.Out.WriteLine("Connected successfully!");
            }
            else
            {
                options = new SQLiteConnectionString(dbpath,
                    SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex | SQLiteOpenFlags.ReadWrite, false);
                Connection = new SQLiteConnection(options); //toadd trycatch
                Console.Out.WriteLine("Database successfully created...");
                Connection.CreateTable<Article>();
                Console.Out.WriteLine("and articles table successfully created!");
            }
            return false;
        }

        public void ModifyArticle(uint id, string name=null, string category=null, string content=null) 
        {
            var article = Connection.Query<Article>(("SELECT * FROM articles WHERE id="+id));
            if (article.Count == 0)
            {
                Console.Out.WriteLine("Article not found!");
                return;
            }
            Console.Out.WriteLine("Article found, modifying...");
            if(name != null)
                article[0].Name = name;
            if(category != null)
                article[0].Category = category;
            if(content != null)
                article[0].Content = content;
            article[0].ModificationDate = DateTime.Now;
            Connection.Update(article[0]);
            Console.Out.WriteLine("Article successfully modified!");
        }

        public void AddArticle(string name, string category, DateTime creation, DateTime modification, string content)
        {
            var article = new Article()
            {
                Name = name,
                Category = category,
                CreationDate = creation,
                ModificationDate = modification,
                Content = content
            };
            Connection.Insert(article);
        }
        public void DeleteArticle(uint id) 
        {
            Connection.Delete<Article>(id);
        }

        public List<Article> GetArticleList()
        {
            return Connection.Query<Article>("SELECT * FROM articles");
        }

        public void CloseConnection()
        {
            Connection.Close();
        }

    }
}