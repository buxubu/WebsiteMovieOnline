using AutoMapper;
using WebMovieOnline.Models;
using WebMovieOnline.ModelViews;
namespace WebMovieOnline.Helper
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<Movie,MovieModelViews>();
            CreateMap<MovieModelViews,Movie>()/*.ForMember(x=>x.IdLanguages, y=>y.MapFrom(z=>z.idLanguages))*/;
            CreateMap<Video,VideoModelViews>();
            CreateMap<VideoModelViews,Video>();
            CreateMap<Genre, GenresModelViews>();
            CreateMap<GenresModelViews, Genre>();
            CreateMap<Country, CountriesModelViews>();
            CreateMap<CountriesModelViews, Country>();
            CreateMap<Actor, ActorsModelViews>();
            CreateMap<ActorsModelViews, Actor>();
            CreateMap<MovieCastModelViews, MovieCast>();
            CreateMap<MovieCast, MovieCastModelViews>();
            CreateMap<Award, AwardModelViews>();
            CreateMap<AwardModelViews, Award>();
            CreateMap<ActorOfAward, ActorsOfAwardModelViews>();
            CreateMap<ActorsOfAwardModelViews, ActorOfAward>();
            CreateMap<ProductionCompany, ProductionCompaniesModelViews>();
            CreateMap<ProductionCompaniesModelViews, ProductionCompany>();
            CreateMap<Director, DirectorsModelViews>();
            CreateMap<DirectorsModelViews, Director>();
            CreateMap<Movie, MapMovie>();
            CreateMap<MapMovie,Movie>();
            CreateMap<Video, VideoModel>();
            CreateMap<VideoModel, Video>();
        }
    }
}
