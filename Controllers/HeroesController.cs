using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeroesApi.interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HeroesApi.Controllers
{

    [Route("api/[controller]")]
    public class HeroesController : Controller
    {

        private IHeroesService service;

        public HeroesController(IHeroesService service)
        {
            this.service = service;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<Hero>> Get([FromQuery(Name = "name")]string name)
        {
            if (name == null)
            {
                return Ok(service.GetHeroes());
            }
            else
            {
                return Ok(service.SearchHeroes(name));
            }
            
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<Hero> Get(int id)
        {
            Hero hero = service.GetById(id);
            if (hero == null) 
            {
                return NotFound();
            }
            return Ok(hero);
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult Post([FromBody]Hero hero)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            };

            Hero mHero = service.Add(hero);

            return CreatedAtAction("Post", new { id = mHero.Id }, mHero);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public ActionResult Put([FromBody]Hero hero)
        {
            
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState); 
            }

            Hero mHero = service.GetById(hero.Id);

            if (mHero == null)
            {
                return NotFound();
            }

            mHero = service.Update(hero);

            return AcceptedAtAction("Put", mHero);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Hero hero = service.GetById(id);

            if (hero == null) 
            {
                return NotFound();
            }

            service.Remove(id);
            return Ok();
        } 
    }
}
