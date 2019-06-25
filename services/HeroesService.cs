using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeroesApi.interfaces;
using Newtonsoft.Json;

namespace HeroesApi.services
{
    public class HeroesService : IHeroesService
    {

        private readonly string data_path = System.IO.Directory.GetCurrentDirectory() + "\\data\\heroes.json";
        private List<Hero> heroes;

        public HeroesService()
        {
            heroes = new List<Hero>();
            load();
        }

        public Hero Add(Hero hero)
        {
            hero.Id = GenerateId();
            heroes.Add(hero);
            save();
            return hero;
        }

        public int GenerateId()
        {
            int max = 0;
            foreach (Hero hero in heroes)
            {
                int id = hero.Id;
                if (id > max)
                {
                    max = id;
                }
            }
            max += 1;
            Console.WriteLine("Next highest id is " + max);
            return max;
        }

        public Hero GetById(int id)
        {
            foreach (Hero hero in heroes)
            {
                if (hero.Id == id)
                {
                    return hero;
                }
            }
            return null;
        }

        public IEnumerable<Hero> GetHeroes()
        {
            return heroes;
        }

        public void Remove(int id)
        {
            Hero hero = GetById(id);
            if (hero != null)
            {
                heroes.Remove(hero);
            }
            save();
        }

        public IEnumerable<Hero> SearchHeroes(string name)
        {
            List<Hero> filtered = new List<Hero>();
            name = name.ToLower();
            foreach (Hero hero in heroes)
            {
                string heroName = hero.Name.ToLower();
                if (heroName.Contains(name))
                {
                    filtered.Add(hero);
                }
            }
            return filtered;
        }

        public Hero Update(Hero hero)
        {
            int x = -1;
            for (int i = 0; i < heroes.Count; i++)
            {
                if (heroes[i].Id == hero.Id)
                {
                    x = i;
                    break;
                }
            }
            if (x != -1)
            {
                heroes[x] = hero;
            }
            save();
            return heroes[x];
        }

        private void save()
        {
            string json = JsonConvert.SerializeObject(heroes.ToArray());
            System.IO.File.WriteAllText(data_path, json);
        }

        private void load()
        {
            String data = System.IO.File.ReadAllText(data_path);
            heroes.AddRange(JsonConvert.DeserializeObject<IEnumerable<Hero>>(data));
        }

    }
}
