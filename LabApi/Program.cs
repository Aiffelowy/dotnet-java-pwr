

using System.Runtime.CompilerServices;
using System.Text.Json;



namespace LabApi
{
    using Microsoft.EntityFrameworkCore;
    using Errors;
    using Microsoft.EntityFrameworkCore.Query;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.VisualBasic;
    using System.Linq.Expressions;

    readonly struct ApiError
    {
        readonly string msg;

        public ApiError(string msg) { this.msg = msg; }
        public ApiError(Exception e)
        {
            if(e.InnerException is null) { this.msg = e.Message; }
            this.msg = e.InnerException.Message;
        }
        public override string ToString()
        {
            return msg;
        }
    }
    internal class DbManager : DbContext {
        public DbSet<Post> posts { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbManager()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=C:\Users\adask\source\repos\LabApi\LabApi\db.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().HasData();
            modelBuilder.Entity<Comment>().HasData();
        }
    }

    class PostJson {
        required public int userId { set; get; }
        required public string title { get; set; }
        required public string body { get; set; }
    }
    class Post {
        required public int plsWork { set; get; }
        required public int userId { set; get; }
        [Key]
        public int id { set; get; }
        required public string title { get; set; }
        required public string body { get; set; }

        public override string ToString()
        {
            return $"title: {title}; body: {body}";
        }
    }

    class Comment {
        [Key]
        required public int id { set; get; }
        [ForeignKey("PK_posts")]
        required public int postId { set; get; }
        required public string name { set; get; }
        required public string email { set; get; }
        required public string body { set; get; }

        public override string ToString()
        {
            return $"name: {name}; email: {email}; body: {body}";
        }
    }
    class ApiWrapper
    {
        private HttpClient httpclient;
        private DbManager database;
        static string url = "https://jsonplaceholder.typicode.com";

        public ApiWrapper()
        {
            httpclient = new HttpClient();
            database = new DbManager();
        }

        private async Task<Result<Post, ApiError>> fetch_post(int post_id)
        {
            var response = await this.httpclient.GetStringAsync($"{url}/posts/{post_id}");
            var post = JsonSerializer.Deserialize<PostJson>(response);
            if(post == null)
                return Result.Err(new ApiError("Invalid Json"));
            return Result.Ok(new Post { title = post.title, plsWork = post_id, body = post.body, userId = post.userId});
        }

        private async Task<Result<List<Comment>, ApiError>> fetch_comments(int post_id)
        {
            var response = await this.httpclient.GetStringAsync($"{url}/posts/{post_id}/comments");
            try
            {
                var json = JsonSerializer.Deserialize<List<Comment>>(response);
                if(json == null) { return Result.Err(new ApiError("???")); }
                return Result.Ok(json);
            }
            catch (Exception e)
            {
                return Result.Err(new ApiError(e));
            }
        }

        private async Task<Result<Nothing, ApiError>> db_add_post(Post post) {
            await this.database.AddAsync(post);
            try
            {
                await this.database.SaveChangesAsync();
            } catch(Exception e) {
                return Result.Err(new ApiError(e));
            }
            return Result.Ok();
        }

        private async Task<Result<Nothing, ApiError>> db_add_comments(List<Comment> comments)
        {
            await this.database.AddRangeAsync(comments);
            try
            {
                await this.database.SaveChangesAsync();
            } catch(Exception e) {
                return Result.Err(new ApiError(e));
            }
            return Result.Ok();
        }

        public async Task<Result<Post, ApiError>> get_post(int post_id)
        {
            var query = this.database.posts.Where(p => p.plsWork == post_id);
            if(await query.AnyAsync()) {
                var post = await query.FirstAsync();
                return Result.Ok(post);
            }

            var result = await this.fetch_post(post_id);
            if(result.is_err()) { return result; }

            var post_ = result.unwrap();
            var res = await db_add_post(post_);
            if(res.is_err()) { return Result.Err(res.unwrap_err()); }

            return Result.Ok(post_);
        }
        public async Task<Result<List<Comment>, ApiError>> get_comments(int post_id) {
            var query = this.database.comments.Where(c => c.postId == post_id);
            if(await query.AnyAsync())
            {
                var comments = await query.ToListAsync();
                return Result.Ok(comments);
            }

            var result = await this.fetch_comments(post_id);
            if(result.is_err()) { return Result.Err(result.unwrap_err()); }

            var comments_ = result.unwrap();
            var res = await this.db_add_comments(comments_);

            if(res.is_err()) { return Result.Err(res.unwrap_err()); }

            return Result.Ok(comments_);
        }
    }

    internal class MyApp
    {
        static async Task Main(string[] args)
        {
            ApiWrapper api = new ApiWrapper();
            var response = await api.get_post(1);
            //response.unwrap().ForEach(c => Console.WriteLine(c));
            Console.WriteLine(response.unwrap());
        }
    }
}
