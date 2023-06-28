using System.ComponentModel.DataAnnotations;

namespace WebMovieOnline.ModelViews
{
    public class AwardModelViews
    {
        public int IdAward { get; set; }

        public string? NameAward { get; set; }

    }
    public class ActorsOfAwardModelViews
    {
        public int IdPerson { get; set; }

        public int IdAward { get; set; }
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime? AwardYear { get; set; }
    }
}
