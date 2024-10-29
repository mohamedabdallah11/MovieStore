using MovieStore.Models.Domain;
using MovieStore.Repositories.Abstract;

namespace MovieStore.Repositories.Implementation
{
    public class GenreService : IGenreService
    {
        private readonly DataBaseContext database;
        public GenreService(DataBaseContext context)
        {
            database = context;
        }
        public bool Add(Genre model)
        {
            try
            {
                database.Genre.Add(model);
                database.SaveChanges();
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
                var genre = this.GetById(id);
            //  var genre = database.Genre.Where(g => g.Id == id).FirstOrDefault();
                if (genre == null)
                {
                    return false;
                }
                database.Genre.Remove(genre);

                database.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<Genre> GetAllGeneries()
        {
            var generies = database.Genre.AsQueryable();
            return generies;
        }

        public Genre GetById(int id)
        {
            return database.Genre.Find(id);

        }

       public bool Update(Genre model)
        {
            try
            {
                database.Genre.Update(model);
                database.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
