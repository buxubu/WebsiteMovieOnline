using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebMovieOnline.Models;

public partial class DbwebsiteMovieOnlineContext : DbContext
{
    public DbwebsiteMovieOnlineContext()
    {
    }

    public DbwebsiteMovieOnlineContext(DbContextOptions<DbwebsiteMovieOnlineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<ActorOfAward> ActorOfAwards { get; set; }

    public virtual DbSet<Award> Awards { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Cinema> Cinemas { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Director> Directors { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MovieCast> MovieCasts { get; set; }

    public virtual DbSet<MovieOfCinema> MovieOfCinemas { get; set; }

    public virtual DbSet<ProductionCompany> ProductionCompanies { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Reviewer> Reviewers { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=DBWebsiteMovieOnline;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.IdAccount).HasName("PK__account__B2C7C7830DE1C52F");

            entity.ToTable("account");

            entity.Property(e => e.IdAccount).HasColumnName("id_account");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("firstName");
            entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("lastName");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.IdPerson).HasName("PK__actor__E9AB6A41CC27BCF4");

            entity.ToTable("actor");

            entity.Property(e => e.IdPerson).HasColumnName("id_person");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("address");
            entity.Property(e => e.Birthday)
                .HasColumnType("datetime")
                .HasColumnName("birthday");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.Sex)
                .HasMaxLength(50)
                .HasColumnName("sex");
        });

        modelBuilder.Entity<ActorOfAward>(entity =>
        {
            entity.HasKey(e => new { e.IdPerson, e.IdAward }).HasName("PK__actor_of__F13E73911CFFC5C5");

            entity.ToTable("actor_of_award");

            entity.Property(e => e.IdPerson).HasColumnName("id_person");
            entity.Property(e => e.IdAward).HasColumnName("id_award");
            entity.Property(e => e.AwardYear)
                .HasColumnType("datetime")
                .HasColumnName("award_year");

            entity.HasOne(d => d.IdAwardNavigation).WithMany(p => p.ActorOfAwards)
                .HasForeignKey(d => d.IdAward)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__actor_of___id_aw__68487DD7");

            entity.HasOne(d => d.IdPersonNavigation).WithMany(p => p.ActorOfAwards)
                .HasForeignKey(d => d.IdPerson)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__actor_of___id_pe__656C112C");
        });

        modelBuilder.Entity<Award>(entity =>
        {
            entity.HasKey(e => e.IdAward).HasName("PK__award__89519D0080640BE0");

            entity.ToTable("award");

            entity.Property(e => e.IdAward).HasColumnName("id_award");
            entity.Property(e => e.NameAward)
                .HasMaxLength(100)
                .HasColumnName("name_award");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PK__category__E548B673CC34A65D");

            entity.ToTable("category");

            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.NameCategory)
                .HasMaxLength(50)
                .HasColumnName("name_category");
        });

        modelBuilder.Entity<Cinema>(entity =>
        {
            entity.HasKey(e => e.IdCinema).HasName("PK__cinema__B6D4FDA89E660A72");

            entity.ToTable("cinema");

            entity.Property(e => e.IdCinema).HasColumnName("id_cinema");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.NameCinema)
                .HasMaxLength(200)
                .HasColumnName("name_cinema");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .HasColumnName("state");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.IdCountry).HasName("PK__country__294318F4D5D3D473");

            entity.ToTable("country");

            entity.Property(e => e.IdCountry).HasColumnName("id_country");
            entity.Property(e => e.NameCountry)
                .HasMaxLength(100)
                .HasColumnName("name_country");

