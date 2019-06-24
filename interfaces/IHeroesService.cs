using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesApi.interfaces
{
    public interface IHeroesService
    {

        IEnumerable<Hero> GetHeroes();
        Hero Add(Hero hero);
        Hero GetById(int id);
        void Remove(int id);
        Hero Update(Hero hero);
        IEnumerable<Hero> SearchHeroes(string name);
        int GenerateId();

    }
}
