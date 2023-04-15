using AutoMapper;
using Hangfire;
using Hangfire.Storage;
using HC.Shared.Application.Interfaces;
using HC.Shared.Domain.Entities;
using HC.Shared.Infrastructure;
using HC.TmdbBackgroundJob.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace HC.TmdbBackgroundJob.Managers.RecurringJobs
{
    public class TmdbFilmsJobManager
    {
        readonly IPostgreDbContext _db;
        readonly IMapper _mapper;
        public TmdbFilmsJobManager(PostgreDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task Process()
        {
            using (var connection = JobStorage.Current.GetConnection())
            {
                foreach (var recurringJob in connection.GetRecurringJobs())
                {
                    RecurringJob.RemoveIfExists(recurringJob.Id);
                }
            }

            List<Movie> movies = new();


            using (var httpClient = new HttpClient())
            {
                for (int i = 1; i <= 500; i++)
                {

                    using (var response = await httpClient.GetAsync("https://api.themoviedb.org/3/discover/movie?api_key=d0786936b92ba8a7a047c1957feb0088&page=" + i))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        var resp = _mapper.Map<MovieDTO>(System.Text.Json.JsonSerializer.Deserialize<MovieDTO>(apiResponse));
                        resp.results.ForEach(x => { x.page = resp.page; }); 
                        movies.AddRange(resp.results);
                        
                        await Console.Out.WriteLineAsync(i.ToString()+"/500");
                    }
                }
            }
          
            foreach (var item in movies)
            {
                item.movie_id = item.id;
                item.id = 0;
                if (!_db.Movies.Any(x=>x.movie_id == item.movie_id))
                {
                    await _db.Movies.AddAsync(item);
                    await _db.SaveChangesAsync();
                }
            }

            await Console.Out.WriteLineAsync("Güncelleme tamamlandı");




        }
    }
}