            entity.HasMany(d => d.IdMovies).WithMany(p => p.IdCountries)
                .UsingEntity<Dictionary<string, object>>(
                    "MoveOfCountry",
                    r => r.HasOne<Movie>().WithMany()
                        .HasForeignKey("IdMovie")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__move_of_c__id_mo__5629CD9C"),
                    l => l.HasOne<Country>().WithMany()
                        .HasForeignKey("IdCountry")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__move_of_c__id_co__5FB337D6"),
                    j =>
                    {
                        j.HasKey("IdCountry", "IdMovie").HasName("PK__move_of___46FA17397B6539FF");
                        j.ToTable("move_of_country");
                        j.IndexerProperty<int>("IdCountry").HasColumnName("id_country");
                        j.IndexerProperty<int>("IdMovie").HasColumnName("id_movie");
                    });
        });

        modelBuilder.Entity<Director>(entity =>
        {
            entity.HasKey(e => e.IdDirector).HasName("PK__director__6B65E2A2F0057458");

            entity.ToTable("director");

            entity.Property(e => e.IdDirector).HasColumnName("id_director");
            entity.Property(e => e.Birhday)
                .HasColumnType("datetime")
                .HasColumnName("birhday");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.IdGenre).HasName("PK__genre__CB767C6944E9986A");

            entity.ToTable("genre");

            entity.Property(e => e.IdGenre).HasColumnName("id_genre");
            entity.Property(e => e.NameGenre)
                .HasMaxLength(100)
                .HasColumnName("name_genre");

            entity.HasMany(d => d.IdMovies).WithMany(p => p.IdGenres)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieOfGenre",
                    r => r.HasOne<Movie>().WithMany()
                        .HasForeignKey("IdMovie")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__movie_of___id_mo__571DF1D5"),
                    l => l.HasOne<Genre>().WithMany()
                        .HasForeignKey("IdGenre")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__movie_of___id_ge__5EBF139D"),
                    j =>
                    {
                        j.HasKey("IdGenre", "IdMovie").HasName("PK__movie_of__A4CF73A425407800");
                        j.ToTable("movie_of_genre");
                        j.IndexerProperty<int>("IdGenre").HasColumnName("id_genre");
                        j.IndexerProperty<int>("IdMovie").HasColumnName("id_movie");
                    });
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.IdLanguage).HasName("PK__language__1D196341CAE9EA5D");

            entity.ToTable("language");

            entity.Property(e => e.IdLanguage).HasColumnName("id_language");
            entity.Property(e => e.NameLanguage)
                .HasMaxLength(100)
                .HasColumnName("name_language");

            entity.HasMany(d => d.IdMovies).WithMany(p => p.IdLanguages)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieOfLanguage",
                    r => r.HasOne<Movie>().WithMany()
                        .HasForeignKey("IdMovie")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__movie_of___id_mo__5812160E"),
                    l => l.HasOne<Language>().WithMany()
                        .HasForeignKey("IdLanguage")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__movie_of___id_la__60A75C0F"),
                    j =>
                    {
                        j.HasKey("IdLanguage", "IdMovie").HasName("PK__movie_of__72A06C8C66802030");
                        j.ToTable("movie_of_language");
                        j.IndexerProperty<int>("IdLanguage").HasColumnName("id_language");
                        j.IndexerProperty<int>("IdMovie").HasColumnName("id_movie");
                    });
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.IdMovie).HasName("PK__movie__FB90FCD71A40A519");

            entity.ToTable("movie");

            entity.Property(e => e.IdMovie).HasColumnName("id_movie");
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .HasColumnName("description");
            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.Images)
                .HasMaxLength(4000)
                .HasColumnName("images");
            entity.Property(e => e.MovieStatus)
                .HasMaxLength(100)
                .HasColumnName("movie_status");
            entity.Property(e => e.NameMovie)
                .HasMaxLength(500)
                .HasColumnName("name_movie");
            entity.Property(e => e.ReleaseDate)
                .HasColumnType("datetime")
                .HasColumnName("release_date");
            entity.Property(e => e.Showtimes).HasColumnName("showtimes");
            entity.Property(e => e.Tagline)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tagline");
            entity.Property(e => e.TrailerMovie)
                .HasMaxLength(4000)
                .HasColumnName("trailer_movie");
            entity.Property(e => e.Trending).HasColumnName("trending");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Movies)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__movie__id_catego__619B8048");

            entity.HasMany(d => d.IdCompanies).WithMany(p => p.IdMovies)
                .UsingEntity<Dictionary<string, object>>(
                    "MoveOfCompany",
                    r => r.HasOne<ProductionCompany>().WithMany()
                        .HasForeignKey("IdCompany")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__move_of_c__id_co__66603565"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("IdMovie")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__move_of_c__id_mo__5AEE82B9"),
                    j =>
                    {
                        j.HasKey("IdMovie", "IdCompany").HasName("PK__move_of___8E40152724FC1FAE");
                        j.ToTable("move_of_company");
                        j.IndexerProperty<int>("IdMovie").HasColumnName("id_movie");
                        j.IndexerProperty<int>("IdCompany").HasColumnName("id_company");
                    });

            entity.HasMany(d => d.IdDirectors).WithMany(p => p.IdMovies)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieOfDirector",
                    r => r.HasOne<Director>().WithMany()
                        .HasForeignKey("IdDirector")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__movie_of___id_di__628FA481"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("IdMovie")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__movie_of___id_mo__5CD6CB2B"),
                    j =>
                    {
                        j.HasKey("IdMovie", "IdDirector").HasName("PK__movie_of__CD26A2FD14175AB2");
                        j.ToTable("movie_of_director");
                        j.IndexerProperty<int>("IdMovie").HasColumnName("id_movie");
                        j.IndexerProperty<int>("IdDirector").HasColumnName("id_director");
                    });
        });

        modelBuilder.Entity<MovieCast>(entity =>
        {
            entity.HasKey(e => new { e.IdMovie, e.IdPerson }).HasName("PK__movie_ca__F50A4A73C69A8DD3");

            entity.ToTable("movie_cast");

            entity.Property(e => e.IdMovie).HasColumnName("id_movie");
            entity.Property(e => e.IdPerson).HasColumnName("id_person");
            entity.Property(e => e.CharacterName)
                .HasMaxLength(100)
                .HasColumnName("character_name");
            entity.Property(e => e.Position)
                .HasMaxLength(100)
                .HasColumnName("position");

            entity.HasOne(d => d.IdMovieNavigation).WithMany(p => p.MovieCasts)
                .HasForeignKey(d => d.IdMovie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__movie_cas__id_mo__59063A47");

            entity.HasOne(d => d.IdPersonNavigation).WithMany(p => p.MovieCasts)
                .HasForeignKey(d => d.IdPerson)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__movie_cas__id_pe__6477ECF3");
        });

        modelBuilder.Entity<MovieOfCinema>(entity =>
        {
            entity.HasKey(e => new { e.IdMovie, e.IdCinema }).HasName("PK__movie_of__60FDB30D952805A4");

            entity.ToTable("movie_of_cinema");

            entity.Property(e => e.IdMovie).HasColumnName("id_movie");
            entity.Property(e => e.IdCinema).HasColumnName("id_cinema");
            entity.Property(e => e.DateFirstShowing)
                .HasColumnType("datetime")
                .HasColumnName("date_first_showing");
            entity.Property(e => e.DateLastShowing)
                .HasColumnType("datetime")
                .HasColumnName("date_last_showing");

            entity.HasOne(d => d.IdCinemaNavigation).WithMany(p => p.MovieOfCinemas)
                .HasForeignKey(d => d.IdCinema)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__movie_of___id_ci__6754599E");

            entity.HasOne(d => d.IdMovieNavigation).WithMany(p => p.MovieOfCinemas)
                .HasForeignKey(d => d.IdMovie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__movie_of___id_mo__5BE2A6F2");
        });

        modelBuilder.Entity<ProductionCompany>(entity =>
        {
            entity.HasKey(e => e.IdCompany).HasName("PK__producti__5D0E9F06CC1777E7");

            entity.ToTable("production_company");

            entity.Property(e => e.IdCompany).HasColumnName("id_company");
            entity.Property(e => e.NameCompany)
                .HasMaxLength(100)
                .HasColumnName("name_company");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => new { e.IdMovie, e.IdRev }).HasName("PK__rating__3D3B1A20AB14E216");

            entity.ToTable("rating");

            entity.Property(e => e.IdMovie).HasColumnName("id_movie");
            entity.Property(e => e.IdRev).HasColumnName("id_rev");
            entity.Property(e => e.RatingCount).HasColumnName("rating_count");
            entity.Property(e => e.RevStar).HasColumnName("rev_star");

            entity.HasOne(d => d.IdMovieNavigation).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.IdMovie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__rating__id_movie__59FA5E80");

            entity.HasOne(d => d.IdRevNavigation).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.IdRev)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__rating__id_rev__6383C8BA");
        });

        modelBuilder.Entity<Reviewer>(entity =>
        {
            entity.HasKey(e => e.IdRev).HasName("PK__reviewer__6ABE6F7D48966956");

            entity.ToTable("reviewer");

            entity.Property(e => e.IdRev).HasColumnName("id_rev");
            entity.Property(e => e.RevName)
                .HasMaxLength(200)
                .HasColumnName("rev_name");
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.IdVideo).HasName("PK__video__2B9E8C403FA6ECE7");

            entity.ToTable("video");

            entity.Property(e => e.IdVideo).HasColumnName("id_video");
            entity.Property(e => e.Episode).HasColumnName("episode");
            entity.Property(e => e.IdMovie).HasColumnName("id_movie");
            entity.Property(e => e.NameVideo)
                .HasMaxLength(500)
                .HasColumnName("name_video");

            entity.HasOne(d => d.IdMovieNavigation).WithMany(p => p.Videos)
                .HasForeignKey(d => d.IdMovie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__video__id_movie__5DCAEF64");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
