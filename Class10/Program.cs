using System;
using System.Linq;
using Class10.Models;
using NLog.Web;

namespace Class10
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            logger.Info("Program started");

            string choice;

            do
            {
                // display choices to user
                Console.WriteLine("1) Display all Blogs");
                Console.WriteLine("2) Add new Blog");
                Console.WriteLine("3) Display all Posts for a Blog");
                Console.WriteLine("4) Add new Post");
                Console.WriteLine("Press Enter to quit");
                choice = Console.ReadLine();
                logger.Info("User choice: {Choice}", choice);

                if (choice == "1")
                {
                    using (var db = new BlogContext())
                    {
                        Console.WriteLine($"{db.Blogs.Count()} Blogs Returned");
                        Console.WriteLine("Blogs:");
                        foreach (var b in db.Blogs)
                        {
                            Console.WriteLine($"Blog: {b.BlogId} Name: {b.Name}");
                        }
                    }
                }

                else if (choice == "2")
                {
                    Console.WriteLine("Enter your Blog name");
                    var blogName = Console.ReadLine();

                    try
                    {
                        if (blogName == "" || blogName == null)
                        {
                            throw new ArgumentNullException(nameof(blogName));
                        }

                        var blog = new Blog();
                        blog.Name = blogName;

                        using (var db = new BlogContext())
                        {
                            db.Add(blog);
                            db.SaveChanges();
                        }
                    }
                    catch (System.Exception)
                    {
                        System.Console.WriteLine("Adding blog failed.");
                        logger.Error("Adding blog failed.", blogName);
                        throw;
                    }
                }

                else if (choice == "3")
                {
                    Console.WriteLine("Enter Blog ID to display posts for:");
                    int blogId = Int32.Parse(Console.ReadLine());

                    using (var db = new BlogContext())
                    {
                        var blog = db.Blogs.Where(x => x.BlogId == blogId).FirstOrDefault();
                        var posts = db.Posts.Where(x => x.BlogId == blogId);
                        Int32 postCount = posts.Count();
                        Console.WriteLine($"Blog {blog.Name} has {postCount} posts:");
                        
                        foreach (var p in posts)
                        {
                            Console.WriteLine($"\tPost: {p.PostId} Title: {p.Title}");
                            Console.WriteLine($"\t{p.Content}");
                        }
                    }
                }

                else if (choice == "4")
                {
                    Console.WriteLine("Enter Blog ID to posts to:");
                    int blogId = Int32.Parse(Console.ReadLine());


                        using (var db = new BlogContext())
                        {
                            var blog = db.Blogs.Where(x => x.BlogId == blogId).FirstOrDefault();
                            Console.WriteLine($"Posting to: {blog.Name}");
                        }

                        Console.WriteLine("Enter your Post title");
                        var postTitle = Console.ReadLine();
                    try
                    {
                        if (postTitle == "" || postTitle == null)
                        {
                            throw new ArgumentNullException(nameof(postTitle));
                        }
                        Console.WriteLine("Write your Post contents");
                        var postContents = Console.ReadLine();

                        var post = new Post();
                        post.Title = postTitle;
                        post.Content = postContents;
                        post.BlogId = blogId;

                        using (var db = new BlogContext())
                        {
                            db.Posts.Add(post);
                            db.SaveChanges();
                        }
                    }
                    catch (System.Exception)
                    {
                        System.Console.WriteLine("Adding post failed.");
                        logger.Error("Adding post failed.", postTitle);
                        throw;
                    }
                }

            } while (choice == "1" || choice == "2" || choice == "3" || choice == "4");

            logger.Info("Program ended");
        }
    }
}
