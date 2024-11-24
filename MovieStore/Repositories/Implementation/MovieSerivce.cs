using Microsoft.AspNetCore.Authorization;
using MovieStore.Models.Domain;
using MovieStore.Models.DTO;
using MovieStore.Repositories.Abstract;

namespace MovieStore.Repositories.Implementation
{
    [Authorize]
    public class MovieSerivce:IMovieService
    {
        private readonly DataBaseContext database;
        public MovieSerivce(DataBaseContext context)
        {
            database = context;
        }
        public bool Add(Movie model)
        {
            try
            { 
                database.Movie.Add(model);
                database.SaveChanges();
                foreach (int genreId in model.Genres)
                {
                    var MovieGenre = new MovieGenre
                    {
                        MovieId =model.Id,
                        GenreId= genreId,
                    };
                    database.MovieGenre.Add(MovieGenre);
                    database.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.GetById(id);
                    if (data == null)
                {
                    return false;
                }
                var movieGenres = database.MovieGenre.Where(s=>s.MovieId==data.Id);
                foreach(var movieGenre in movieGenres)
                {
                    database.MovieGenre.Remove(movieGenre);
                }
                database.Movie.Remove(data);

                database.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public MovieListVm GetAllMovies(string term = "", bool paging = false, int currentPage = 0)
        {
            var data = new MovieListVm();

            var list = database.Movie.ToList();


            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                list = list.Where(a => a.Title.ToLower().StartsWith(term)).ToList();
            }

            if (paging)
            {
                int pageSize = 5;
                int count = list.Count;
                int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                list = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                data.PageSize = pageSize;
                data.CurrentPage = currentPage;
                data.TotalPages = TotalPages;
            }

            foreach (var movie in list)
            {
                var genres = (from genre in database.Genre
                              join mg in database.MovieGenre
                              on genre.Id equals mg.GenreId
                              where mg.MovieId == movie.Id
                              select genre.GenreName
                              ).ToList();
                var genreNames = string.Join(',', genres);
                movie.GenreNames = genreNames;
            }
            data.MovieList = list.AsQueryable();
            return data;
        }

        public Movie GetById(int id)
        {
            return database.Movie.Find(id);

        }
        public List<int> GetGenreByMovieId(int movieId)
        {
            var genreIds = database.MovieGenre.Where(a => a.MovieId == movieId).Select(a => a.GenreId).ToList();
            return genreIds;
        }
        public bool Update(Movie model) 
        {
            try
            {

                var genresToDeleted = database.MovieGenre.Where(a => a.MovieId == model.Id && !model.Genres.Contains(a.GenreId)).ToList();
                foreach (var mGenre in genresToDeleted)
                {
                    database.MovieGenre.Remove(mGenre);
                }
                foreach (int genId in model.Genres)
                {
                    var movieGenre = database.MovieGenre.FirstOrDefault(a => a.MovieId == model.Id && a.GenreId == genId);
                    if (movieGenre == null)
                    {
                        movieGenre = new MovieGenre { GenreId = genId, MovieId = model.Id };
                        database.MovieGenre.Add(movieGenre);
                    }
                }

                database.Movie.Update(model);
                database.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    
    }
}
