namespace WebMovieOnline.ModelViews
{
    public class VideoModelViews
    {
        public int IdVideo { get; set; }

        public string? NameVideo { get; set; }

        public int? Episode { get; set; }

        public IFormFile? UploadVideo { get; set; }
        
        public int IdMovie { get; set; }
    }
    public class VideoModel
    {
        public int IdVideo { get; set; }

        public string? NameVideo { get; set; }

        public int? Episode { get; set; }

        public int IdMovie { get; set; }
    }
}
