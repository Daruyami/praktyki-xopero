using System;
using System.IO;
using SQLite;

//używam paczki nuget: https://github.com/praeclarum/sqlite-net
namespace SQLiteArticleManager
{
    [Table("articles")]
    public class Article
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public uint Id { get; set; }
        
        [Column("name")]
        public string Name { get; set; }
        
        [Column("category")]
        public string Category { get; set; }
        
        [Column("creation")]
        public DateTime CreationDate { get; set; }
        
        [Column("modification")]
        public DateTime ModificationDate { get; set; }
        
        [Column("content")]
        public string Content { get; set; }
    }
    
    internal static class Program
    {
        private static SQLiteConnection _connection;
        public static bool InitialiseConnection()
        {
            Console.Clear();
            string dbpath = GetUserInput("Please input path to database: ");
            SQLiteConnectionString options;
            if (File.Exists(dbpath))
            {
                Console.Out.WriteLine("Database found, initializing connection...");
                options = new SQLiteConnectionString(dbpath, false);
                _connection = new SQLiteConnection(options); //toadd trycatch
                Console.Out.WriteLine("Connected successfully!");
            }
            else
            {
                string input = GetUserInput("Database not found, would you like to create a new one? y/n");
                if (input[0] == 'y')
                {
                    options = new SQLiteConnectionString(dbpath,
                        SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex | SQLiteOpenFlags.ReadWrite, false);
                    _connection = new SQLiteConnection(options); //toadd trycatch
                    Console.Out.WriteLine("Database successfully created...");
                    _connection.CreateTable<Article>();
                    Console.Out.WriteLine("and articles table successfully created!");
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        private static void _modifyArticle(uint id, string name=null, string category=null, string content=null) 
        {
            var article = _connection.Query<Article>(("SELECT * FROM articles WHERE id="+id));
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
            _connection.Update(article[0]);
            Console.Out.WriteLine("Article successfully modified!");
        }

        private static void _addArticle(string name, string category, DateTime creation, DateTime modification, string content)
        {
            var article = new Article()
            {
                Name = name,
                Category = category,
                CreationDate = creation,
                ModificationDate = modification,
                Content = content
            };
            _connection.Insert(article);
        }
        private static void _deleteArticle(uint id) 
        {
            _connection.Delete<Article>(id);
        }

        public static string GetUserInput(string message=null)
        {
            if (message != null)
                Console.Out.WriteLine(message);
            string input;
            do
                input = Console.ReadLine();
            while (string.IsNullOrEmpty(input));
            return input;
        }

        public static bool InputCommands()
        {
            string input = GetUserInput("\nWaiting for user commands, to list available commands type: ?");
            switch (input)
            {
                case "?":
                    DisplayHelp();
                    break;
                case "list":
                    ListArticles();
                    break;
                case "mod":
                    ModifyArticle();
                    break;
                case "add":
                    AddArticle();
                    break;
                case "del":
                    DeleteArticle();
                    break;
                case "exit":
                    return false;
                default:
                    DisplayHelp();
                    break;
                    
            }
            //(wyświetlanie, dodawanie, edycja, kasowanie)
            return true;
        }

        public static void DisplayHelp()
        {
            Console.Out.WriteLine("Available commands: ");
            Console.Out.WriteLine("?\tdisplays this message");
            Console.Out.WriteLine("list\tlist existing articles");
            Console.Out.WriteLine("mod\tmodify an existing article");
            Console.Out.WriteLine("add\tadds a new article");
            Console.Out.WriteLine("del\tdeletes an existing article");
            Console.Out.WriteLine("exit\texits from the program");
        }
        
        public static void ListArticles()		
        {
            var articles = _connection.Query<Article>("SELECT * FROM articles");
            if (articles.Count == 0)
            {
                Console.Out.WriteLine("No articles found!");
                return;
            }
            Console.Out.WriteLine("\nListing articles: ");
            foreach(var article in articles)
            {
                Console.Out.WriteLine("Name: "+article.Name+" (id: "+article.Id+" )");
                Console.Out.WriteLine("Category: "+article.Category+"\n");
                Console.Out.WriteLine("Modification Date: "+article.ModificationDate);
                Console.Out.WriteLine("Creation Date: "+article.CreationDate);
                Console.Out.WriteLine("\n\t"+article.Content);
                Console.Out.WriteLine("— — — — — — \n");
            }	
        }

        public static void AddArticle()
        {
            string name = GetUserInput("Input articles name:");
            string category = GetUserInput("Input articles category:");
            string content = GetUserInput("Input articles content:");
            _addArticle(name, category, DateTime.Now, DateTime.Now, content);
        }

        public static void ModifyArticle()
        {
            bool notPass = true;
            uint? id = null;
            do
            {
                try
                {
                    id = uint.Parse(GetUserInput("Input articles id:"));
                }
                catch (Exception)
                {
                    Console.Out.WriteLine("Wrong type of input!");
                    continue;
                }

                notPass = false;
            } while (notPass);

            bool edit = true;
            string name = null, category = null, content = null;
            do
            {
                
                string input = GetUserInput("What do you want to modify? \nname/category/content/save/nothing");
                switch (input)
                {
                    case "name":
                        name = GetUserInput("Input the articles new name:");
                        break;
                    case "category":
                        category = GetUserInput("Input the articles new category:");
                        break;
                    case "content":
                        content = GetUserInput("Input the articles new content:");
                        break;
                    case "save":
                        edit = false;
                        break;
                    case "nothing":
                        edit = false;
                        Console.Out.WriteLine("Returning, article not modified!");
                        return;
                }
            } while (edit) ;

            _modifyArticle((uint)id, name, category, content);
        }

        public static void DeleteArticle()
        {
            bool notPass = true;
            uint? id = null;
            do
            {
                try
                {
                    id = uint.Parse(GetUserInput("Input articles id:"));
                }
                catch (Exception)
                {
                    Console.Out.WriteLine("Wrong type of input!");
                    continue;
                }

                notPass = false;
            } while (notPass);
            
            _deleteArticle((uint)id);
        }
        
        public static void Main(string[] args)
        {
            // ReSharper disable once EmptyEmbeddedStatement
            while (InitialiseConnection());

            //do stuff, then close connection and exit
            // ReSharper disable once EmptyEmbeddedStatement
            while(InputCommands());
            
            _connection.Close();
        }
    }
}