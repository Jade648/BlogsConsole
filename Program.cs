using System;
using NLog.Web;
using System.IO;
using System.Linq;

namespace BlogsConsole
{
    class Program
    {
             private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        public static object p { get; private set; }

        private static void Main(string[] args, int postId)
        {
            Main(args, postId, p);
        }

        static void Main(string[] args, int postId, object p)
        {

            logger.Info("Program started");

            Console.ForegroundColor = ConsoleColor.Green;

            string option = ""; 
            do {

                Console.WriteLine("1) Display all Blogs");

                Console.WriteLine("2) Add a Blog");

                Console.WriteLine("3) Create a Blog Post");

                Console.WriteLine("4) Display all Posts");

                Console.WriteLine("Press q to exit the progam menu");

                option = Console.ReadLine();

                if (option == "1"){

                    var db = new BloggingContext();

                    var query = db.Blogs.OrderBy(b => b.Name); 

                    Console.WriteLine("All blogs in the database:");
                        foreach (var item in query)
                        {
                            Console.WriteLine(item.Name);
                        }
                }

                else if (option == "2"){

                    try
                    {
                        Console.Write("Enter a name for a new Blog: ");
                
                        var name = Console.ReadLine();

                        var blog = new Blog { Name = name };

                          var db = new BloggingContext();

                          db.AddBlog(blog);

                      var query = db.Blogs.OrderBy(b => b.Name);    

                        Console.WriteLine("All blogs in the database:");
                        foreach (var item in query)
                        {
                            Console.WriteLine(item.Name);
                        }
            
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }
                } else if (option == "3"){

                    Console.WriteLine("Select Blog to Post to");

                    Console.ReadLine();

                    Console.WriteLine("Enter the details for the Post");

                    Console.ReadLine();
                }

                else if(option == "4"){

                    Console.WriteLine("Select Blog to view Posts from");

                    Console.ReadLine();

                    Console.WriteLine("Display all Posts and number of posts");
                    
                    int num = postId.Where (p -> p.PostId.Count());

                    Console.WriteLine($"There are{num} posts from Blogs");

                }   

            } while (option != "q");  

            Console.ForegroundColor = ConsoleColor.White;

            logger.Info("Program Ended");   

        }
    }
}
