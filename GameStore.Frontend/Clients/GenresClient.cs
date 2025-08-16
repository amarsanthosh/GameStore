using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients
{
    public class GenresClient(HttpClient httpClient)
    {
        public async Task<Genre[]> GetGenresAsync()
        => await httpClient.GetFromJsonAsync<Genre[]>("genres") ?? [];
    }
}