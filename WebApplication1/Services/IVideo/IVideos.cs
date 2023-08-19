using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebMovieOnline.ModelViews;

namespace WebMovieOnline.Services.IVideo
{
    public interface IVideos
    {
        Task<IEnumerable<Video>> GetAllVideosAsync();
        Task<Video> AddVideosAsync([FromForm] VideoModelViews model);
        Task<Video> EditVideosAsync([FromForm] VideoModelViews model, int idVideo);
        Task DeleteVideosAsync(int idVideo);
        Task<Video> GetVideosAsync(int idVideo);
    }
    public class RepoVideos : IVideos
    {
        private readonly DbwebsiteMovieOnlineContext _db;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public RepoVideos(DbwebsiteMovieOnlineContext db, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<Video> AddVideosAsync([FromForm] VideoModelViews model)
        {
            string directoryPathVideo = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadVideo");
            string filePathV = Path.Combine(directoryPathVideo, model.UploadVideo.FileName);



            await model.UploadVideo.CopyToAsync(new FileStream(filePathV, FileMode.Create));

            string hostUrl = "https://localhost:7012/";
            string videoUrl = hostUrl + "UploadVideo/" + model.UploadVideo.FileName;

            model.NameVideo = videoUrl;
            var mapVideo = _mapper.Map<Video>(model);
            await _db.Videos.AddAsync(mapVideo);
            await _db.SaveChangesAsync();
            return mapVideo;

        }

        public async Task DeleteVideosAsync(int idMovie)
        {
            var findIdVideo = await _db.Videos.FindAsync(idMovie);

            var directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadVideo");
            var cutNameVideo = findIdVideo.NameVideo.Split("/").Last();

            var fileVideoPath = Path.Combine(directoryPath, cutNameVideo);

            System.IO.File.Delete(fileVideoPath);

            _db.Videos.Remove(findIdVideo);
            await _db.SaveChangesAsync();

        }

        public async Task<Video> EditVideosAsync([FromForm] VideoModelViews model, int idVideo)
        {
            var findIdVideo = await _db.Videos.FindAsync(idVideo);
            if (findIdVideo == null) return null;

            string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadVideo");
            string cutFileNameVideo = findIdVideo.NameVideo.Split("/").Last();

            if (model.UploadVideo.FileName != cutFileNameVideo)
            {
                string delFilePathVideo = Path.Combine(directoryPath, cutFileNameVideo);
                System.IO.File.Delete(delFilePathVideo);

                var filePathVideo = Path.Combine(directoryPath, model.UploadVideo.FileName);

                await model.UploadVideo.CopyToAsync(new FileStream(filePathVideo, FileMode.Create));

                string hostUrl = "https://localhost:7012/";
                string videoUrl = hostUrl + "UploadVideo/" + model.UploadVideo.FileName;

                model.NameVideo = videoUrl;

                findIdVideo.NameVideo = videoUrl;
                findIdVideo.Episode = model.Episode;
                findIdVideo.IdVideo = model.IdVideo;
                _db.Videos.Update(findIdVideo);
                await _db.SaveChangesAsync();
                return findIdVideo;
            }
            return null;
        }

        public async Task<IEnumerable<Video>> GetAllVideosAsync()
        {
            return await _db.Videos.ToListAsync();
        }

        public async Task<Video> GetVideosAsync(int idVideo)
        {
            var getIdVideo = await _db.Videos.Where(x=>x.IdVideo == idVideo).FirstOrDefaultAsync();
            return getIdVideo;
        }
    }
}
