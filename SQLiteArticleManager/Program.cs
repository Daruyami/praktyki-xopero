using System;
using System.IO;

//używam paczki nuget: https://github.com/praeclarum/sqlite-net do obsługi sqlite
namespace SQLiteArticleManager
{
    
    
    internal static class Program
    {
        private static ArticleDbManager _articleDbManager = new ArticleDbManager();
        
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
            var articles = _articleDbManager.GetArticleList();
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
            _articleDbManager.AddArticle(name, category, DateTime.Now, DateTime.Now, content);
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
                        Console.Out.WriteLine("Returning, article not modified!");
                        return;
                }
            } while (edit) ;

            _articleDbManager.ModifyArticle((uint)id, name, category, content);
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
            
            _articleDbManager.DeleteArticle((uint)id);
        }
        
        public static void Main(string[] args)
        {
            bool notInitialisedConnection = true;
            do
            {
                bool dbFound;
                Console.Clear();
                var dbPath = Program.GetUserInput("Please input path to database: ");
                if (File.Exists(dbPath))
                {
                    Console.Out.WriteLine("Database found, initializing connection...");
                    dbFound = true;

                }
                else
                {
                    string input = Program.GetUserInput("Database not found, would you like to create a new one? y/n");
                    if (input[0] == 'y')
                    {
                        dbFound = false;
                    }
                    else
                    {
                        continue;
                    }
                }
                notInitialisedConnection = _articleDbManager.InitialiseConnection(dbPath, dbFound);
            } while (notInitialisedConnection);
            
            while(InputCommands());
            
            _articleDbManager.CloseConnection();
        }
    }
}