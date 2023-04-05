using System.Collections.Generic;

public class Comment
{
    public int id { get; set; }
    public string body { get; set; }
    public int postId { get; set; }
}

public class Post
{
    public int id { get; set; }
    public string title { get; set; }
}

public class Profile
{
    public string name { get; set; }
}

public class DB
{
    public List<Post> posts { get; set; }
    public List<Comment> comments { get; set; }
    public Profile profile { get; set; }
}